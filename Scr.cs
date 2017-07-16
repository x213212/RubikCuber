using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scr : MonoBehaviour {
    Scrollbar tx;
    GameObject targetObject;
    public GameObject temp;
    //控制魔術方塊每一格所要縮放的數值
    Scrollbar bar;
    // Use this for initialization
    void Start () {
      temp = GameObject.Find("Scrollbar");
        targetObject = GameObject.Find("MagicCube");
    }
    public float minFov = 15f;
    public float maxFov = 15f;
    public float sensitivity =25;
    public bool change_bool = false;
    float sensitivitytmp;


    void Update()
    {
     

        bar = temp.GetComponent<Scrollbar>();
        if (bar.value != sensitivitytmp)
        {
            object[] msgArrCS2 = new object[1];
            msgArrCS2[0] = 0.1f + (bar.value * 1.8f);
            sensitivitytmp = (float)msgArrCS2[0];
            targetObject.SendMessage("set_speed", msgArrCS2);
      
            
        }





    }

        
    


}
