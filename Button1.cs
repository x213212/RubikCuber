using UnityEngine;
using System.Collections;

public class Button1 : MonoBehaviour
{
    public GameObject secScreen;
    public Animator ani;
    public GameObject secScreen2;
    public Animator ani2;
    private bool isbool = false;
    //控制切換按鈕控制的縮放動畫

    void Start()
    {

    }


    void Update()
    {

    }
    public void show()
    {


        secScreen.SetActive(true);
        ani.enabled = true;

    }
    public void hide()
    {
        secScreen.SetActive(false);
        ani.enabled = false;

    }
    public void show2()
    {


        secScreen2.SetActive(true);
        ani2.enabled = true;

    }
    public void hide2()
    {
        secScreen2.SetActive(false);
        ani2.enabled = false;

    }
}
