using UnityEngine;

public class CubeEffect : MonoBehaviour
{
    public Material material;

    private MeshFilter filter;
    private Mesh mesh;
    private Renderer renderer;

    private void CreateMesh()
    {
        filter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        filter.mesh = mesh;
        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;

        mesh.name = "Quad Mesh";
        mesh.vertices = new Vector3[] 
        {
			new Vector3(-1.0f, 1.0f, 0.0f),
			new Vector3(1.0f, 1.0f, 0.0f),
			new Vector3(-1.0f, -1.0f, 0.0f),
			new Vector3(1.0f, -1.0f, 0.0f)
		};
        mesh.triangles = new int[]
        {
			0, 1, 2,
			2, 1, 3
		};
        mesh.uv = new Vector2[] 
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
        };
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * float.MaxValue);

        filter.sharedMesh = mesh;
    }

    public void Start()
    {
        CreateMesh();
    }
}