using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class dropdown : MonoBehaviour
{
    List<string> ans = new List<string>() { "初始狀態", "上層十字","上層角塊","中層邊塊","下層十字","下層角塊" };
    public Dropdown drop;

    // Use this for initialization
    void Start()
	{        drop.ClearOptions();
        popliatelist();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setans_mode(int index)
    {
        Debug.Log(index);
        
        GameObject targetObject;
        
        object[] msgArrCS = new object[1];
        
        msgArrCS[0] = index;
            

        targetObject = GameObject.Find("MagicCube");
        targetObject.SendMessage("set_anscount", msgArrCS);
      
    }
    void popliatelist()
    {
        drop.AddOptions(ans);
      
    }
    void Refresh()
    {
        drop.ClearOptions();
    }
 
}