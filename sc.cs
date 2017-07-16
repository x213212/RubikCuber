using UnityEngine;
using System.Collections;

public class sc : MonoBehaviour {

	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Home) )
		{
			Application.Quit();
		}
	}

	void OnGUI()
	{
    //   if (GUI.Button(new Rect(50,300, 100, 100), "開啟設定"))
     //   {


    //    }

    }
   
}
