using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//傳遞魔術方塊顏色的陣列到其他腳本

public class touch : MonoBehaviour, IPointerExitHandler,IPointerClickHandler {
   public RectTransform panel;
   GameObject targetObject;

    GameObject targetObject2;
   public static GameObject targetObject3 ;
    public static Image tx;
    public static Image tx2;
    public static Image tx3;
    static string tmp;
    public static int now;
    public static int now_select;
    public  static int now_color;
    static bool seach_lock = false;
    static public int[,] cube_arr = new int[6, 9];
    static GameObject []gamearr =new GameObject[9];
    static bool firs=false;
    static bool sec = false;
    static Color[] colorarr = new Color[6];
    static int change =0;
    static int[] cubearr_change = new int[6];
    public int get_now()
    {
        return now;
    }
    void Start() {
        panel = GetComponent<RectTransform>();
        
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                cube_arr[i, j] = i;

        for (int i = 0; i < 6; i++)
           cubearr_change[i]=0;

        change = 0;
    }

    // Use this for initialization

    // Update is called once per frame

    void Update () {
      
    }
    void OnMouseEnter()
    {
      

    }
    public void cube_arrreset()
    {//根據本身進行陣列重新定位

        object[] msgArrCS=new object[6] ;
     for(int i=0;i<6;i++)
        msgArrCS[i] = cubearr_change[i];
    

        targetObject = GameObject.Find("MagicCube");
        targetObject.SendMessage("arr_change", msgArrCS);
        Debug.Log("cube_arrreset摳了");

    }
    public void sendchangearr()
    {
        object[] msgArrCS = new object[3];
        for (int i = 0; i < 6; i++)

            for (int j = 0; j < 9; j++)
            {
           
                msgArrCS[0] = i;
                msgArrCS[1] = j;
                msgArrCS[2] = cube_arr[i, j];

                targetObject = GameObject.Find("MagicCube");

                targetObject.SendMessage("uiset_cubearr", msgArrCS);
            
        }

    }
    public void Exit()
    {
        object[] msgArrCS2 = new object[6];
        for (int i = 0; i < 6; i++)
            msgArrCS2[i] = cubearr_change[i];


        targetObject = GameObject.Find("MagicCube");
        targetObject.SendMessage("arr_change", msgArrCS2);
 
        Debug.Log("cube_arrreset摳了");
    }
    public void uiset_cube()
    {//陣列的傳遞過程中必須要轉成object然後再傳遞過那邊的腳本再進行解析
       

        int a = now;
        int b = now_select;
        int c = now_color;

        cubearr_change[a] = 1;
        cube_arr[a, b] = c;





  
    }
    public void uiset_value()
    {//根據顏色陣列然後設定到表單顏色



        for (int i = 0; i < 6; i++)
            if (name.ToString() == "text" + i)
            {
                if (seach_lock == false) { 
                targetObject3 = GameObject.Find("buttontext");
                    seach_lock = true;
                  
                }
                Text textbox = targetObject3.GetComponent<Text>();
              
                textbox.text = "face:"+i ;
                now = i;
             
                for (int j = 0; j < 9; j++)
                {

                    tx3 = gamearr[j].GetComponent<Image>();
                    tx3.color = colorarr[cube_arr[now, j]];
                }
             
            }
 
        Debug.Log("unset摳了2");
    }

    public void OnPointerClick(PointerEventData eventData)
    {//設定顏色

       
        if (firs == false)
        {

            for (int i = 1; i < 10; i++) {
                targetObject = GameObject.Find("input" + i);
                if (targetObject.name == "input" + i)
            {
                gamearr[i - 1] = targetObject;
                    firs = true;
                
                }
        }
           
        }
        if (sec == false)
        {

            for (int i = 0; i < 6; i++)
            {
                targetObject = GameObject.Find( i.ToString());
                if (targetObject.name ==   i.ToString())
                {
                    tx2 = targetObject.GetComponent<Image>();
                    colorarr[i] = tx2.color;
                    sec = true;
                }
            }
      
        }

        targetObject = GameObject.Find(panel.name.ToString());
        Debug.Log(targetObject.name.ToString());
        if (targetObject.name == "0"|| targetObject.name == "1"|| 
            targetObject.name == "2"|| targetObject.name == "3" ||
            targetObject.name == "4"|| targetObject.name == "5")
        {
       
            tx2 = targetObject.GetComponent<Image>();
            // tx.color = tx2.color;
            Debug.Log("color_nomber : "+targetObject.name.ToString());
     
            now_color = Convert.ToInt16(targetObject.name.ToString());;
            change = 1;

        }
        else
        {
            bool isbo=false;
       
            for (int i = 1; i < 10; i++)
                if (targetObject.name == "input" + i)
                {
                    now_select = i-1;
                   
                        
                    tx = targetObject.GetComponent<Image>();
                    // tx.color = tx2.color;
                    try
                    {
                        tx.color = tx2.color;


                    }
                    catch (Exception x)
                    { }

                }

                Debug.Log(targetObject.name.ToString());
         
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
    public void setcolor(object[] tok)
    { 
        cube_arr[now, (int)tok[0]]= (int)tok[1];
    }

}
