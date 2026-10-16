# RubikCuber: from a Unity 8355 solver to a learning experiment

Hi, I am x213212. This started as my university Rubik's Cube teaching project: represent a cube, find a solution using the 8355 method, and show the moves in Unity. Later I revisited it in Python to see whether a small model could learn useful moves instead of always following a long rule-based solution.

This README documents that development process, including what failed and what the recorded results actually cover.

**Topics:** Rubik's Cube, 8355 method, Unity/C#, Python/PyTorch, imitation learning, curriculum learning, MCTS, and DAgger.

- [Original project README](README.md)
- [Original Unity project article, in Traditional Chinese](https://x213212.github.io/2018/06/unity.html)
- [2024 DQN experiment](https://x213212.github.io/2024/11/mstc-dqn-cube.html)
- [2026 development write-up, in Traditional Chinese](https://x213212.github.io/2026/10/rubik8355-ai-training.html)

## What is available here

The public repository contains the original Unity/C# project and this documentation. The later Python `ai8355` lab currently exists in my local checkout; its source, datasets, and checkpoints are not included in this documentation update. Cloning this repository alone does **not** provide the Python training or inference commands.

The experiment results below are transcribed from local training notes dated **September 5, 2026**. They are historical observations, not a new benchmark of a published model. Those notes do not identify a frozen checkpoint hash or every evaluation setting, so they are not a complete reproduction package.

## 1. The original Unity project

The original project combined cube-state representation, an 8355 solver, and animated moves. Its teaching goal was to show how pieces move through the solving stages.

The solver code includes `MagicCube.cs`, `Cube.cs`, and `RotateTarget.cs`. The 8355 stages used in this project are:

```text
8 first-layer pieces -> 3 middle-layer edges -> 5 remaining edges -> 5 remaining corners
```

The working corner and edge provide room for the later exchanges. This is a constructive solving method, rather than a shortest-path solver.

For the original application, use the Unity project workflow described in [Open and run](README.md#open-and-run). The README expects a Unity project folder; if your checkout lacks `Assets` and `ProjectSettings`, the loose C# scripts alone are not a complete Unity application. The old README also links to the original project download, whose availability is not verified by this documentation update.

## 2. Verify the cube before training a model

The Python rewrite first separated a geometric cube environment from the ported C# decision logic. It generates sticker permutations from integer 3D geometry and tests inverse moves, four quarter turns, and color conservation.

Auditing the old implementation uncovered several concrete problems:

- Some F/B/L/R counterclockwise sticker mappings did not invert their clockwise mappings.
- The last-five-edges stopping condition checked only part of the cube. The short scramble `U' F R` is a regression case.
- The final-corner predicate accepted additional incomplete terminal states.
- Returning a move history did not itself establish that all 54 stickers were solved.

The local deterministic port validates physical cube legality, bounds its solving loops, checks the complete final state, and independently replays the answer from the original input through the geometric engine. Missing cases produce an error with a counterexample rather than a success result.

The audit notes record zero failures across **40,000 legal 50-move scrambles** and **10,000 uniformly sampled legal cubie states**. These are tests of the deterministic solver. They are neither neural-network success rates nor a mathematical proof covering every cube state.

## 3. The DQN experiment and its limitations

The earlier learning approach chose moves using DQN. The revised research implementation introduced Double DQN targets, a target network, action masks, Huber loss, gradient clipping, and imitation warm-up. Its observation explicitly included the solving phase and previous action. Macro-action discounting used the number of primitive moves, rather than treating a whole formula as one turn.

Colors became categorical one-hot inputs; numeric color IDs should not imply an ordering. More importantly, decreasing training loss was no longer treated as evidence that a model could finish a cube.

## 4. A separate state-only policy/value model

The v3 experiment starts a new model instead of continuing the old DQN checkpoint. It receives only the current **54 facelets**, encoded as 324 one-hot values. It predicts a prior over **14 primitive quarter-turn actions**, including E-slice turns, and a remaining-distance estimate.

For teacher-generated intermediate states, the deterministic solver replans from that state. This avoids giving a state-only learner labels that depend on an unobserved C# phase or counter. The model does not receive the scramble history during inference.

The default network has **416,655 parameters**. The recorded FP32 checkpoint is about 1.6 MB; the recorded INT8 TorchScript export is **500,777 bytes**, approximately 489 KiB. These are model sizes, not runtime or solve-quality measurements.

## 5. Curriculum, search, and failure-driven data

The learning pipeline combines several components:

| Component | Purpose |
| --- | --- |
| Verified 8355 teacher | Supply complete, replay-checked solutions and candidate continuations |
| Reverse curriculum | Supply short solutions near the goal, then increase scramble length |
| Policy/value model | Estimate promising moves and remaining distance from the current state |
| PUCT/MCTS | Search with an explicit simulation budget for each chosen move |
| DAgger | Collect states reached by the learner and add corrective training examples |
| Replay verification | Check the complete returned move sequence against the original input |

Reverse curriculum may use the inverse of a generated scramble as a **training** target. Evaluation still gives the model only the current cube. Scramble length describes how a problem was generated; it is not necessarily its optimal solution distance.

Adding data did not always improve the model. One DAgger round visited 7,704 states and added 6,466 deduplicated examples, but its fine-tuning runs failed to beat the earlier checkpoint. The better checkpoint was retained. Later work focused on failed MCTS episodes and increased the weight of corrective examples.

## Recorded results and how to read them

The following rows use different test populations and inference methods. They should not be read as a controlled progression on one shared benchmark.

| Experiment | Evaluation population and method | Recorded outcome |
| --- | --- | --- |
| Static 8355 teaching data | Scramble lengths 1–50; autonomous model rollout | 2% success despite falling training loss |
| Added reverse curriculum | 200 unseen scrambles of lengths 1–12; greedy rollout | Best success rate: 55.5% |
| Focused curriculum plus MCTS | Same mixed-depth panel of 100 unseen states; 64 simulations per move; 50-move limit | 65% to 66% success; 4.61 moves on successful cases |
| Later CUDA evaluation | 1,000 new-seed scrambles, all length 6; 50-move failure cap | 972/1,000 solved, or 97.2%; 5.86 moves on successes; capped mean 7.096 |

For the mixed-depth panel, the notes report 100% success at scramble lengths 1–5 and 0% at lengths 11–12. The original 8355 baseline averaged 200.49 moves across the entire panel. Its complete depth distribution and exact seeds are not preserved in the summary notes.

For the later length-6 evaluation, the same-input 8355 baseline averaged 211.91 moves. The historical summary does not restate its MCTS simulation count or exact seed. Do not copy the 64-simulation setting from the other experiment into this row.

The successful-case average excludes failures. The capped average includes each failure as 50 moves: `(972 × 5.86 + 28 × 50) / 1000` is approximately 7.096, with rounding in the reported successful-case mean. A short solution on successful cases does not establish a speedup over a baseline measured on all inputs. Move count also does not measure elapsed time.

The move-count conventions also differ: the teacher baseline counts notation tokens, so `U2` counts as one token, while the model selects individual quarter turns. These historical counts have not been normalized to a shared turn metric and should not be used for a strict efficiency ranking.

## Autonomous evaluation and optional fallback

Research evaluation allows the model and MCTS to solve independently. Failure stays a failure. The evaluator also computes the 8355 baseline for comparison, but does not use that solution to rescue the model rollout.

The local application CLI separately supports `--fallback-teacher`. It compares a plain 8355 solution with a model prefix followed by an 8355 continuation, selects the shorter candidate, and verifies the selected answer by replay. This is a hybrid solver. Its completed solutions must not be reported as autonomous model successes, and its move-count bound applies when both candidate computations complete successfully.

## Current limits and the next acceptance gate

The recorded checkpoint is an intermediate result, not a converged arbitrary-state solver. The length-6 evaluation still misses the proposed 99% acceptance threshold, and deeper scrambles remain a major gap.

The intended gate for each curriculum depth is three disjoint fixed-seed evaluations of 1,000 cases each, with at least 99% autonomous MCTS success in every evaluation. Capped move counts should also stabilize. Loss and next-move accuracy remain diagnostic metrics rather than substitutes for full-solve evaluation.

Future result packages should include the source revision, checkpoint hash, complete evaluation command, seeds, search budget, hardware, wall-clock timing, and raw per-case outcomes. The historical notes do not supply all of these, so this README makes no new general accuracy or runtime claim.

## Local lab layout

These names describe the local lab, rather than files available in this public checkout:

| Local module | Responsibility |
| --- | --- |
| `cube.py`, `validation.py` | Geometric turns and physical-state validation |
| `legacy_solver.py` | Audited deterministic 8355 port |
| `train.py`, `solve.py` | Earlier DQN research path |
| `expert_data.py`, `expert_model.py`, `train_expert.py` | State-only teacher data and v3 training |
| `mcts.py`, `mcts_solve.py` | PUCT search and solving CLI |
| `dagger.py`, `evaluate_expert.py`, `export_expert.py` | Corrective data, strict evaluation, and export |

## License

The public project is licensed under [Apache-2.0](LICENSE).
