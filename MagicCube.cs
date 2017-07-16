using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Threading;
using UnityEngine.UI;

public class MagicCube : MonoBehaviour
{
    public GameObject startScreen;
    public string outTrigger;
    private List<GameObject> screenHistory;
    public  GameObject cubePrefab;
    public GameObject cubePrefab2;
    public AudioClip rotateClip;
    public AudioClip buttonClip;
    static public int[,] cube_arr = new int[6, 9];
    static public int[,] cube_arr2 = new int[6, 9];
    static public int[,] cube_arr3 = new int[6, 9];
    public UnityEngine.UI.Text countText;
    public UnityEngine.UI.Text countText2;
    private Transform[] cubes;
    private Cube[] cubeComs;
    public Vector3[] savedPositions;
    private int[] indexMapping;
    private Transform root;
    private System.Collections.Generic.List<int> history = new System.Collections.Generic.List<int>();
    private System.Collections.Generic.List<int> history2 = new System.Collections.Generic.List<int>();
    private int groupIndex = 0;
    private bool moveLock = false;
    private bool controlLock = false;
    private int count = 0;
    public string ssssss = "1";
    string str;
    private AndroidJavaObject toastExample = null;
    private AndroidJavaObject activityContext = null;
    private bool isShowing = false;
    static int change = 0;
    static int[] cubearr_change = new int[6];
    public GameObject cube;
    public bool color_true = false;
    private System.Threading.Thread thread;
    public int ans_count = 0;
    //cube_angle_trun false=left
    //cube_angle_trun right=right
    private bool cube_angle_trun = false;
    private int cube_angle_face = 0;
    private bool cube_angle_colortrue = false;
    public int counter = 3;

    public int first_load=0;
    public int[,] final_level3 = { { 0, 4 },
                                   { 1, 4 },
                                   { 3, 4 },
                                   { 2, 4 },
                                   { 0, 1 }};
    public int final_angle = 0;
    public int trun_number = 0;
    public int whoface = 0;
    static    public bool restart_cube = false;
    static public bool trans_arr = false;
    public int ans_tmpback=0;
    /// <summary>
    /// 目前所選擇模式
    /// </summary>
    public int chancemode = 0;

    /// <summary>
    /// 解法起始點
    /// </summary>
    public int defaults = 0;
    public int topcross = 0;
    public int topangle = 0;
    public int midblock = 0;
    public int downcross = 0;
    public int downangle = 0;

    /// <summary>
    /// 儲存當前陣列
    /// </summary>
    /// 	
    static public int[,] defaultarr = new int[6, 9];
	static public int[,] topcrossarr = new int[6, 9];
	static public int[,] topanglearr = new int[6, 9];
	static public int[,] midblockarr = new int[6, 9];
	static public int[,] downcrossarr = new int[6, 9];
	static public int[,] downanglearr = new int[6, 9];

    /// <summary>
    /// 有無更改
    /// </summary>
    static public bool change_bool = false;
    /// <summary>
    /// speed
    /// </summary>
    static public float speed=0.1f;
    int[] ans_tmp;

    public Transform CreateCube(int[] indexs, Vector3 position)
    {


  

            cube = Instantiate(cubePrefab);
  
        cube.transform.parent = transform;
        cube.transform.localPosition = position;
        cube.GetComponent<Cube>().Create(indexs);
        return cube.transform;
    }
    public void set_speed(object[] tok)
    {
        speed = (float)(tok[0]);
       // Debug.Log(speed);
    }

    public void uiset_cubearr(object[] tok)
    {
        Debug.Log("uiset_cubearr被摳了");

        cube_arr[(int)tok[0], (int)tok[1]] = (int)tok[2];

        Debug.Log((int)tok[0] + "   " + (int)tok[1] + "  " + (int)tok[2]);
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                cube_arr2[i, j] = cube_arr[i, j];
        trans_arr = true;

        change_bool = true;

    }
    public void arr_change(object[] tok)
    {
        Debug.Log(" arr_change被摳了");
        string tmpstr2 = "";

        for (int i = 0; i < 6; i++)
        {
            cubearr_change[i] = (int)tok[i];

        }

        Debug.Log(change_bool);
    }

    void Start()
    {

        change = 0;
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                cube_arr[i, j] = i;
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                cube_arr2[i, j] = cube_arr[i, j];



        Quaternion quate = Quaternion.identity;
        quate.eulerAngles = new Vector3(0, 0, 0);
        GameObject test;
        test = GameObject.Find("MagicCube");
        test.transform.rotation = quate;


        //陣列 重新定位



 

        cubes = new Transform[]
               {
            CreateCube(new int[] { cube_arr[0, 0] + 1, 0, cube_arr[5, 0] + 1, 0, cube_arr[3, 0] + 1, 0 }, new Vector3(-1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 1] + 1, 0, cube_arr[5, 1] + 1, 0, 0, 0 }, new Vector3(0, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 2] + 1, 0, cube_arr[5, 2] + 1, 0, 0, cube_arr[1, 0] + 1}, new Vector3(1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 3] + 1, 0, 0, 0, cube_arr[3, 1] + 1, 1 }, new Vector3(-1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 4] + 1, 0, 0, 0, 0, 0 }, new Vector3(0, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 5] + 1, 0, 0, 0, 0,  cube_arr[1, 1] + 1 }, new Vector3(1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 6] + 1, 0, 0, cube_arr[4, 0] + 1, cube_arr[3, 2] + 1,2 }, new Vector3(-1, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 7] + 1, 0, 0, cube_arr[4, 1] + 1, 0, 0 }, new Vector3(0, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 8] + 1, 0, 0, cube_arr[4, 2] + 1, 0,  cube_arr[1, 2] + 1 }, new Vector3(1, -1, -1)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 3] + 1, 0, cube_arr[3, 3] + 1, 0 }, new Vector3(-1, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 4] + 1, 0, 0, 0 }, new Vector3(0, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 5] + 1, 0, 0,  cube_arr[1, 3] + 1  }, new Vector3(1, 1, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, cube_arr[3, 4] + 1, 0 }, new Vector3(-1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0, 0 }, new Vector3(0, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0,  cube_arr[1, 4] + 1  }, new Vector3(1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 3] + 1, cube_arr[3, 5] + 1, 0 }, new Vector3(-1, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 4] + 1, 0, 0 }, new Vector3(0, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 5] + 1, 0,  cube_arr[1, 5] + 1  }, new Vector3(1, -1, 0)),
            CreateCube(new int[] { 0, cube_arr[2, 0] + 1, cube_arr[5, 6] + 1, 0, cube_arr[3, 6] + 1, 0 }, new Vector3(-1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 1] + 1, cube_arr[5, 7] + 1, 0, 0, 0 }, new Vector3(0, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 2] + 1, cube_arr[5, 8] + 1, 0, 0, cube_arr[1, 6] + 1  }, new Vector3(1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 3] + 1, 0, 0, cube_arr[3, 7] + 1, 0 }, new Vector3(-1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 4] + 1, 0, 0, 0, 0 }, new Vector3(0, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 5] + 1, 0, 0, 0,cube_arr[1, 7] + 1  }, new Vector3(1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 6] + 1, 0, cube_arr[4, 6] + 1, cube_arr[3, 8] + 1, 0 }, new Vector3(-1, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 7] + 1, 0, cube_arr[4, 7] + 1, 0, 0 }, new Vector3(0, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 8] + 1, 0, cube_arr[4, 8] + 1, 0, cube_arr[1, 8] + 1  }, new Vector3(1, -1, 1)),
               };

        indexMapping = new int[27];
        cubeComs = new Cube[27];
        savedPositions = new Vector3[27];
        for (int i = 0; i < 27; ++i)
        {
            indexMapping[i] = i;
            cubeComs[i] = cubes[i].GetComponent<Cube>();

            savedPositions[i] = cubes[i].localPosition;
            cubeComs[i].SetColor(new Color(0.3f, 0.3f, 0.3f, 1));
        }

        root = new GameObject("root").transform;
        root.parent = transform;
        root.localPosition = Vector3.zero;

        first_load = 1;
        OnReset(null);
      


    }
    public void Cub_reset()
    {
        int[,] myArray = new int[3, 3];
        int[,] myArray2 = new int[3, 3];
        int[] tmp = new int[9];
        int count = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                tmp[count] = cube_arr[1, i];
                count++;
            }

        int[] arr = new int[9];
        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[1, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[1, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[1, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[1, i] = arr[j];

        count = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                myArray[i, j] = cube_arr[1, count];
                count++;
            }
        count = 0;

        lef90(myArray, myArray2, 3, 3);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                tmp[count] = myArray2[i, j];
                count++;
            }


        for (int i = 0; i < 9; i++)
            cube_arr[1, i] = tmp[i];


        ///////////////////////////

        count = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                myArray[i, j] = cube_arr[3, count];
                count++;
            }
        count = 0;
        lef90(myArray, myArray2, 3, 3);
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                tmp[count] = myArray2[i, j];
                count++;
            }

        for (int i = 0; i < 9; i++)
            cube_arr[3, i] = tmp[i];


        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[2, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[2, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[2, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[2, i] = arr[j];

        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[5, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[5, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[5, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[5, i] = arr[j];


    }
    public void Cub_reset2()
    {
        int[,] myArray = new int[3, 3];
        int[,] myArray2 = new int[3, 3];
        int[] tmp = new int[9];
        int count = 0;
        count = 0;
        /*
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                myArray[i, j] = cube_arr[1, count];
                count++;
            }
        count = 0;

        rot90(myArray, myArray2, 3, 3);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                tmp[count] = myArray2[i, j];
                count++;
            }
            /*
        for (int i = 0; i < 9; i++)
            cube_arr[1, i] = tmp[i];*/

        int[] arr = new int[9];
        /*
        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[1, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[1, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[1, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[1, i] = arr[j];
            */
        /*
    for (int i = 0; i < 9; i++)
        arr[i] = cube_arr[3, i];
    for (int i = 0, j = 2; i < 3; i++, j--)
        cube_arr[3, i] = arr[j];
    for (int i = 3, j = 5; i < 6; i++, j--)
        cube_arr[3, i] = arr[j];
    for (int i = 6, j = 8; i < 9; i++, j--)
        cube_arr[3, i] = arr[j];*/
        /*
        count = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                myArray[i, j] = cube_arr[3, count];
                count++;
            }
        count = 0;
        rot90(myArray, myArray2, 3, 3);
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                tmp[count] = myArray2[i, j];
                count++;
            }
        for (int i = 0; i < 9; i++)
            cube_arr[3, i] = tmp[i];
            */



        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[2, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[2, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[2, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[2, i] = arr[j];

        for (int i = 0; i < 9; i++)
            arr[i] = cube_arr[5, i];
        for (int i = 0, j = 2; i < 3; i++, j--)
            cube_arr[5, i] = arr[j];
        for (int i = 3, j = 5; i < 6; i++, j--)
            cube_arr[5, i] = arr[j];
        for (int i = 6, j = 8; i < 9; i++, j--)
            cube_arr[5, i] = arr[j];



    }
    public void show_double()
    {
  
        Quaternion quate = Quaternion.identity;
        quate.eulerAngles = new Vector3(0, 0, 0);
        GameObject test;
        test = GameObject.Find("MagicCube");
        test.transform.rotation = quate;
        if (cubeComs != null) {

            for (int i = 0; i < 27; i++)
        {
            cubeComs[i].all_clear();
            }
           
        }

        



        //陣列 重新定位
     //   Debug.Log("----------------------------------------------------------------------------");
        if (restart_cube == true)
        {
         
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = cube_arr2[i, j];
            /*
            if (chancemode == 1)
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = topcrossarr[i, j];
            if (chancemode == 2)
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = topanglearr[i, j];
            if (chancemode == 3)
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = midblockarr[i, j];
            if (chancemode == 4)
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = downcrossarr[i, j];
            if (chancemode == 5)
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 9; j++)
                        cube_arr[i, j] = downanglearr[i, j];
                        */
            Debug.Log("----------------------------------------------------------------------------");
        }
       else if (trans_arr == true)
        {
            trans_arr = false;
        }
        else
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 9; j++)
                    cube_arr[i, j] = i;
        }
        if (change_bool == true)
        {
            Cub_reset();
            change_bool = false;
          
        }
        
        cubes = new Transform[]
        {
            CreateCube(new int[] { cube_arr[0, 0] + 1, 0, cube_arr[5, 0] + 1, 0, cube_arr[3, 0] + 1, 0 }, new Vector3(-1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 1] + 1, 0, cube_arr[5, 1] + 1, 0, 0, 0 }, new Vector3(0, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 2] + 1, 0, cube_arr[5, 2] + 1, 0, 0, cube_arr[1, 0] + 1}, new Vector3(1, 1, -1)),
            CreateCube(new int[] { cube_arr[0, 3] + 1, 0, 0, 0, cube_arr[3, 1] + 1, 1 }, new Vector3(-1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 4] + 1, 0, 0, 0, 0, 0 }, new Vector3(0, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 5] + 1, 0, 0, 0, 0,  cube_arr[1, 1] + 1 }, new Vector3(1, 0, -1)),
            CreateCube(new int[] { cube_arr[0, 6] + 1, 0, 0, cube_arr[4, 0] + 1, cube_arr[3, 2] + 1,2 }, new Vector3(-1, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 7] + 1, 0, 0, cube_arr[4, 1] + 1, 0, 0 }, new Vector3(0, -1, -1)),
            CreateCube(new int[] { cube_arr[0, 8] + 1, 0, 0, cube_arr[4, 2] + 1, 0,  cube_arr[1, 2] + 1 }, new Vector3(1, -1, -1)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 3] + 1, 0, cube_arr[3, 3] + 1, 0 }, new Vector3(-1, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 4] + 1, 0, 0, 0 }, new Vector3(0, 1, 0)),
            CreateCube(new int[] { 0, 0, cube_arr[5, 5] + 1, 0, 0,  cube_arr[1, 3] + 1  }, new Vector3(1, 1, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, cube_arr[3, 4] + 1, 0 }, new Vector3(-1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0, 0 }, new Vector3(0, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, 0, 0,  cube_arr[1, 4] + 1  }, new Vector3(1, 0, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 3] + 1, cube_arr[3, 5] + 1, 0 }, new Vector3(-1, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 4] + 1, 0, 0 }, new Vector3(0, -1, 0)),
            CreateCube(new int[] { 0, 0, 0, cube_arr[4, 5] + 1, 0,  cube_arr[1, 5] + 1  }, new Vector3(1, -1, 0)),
            CreateCube(new int[] { 0, cube_arr[2, 0] + 1, cube_arr[5, 6] + 1, 0, cube_arr[3, 6] + 1, 0 }, new Vector3(-1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 1] + 1, cube_arr[5, 7] + 1, 0, 0, 0 }, new Vector3(0, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 2] + 1, cube_arr[5, 8] + 1, 0, 0, cube_arr[1, 6] + 1  }, new Vector3(1, 1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 3] + 1, 0, 0, cube_arr[3, 7] + 1, 0 }, new Vector3(-1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 4] + 1, 0, 0, 0, 0 }, new Vector3(0, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 5] + 1, 0, 0, 0,cube_arr[1, 7] + 1  }, new Vector3(1, 0, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 6] + 1, 0, cube_arr[4, 6] + 1, cube_arr[3, 8] + 1, 0 }, new Vector3(-1, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 7] + 1, 0, cube_arr[4, 7] + 1, 0, 0 }, new Vector3(0, -1, 1)),
            CreateCube(new int[] { 0, cube_arr[2, 8] + 1, 0, cube_arr[4, 8] + 1, 0, cube_arr[1, 8] + 1  }, new Vector3(1, -1, 1)),

        };

        indexMapping = new int[27];
        cubeComs = new Cube[27];
        savedPositions = new Vector3[27];
        for (int i = 0; i < 27; ++i)
        {
            indexMapping[i] = i;
            cubeComs[i] = cubes[i].GetComponent<Cube>();

            savedPositions[i] = cubes[i].localPosition;
            cubeComs[i].SetColor(new Color(0.3f, 0.3f, 0.3f, 1));
        }

        root = new GameObject("root").transform;
        root.parent = transform;
        root.localPosition = Vector3.zero;

        // Cub_reset2();


        count = 0;
        if (countText)
        {
            countText.text = count.ToString();
        }

        
        for (int i = 0; i < 27; ++i)
        {
            indexMapping[i] = i;
            cubes[i].localPosition = savedPositions[i];
            cubes[i].localRotation = Quaternion.identity;
        }

        HilightGroup();
    
    }
    IEnumerator Move(int group, bool clockwise, bool record, bool animated)
    {

        if (!moveLock)
        {
            moveLock = true;

            int[] groupIndexs = null;
            switch (group)
            {// 0, 1, 2, 3, 4, 5, 6, 7, 8
                //3 4 5 14 23  22 13 12 
                case 0: groupIndexs = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; break;
                case 1: groupIndexs = new int[] { 20, 19, 18, 23, 22, 21, 26, 25, 24 }; break;
                case 2: groupIndexs = new int[] { 18, 19, 20, 9, 10, 11, 0, 1, 2 }; break;
                case 3: groupIndexs = new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 }; break;
                case 4: groupIndexs = new int[] { 18, 9, 0, 21, 12, 3, 24, 15, 6 }; break;
                case 5: groupIndexs = new int[] { 2, 11, 20, 5, 14, 23, 8, 17, 26 }; break;
                // 3, 4, 5, 12,13,14,21,23,22
                //   case 6: groupIndexs = new int[] { 3, 4, 5, 12,13,14,21,22,23};  break;
                case 6: groupIndexs = new int[] { 5, 4, 3, 14, 13, 12, 23, 22, 21 }; break;

            }


            int[] newIndexs = null;
            if (clockwise == true)
            {
                newIndexs = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
                switch (group)
                {
                    case 0:
                        collor_arrrot(0, 5, 3, 4, 1, 0, 0, 0);
                        break;
                    case 5:
                        collor_arrrot(1, 5, 0, 4, 2, 2, 9, 3);

                        break;
                    case 1:
                        collor_arrrot(2, 5, 3, 4, 1, 0, 0, 0);

                        break;
                    case 4:
                        collor_arrrot(3, 5, 2, 4, 0, 0, 7, 3);
                        break;
                    case 3:
                        collor_arrrot(4, 0, 1, 2, 3, 6, 9, 1);

                        break;
                    case 2:
                        collor_arrrot(5, 0, 3, 2, 1, 0, 3, 1);

                        break;

                    case 6:
                        collor_arrrot(6, 0, 1, 2, 3, 0, 0, 0);

                        break;
                }
            }
            else if (clockwise == false)
            {
                newIndexs = new int[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 };
                switch (group)
                {
                    case 0:
                        collor_arrlef(0, 5, 1, 4, 3, 0, 0, 0);
                        break;
                    case 5:
                        collor_arrlef(1, 5, 2, 4, 0, 2, 9, 3);

                        break;
                    case 1:
                        collor_arrlef(2, 5, 1, 4, 3, 0, 0, 0);

                        break;
                    case 4:
                        collor_arrlef(3, 5, 0, 4, 2, 0, 7, 3);
                        break;
                    case 3:
                        collor_arrlef(4, 0, 1, 2, 3, 6, 9, 1);

                        break;
                    case 2:
                        collor_arrlef(5, 0, 3, 2, 1, 0, 3, 1);

                        break;
                    case 6:
                        collor_arrlef(6, 0, 1, 2, 3, 0, 0, 0);
                        break;
                }
            }

            int[] savedIndexs = new int[9];
            for (int i = 0; i < 9; ++i)
            {
                savedIndexs[i] = indexMapping[groupIndexs[i]];
            }

            for (int i = 0; i < 9; ++i)
            {
                indexMapping[groupIndexs[i]] = savedIndexs[newIndexs[i]];
                cubes[savedIndexs[i]].parent = root;
            }

            if (animated && rotateClip)
            {
                AudioSource.PlayClipAtPoint(rotateClip, transform.position);
            }









            float time = speed;
            float current = 0;
            Vector3 srcAngles = root.localRotation.eulerAngles;
            Vector3 destAngles = srcAngles;

            switch (group)
            {
                case 0: destAngles.z += (clockwise ? -90 : 90); break;
                case 1: destAngles.z += (clockwise ? 90 : -90); break;
                case 2: destAngles.y += (clockwise ? 90 : -90); break;
                case 3: destAngles.y += (clockwise ? -90 : 90); break;
                case 4: destAngles.x += (clockwise ? -90 : 90); break;
                case 5: destAngles.x += (clockwise ? 90 : -90); break;
                case 6: destAngles.y += (clockwise ? 90 : -90); break;

            }






                    GameObject tmp;
        tmp = GameObject.Find("cube_view");

        tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + group);
            if (clockwise==false)
            countText2.text = "LEFT";
            else
            countText2.text = "RIGHT";
            while (animated && current < time)
            {
                current += Time.deltaTime;
                root.localRotation = Quaternion.Euler(Vector3.Lerp(srcAngles, destAngles, current / time));
                yield return new WaitForEndOfFrame();

            }

            root.localRotation = Quaternion.Euler(destAngles);


            for (int i = root.childCount - 1; i >= 0; --i)
            {
                root.GetChild(i).parent = transform;
            }
            root.localRotation = Quaternion.identity;

            if (record)
            {
                history.Add((clockwise ? 10 : 0) + group);
            }












        
            if (animated)
            {

                count++;
                Debug.Log(countText.text);
                if (countText)
                {
                    countText.text = count.ToString();
                }
            }

            moveLock = false;
        }
        //   findans();

    }

    void collor_arrrot(int main, int sec, int thr, int forr, int fif, int start, int end, int interval)
    {

        int[,] myArray = new int[3, 3];
        int[,] myArray2 = new int[3, 3];

        int[] tmp = new int[9];
        int[] tmp2 = new int[9];
        int[] tmp3 = new int[9];
        int[] tmp4 = new int[9];
        if (main == 0 || main == 1 || main == 3 || main == 4 || main == 5 || main == 2 || main == 6)
        {
            for (int i = 0; i < 9; i++)
                tmp[i] = cube_arr[sec, i];
            for (int i = 0; i < 9; i++)
                tmp2[i] = cube_arr[thr, i];
            for (int i = 0; i < 9; i++)
                tmp3[i] = cube_arr[forr, i];
            for (int i = 0; i < 9; i++)
                tmp4[i] = cube_arr[fif, i];
        }
        int count = 0;
        if (main == 0 || main == 4)
        {
            count = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count++;
                }
            rot90(myArray, myArray2, 3, 3);


            count = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];

                    count++;
                }
             //   Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");

            }
        }
        if (main == 3)
        {

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 6 + i;
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count -= 3;

                }
                count = 0;
            }

            rot90(myArray, myArray2, 3, 3);

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 6 + i;
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];
                    count -= 3;
                }
            //    Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");
                count = 0;
            }
        }

        if (main == 2 || main == 1 || main == 5)
        {

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 2 + (i * 3);
              //  Debug.Log(count);
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count--;

                }
                count = 0;
            }


            rot90(myArray, myArray2, 3, 3);


            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 2 + (i * 3);
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];

                    count--;
                }
              //  Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");
                count = 0;
            }
        }





        if (main == 0)
        {/*
            cube_arr[fif, j] = tmp[i];
            cube_arr[sec, i] = tmp3[j];
            cube_arr[thr, i] = tmp4[j];
            cube_arr[forr, j] = tmp2[i];*/


            //5341
            //1234

            for (int i = 0, j = 2; i < 3; i++, j--)
                cube_arr[sec, i] = tmp2[j];

            for (int i = 0; i < 3; i++)
                cube_arr[thr, i] = tmp3[i];

            for (int i = 2, j = 0; i >= 0; i--, j++)
                cube_arr[forr, j] = tmp4[i];

            for (int i = 2; i >= 0; i--)
                cube_arr[fif, i] = tmp[i];
        }
        else if (main == 1)
        {
            //5042
            //1234
            for (int i = 2, j = 8; i < 9; i += 3, j -= 3)
                cube_arr[sec, i] = tmp2[j];

            for (int i = 2; i < 9; i += 3)
                cube_arr[thr, i] = tmp3[i];

            for (int i = 2, j = 8; i < 9; i += 3, j -= 3)
                cube_arr[forr, i] = tmp4[j];

            for (int i = 2; i < 9; i += 3)
                cube_arr[fif, i] = tmp[i];
        }
        else if (main == 2)
        {
            //5341
            //1234
       
            for (int i = 6; i < 9; i++)
                cube_arr[sec, i] = tmp4[i];

            for (int i = 6, j = 8; i < 9; i++, j--)
                cube_arr[thr, i] = tmp[j];

            for (int i = 6; i < 9; i++)
                cube_arr[forr, i] = tmp2[i];

            for (int i = 6, j = 8; i < 9; i++, j--)
                cube_arr[fif, i] = tmp3[j];



        }

        else if (main == 3)
        {
            //5240
            //1234

            for (int i = 0; i < 7; i += 3)
                cube_arr[sec, i] = tmp2[i];

            for (int i = 0, j = 6; i < 7; i += 3, j -= 3)
                cube_arr[thr, i] = tmp3[j];

            for (int i = 0; i < 7; i += 3)
                cube_arr[forr, i] = tmp4[i];

            for (int i = 0, j = 6; i < 7; i += 3, j -= 3)
                cube_arr[fif, j] = tmp[i];

        }
        else if (main == 4)
        {
            //0123
            //1234
            for (int i = 8, j = 2; i >= 6; i--, j += 3)
                cube_arr[sec, i] = tmp4[j];

            for (int i = 8, j = 8; i >= 2; i -= 3, j--)
                cube_arr[thr, i] = tmp[j];

            for (int i = 6, j = 8; i < 9; i++, j -= 3)
                cube_arr[forr, i] = tmp2[j];

            for (int i = 2, j = 6; i < 9; i += 3, j++)
                cube_arr[fif, i] = tmp3[j];

        }
        else if (main == 5)
        {
            //0321
            //1234
            for (int i = 0, j = 0; i < 3; i++, j += 3)
                cube_arr[sec, i] = tmp4[j];
            for (int i = 0, j = 2; i < 7; i += 3, j--)
                cube_arr[thr, i] = tmp[j];
            for (int i = 2, j = 6; i >= 0; i--, j -= 3)
                cube_arr[forr, i] = tmp2[j];
            for (int i = 6, j = 0; i >= 0; i -= 3, j++)
                cube_arr[fif, i] = tmp3[j];

        }
        else if (main == 6)
        {
            //0123
            //1234
            for (int i = 3, j = 1; i < 6; i++, j += 3)
                cube_arr[sec, i] = tmp2[j];
            for (int i = 1, j = 5; i < 8; i += 3, j--)
                cube_arr[thr, i] = tmp3[j];
            for (int i = 5, j = 7; i >= 3; i--, j -= 3)
                cube_arr[forr, i] = tmp4[j];
            for (int i = 7, j = 3; i >= 1; i -= 3, j++)
                cube_arr[fif, i] = tmp[j];

        }


        /*
        //Debug.Log(tmpstr);
        Debug.Log("關聯面\n");
        string tmpstr2 = "";
        if (main == 0)
        {
            //5341
            //1234
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[fif, i];
        }

        if (main == 1)
        {
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[sec, i];

            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[forr, i];

            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 2)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[fif, i];


        }
        if (main == 3)
        {
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 4)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        if (main == 5)
        {
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 6)
        {
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        Debug.Log(tmpstr2);
        */
    }

    void collor_arrlef(int main, int sec, int thr, int forr, int fif, int start, int end, int interval)
    {


        int[,] myArray = new int[3, 3];
        int[,] myArray2 = new int[3, 3];

        int[] tmp = new int[9];
        int[] tmp2 = new int[9];
        int[] tmp3 = new int[9];
        int[] tmp4 = new int[9];
        if (main == 0 || main == 1 || main == 3 || main == 4 || main == 5 || main == 2 || main == 6)
        {
            for (int i = 0; i < 9; i++)
                tmp[i] = cube_arr[sec, i];
            for (int i = 0; i < 9; i++)
                tmp2[i] = cube_arr[thr, i];
            for (int i = 0; i < 9; i++)
                tmp3[i] = cube_arr[forr, i];
            for (int i = 0; i < 9; i++)
                tmp4[i] = cube_arr[fif, i];
        }

        int count = 0;
        if (main == 0 || main == 4)
        {
            count = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count++;
                }
            lef90(myArray, myArray2, 3, 3);


            count = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];

                    count++;
                }
              //  Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");

            }
        }
        if (main == 3)
        {

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 6 + i;
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count -= 3;

                }
                count = 0;
            }

            lef90(myArray, myArray2, 3, 3);

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 6 + i;
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];
                    count -= 3;
                }
            //    Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");
                count = 0;
            }
        }

        if (main == 2 || main == 1 || main == 5)
        {

            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 2 + (i * 3);
                //Debug.Log(count);
                for (int j = 0; j < 3; j++)
                {
                    myArray[i, j] = cube_arr[main, count];
                    count--;

                }
                count = 0;
            }


            lef90(myArray, myArray2, 3, 3);


            count = 0;

            for (int i = 0; i < 3; i++)
            {
                count = 2 + (i * 3);
                for (int j = 0; j < 3; j++)
                {
                    cube_arr[main, count] = myArray2[i, j];

                    count--;
                }
             //   Debug.Log(myArray2[i, 0] + "" + myArray2[i, 1] + "" + myArray2[i, 2] + "\n");
                count = 0;
            }
        }






        if (main == 0)
        {/*
            cube_arr[fif, j] = tmp[i];
            cube_arr[sec, i] = tmp3[j];
            cube_arr[thr, i] = tmp4[j];
            cube_arr[forr, j] = tmp2[i];*/
            //5341
            //5143
            //1234
            for (int i = 0; i < 3; i++)
                cube_arr[sec, i] = tmp2[i];

            for (int i = 2, j = 0; i >= 0; i--, j++)
                cube_arr[thr, j] = tmp3[i];

            for (int i = 0; i < 3; i++)
                cube_arr[forr, i] = tmp4[i];

            for (int i = 2, j = 0; i >= 0; i--, j++)
                cube_arr[fif, i] = tmp[j];

        }




        else if (main == 1)
        {
            //5042
            //5240
            //1234
            for (int i = 2; i < 9; i += 3)
                cube_arr[sec, i] = tmp2[i];

            for (int i = 2, j = 8; i < 9; i += 3, j -= 3)
                cube_arr[thr, i] = tmp3[j];

            for (int i = 2; i < 9; i += 3)
                cube_arr[forr, i] = tmp4[i];

            for (int i = 2, j = 8; i < 9; i += 3, j -= 3)
                cube_arr[fif, j] = tmp[i];
        }
        else if (main == 2)
        {
            //5341
            //5143
            //1234
           // Debug.Log("test");
            for (int i = 6, j = 8; i < 9; i++, j--)
                cube_arr[sec, i] = tmp4[j];

            for (int i = 6; i < 9; i++)
                cube_arr[thr, i] = tmp[i];

            for (int i = 6, j = 8; i < 9; i++, j--)
                cube_arr[forr, j] = tmp2[i];

            for (int i = 6; i < 9; i++)
                cube_arr[fif, i] = tmp3[i];



        }

        else if (main == 3)
        {
            //5240
            //5042
            //1234

            for (int i = 0, j = 6; i < 7; i += 3, j -= 3)
                cube_arr[sec, j] = tmp2[i];

            for (int i = 0; i < 7; i += 3)
                cube_arr[thr, i] = tmp3[i];


            for (int i = 0, j = 6; i < 7; i += 3, j -= 3)
                cube_arr[forr, i] = tmp4[j];


            for (int i = 0; i < 7; i += 3)
                cube_arr[fif, i] = tmp[i];

        }
        else if (main == 4)
        {
            //0123
            //1234
            for (int i = 8, j = 8; i >= 6; i--, j -= 3)
                cube_arr[sec, i] = tmp2[j];

            for (int i = 8, j = 6; i >= 2; i -= 3, j++)
                cube_arr[thr, i] = tmp3[j];

            for (int i = 6, j = 2; i < 9; i++, j += 3)
                cube_arr[forr, i] = tmp4[j];

            for (int i = 2, j = 8; i < 9; i += 3, j--)
                cube_arr[fif, i] = tmp[j];

        }
        else if (main == 5)
        {
            //0321
            //1234
            for (int i = 0, j = 6; i < 3; i++, j -= 3)
                cube_arr[sec, i] = tmp2[j];
            for (int i = 0, j = 0; i < 7; i += 3, j++)
                cube_arr[thr, i] = tmp3[j];
            for (int i = 0, j = 6; i < 3; i++, j -= 3)
                cube_arr[forr, i] = tmp4[j];
            for (int i = 6, j = 2; i >= 0; i -= 3, j--)
                cube_arr[fif, i] = tmp[j];



        }
        else if (main == 6)
        {
            //0321
            //1234
            for (int i = 5, j = 1; i >= 3; i--, j += 3)
                cube_arr[sec, i] = tmp4[j];

            for (int i = 7, j = 5; i >= 1; i -= 3, j--)
                cube_arr[thr, i] = tmp[j];

            for (int i = 3, j = 7; i < 6; i++, j -= 3)
                cube_arr[forr, i] = tmp2[j];
            for (int i = 1, j = 3; i < 8; i += 3, j++)
                cube_arr[fif, i] = tmp3[j];

        }


        /*
        //Debug.Log(tmpstr);
        Debug.Log("關聯面\n");
        string tmpstr2 = "";
        if (main == 0)
        {
            //5341
            //1234
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[fif, i];
        }

        else if (main == 1)
        {
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[sec, i];

            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[forr, i];

            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 2)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[fif, i];


        }
        else if (main == 3)
        {
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 4)
        {
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 6; i < 9; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 2; i < 9; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 5)
        {
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 0; i < 3; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 0; i < 7; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }
        else if (main == 6)
        {
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[sec, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[thr, i];
            for (int i = 3; i < 6; i++)
                tmpstr2 += cube_arr[forr, i];
            for (int i = 1; i < 8; i += 3)
                tmpstr2 += cube_arr[fif, i];
        }

        Debug.Log(tmpstr2);*/
    }







    void rot90(int[,] in_ary, int[,] out_ary, int h, int w){
        int p = 0;
        for (int i = h - 1; i >= 0; i--){
            for (int j = 0; j < w; j++)
                out_ary[j, p] = in_ary[i, j];
            p++;
        }//從最後一行讀回來
    }

    void lef90(int[,] in_ary, int[,] out_ary, int h, int w){
        int p = h - 1;
        for (int i = h - 1; i >= 0; i--){
            for (int j = 0; j < h; j++)
                out_ary[j, p] = in_ary[i, (h - 1) - j]; ;
            p--;
        }//從最後一行讀回來
    }






    public void HilightGroup()
    {
  //      Debug.Log("now" + groupIndex + "\n");
        int[] groupIndexs = null;
        int[] groupIndexs2 = null;
        int[] groupIndexs3= null;
        int[] groupIndexs4 = null;
        switch (groupIndex)
        {
            case 0: groupIndexs = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; break;
            case 1: groupIndexs = new int[] { 20, 19, 18, 23, 22, 21, 26, 25, 24 }; break;
            case 2: groupIndexs = new int[] { 18, 19, 20, 9, 10, 11, 0, 1, 2 }; break;
            case 3: groupIndexs = new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 }; break;
            case 4: groupIndexs = new int[] { 18, 9, 0, 21, 12, 3, 24, 15, 6 }; break;
            case 5: groupIndexs = new int[] { 2, 11, 20, 5, 14, 23, 8, 17, 26 }; break;
            case 6: groupIndexs = new int[] { 5, 4, 3, 14, 13, 12, 23, 22, 21 }; break;
        }

        for (int i = 0; i < 27; ++i)
        {
            cubeComs[i].SetColor(new Color(0.3f, 0.3f, 0.3f, 1));
        }
        /*
        for (int i=0;i< groupIndexs.Length;i++)
            cubeComs[groupIndexs[i]].SetColor(Color.white);
            */
       groupIndexs2 = new int[] { 18, 19, 20, 9, 10, 11, 0, 1, 2 };
        groupIndexs4 = new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 }; 
          
        groupIndexs3 = new int[] { 5, 4, 3, 14, 13, 12, 23, 22, 21 };
        for (int i = 0; i < groupIndexs.Length; i++)
            cubeComs[groupIndexs2[i]].SetColor(Color.white);
        for (int i=0;i< groupIndexs.Length;i++)
            cubeComs[groupIndexs3[i]].SetColor(Color.white);
        for (int i = 0; i < groupIndexs.Length; i++)
            cubeComs[groupIndexs4[i]].SetColor(Color.white);

    }
    public void location()
    {

        Quaternion quate = Quaternion.identity;
        quate.eulerAngles = new Vector3(0, 0, 0);
        GameObject test;
        test = GameObject.Find("MagicCube");
        test.transform.rotation = quate;
    }
    public int findblock4(int color, int color2, int color3)
    {

        if (checker2(0, 2, 1, 0, 5, 2, color, color2, color3) == 1)
        {
            cube_angle_trun = true;
            cube_angle_face = 0;

            return 3;
        }
        if (checker2(0, 0, 3, 0, 5, 0, color, color2, color3) == 1)
        {
            cube_angle_trun = false;
            cube_angle_face = 0;

            return 3;
        }
        if (checker2(0, 8, 1, 2, 4, 2, color, color2, color3) == 1)
        {
            cube_angle_trun = true;
            cube_angle_face = 0;

            return 1;
        }

        if (checker2(0, 6, 3, 2, 4, 0, color, color2, color3) == 1)
        {
            cube_angle_trun = false;
            cube_angle_face = 0;

            return 1;
        }

        if (checker2(2, 2, 1, 6, 5, 8, color, color2, color3) == 1)
        {
            cube_angle_trun = false;
            cube_angle_face = 2;

            return 3;
        }
        if (checker2(2, 0, 3, 6, 5, 6, color, color2, color3) == 1)
        {
            cube_angle_trun = true;
            cube_angle_face = 2;

            return 3;
        }
        if (checker2(2, 8, 1, 8, 4, 8, color, color2, color3) == 1)
        {
            cube_angle_trun = false;
            cube_angle_face = 2;

            return 1;
        }
        if (checker2(2, 6, 3, 8, 4, 6, color, color2, color3) == 1)
        {
            cube_angle_trun = true;
            cube_angle_face = 2;

            return 1;
        }
        return -1;
    }

    public int findblock3(int color, int color2, int face)
    {
     //   Debug.Log("幾面" + groupIndex);



        if (checker(0, 1, 5, 1, color, color2, 0) == 1) { return 3; }
        if (checker(0, 3, 3, 1, color, color2, 0) == 1) { return 2; }
        if (checker(0, 7, 4, 1, color, color2, 0) == 1) { return 1; }
        if (checker(0, 5, 1, 1, color, color2, 0) == 1) { return 2; }

        if (checker(1, 1, 0, 5, color, color2, 0) == 1) { return 2; }
        if (checker(1, 3, 5, 5, color, color2, 0) == 1) { return 3; }
        if (checker(1, 5, 4, 5, color, color2, 0) == 1) { return 1; }
        if (checker(1, 7, 2, 5, color, color2, 0) == 1) { return 2; }

        if (checker(2, 1, 5, 7, color, color2, 0) == 1) { return 3; }
        if (checker(2, 5, 1, 7, color, color2, 0) == 1) { return 2; }
        if (checker(2, 3, 3, 7, color, color2, 0) == 1) { return 2; }
        if (checker(2, 7, 4, 7, color, color2, 0) == 1) { return 1; }

        if (checker(3, 1, 0, 3, color, color2, 0) == 1) { return 2; }
        if (checker(3, 3, 5, 3, color, color2, 0) == 1) { return 3; }
        if (checker(3, 5, 4, 3, color, color2, 0) == 1) { return 1; }
        if (checker(3, 7, 2, 3, color, color2, 0) == 1) { return 2; }



        if (checker(4, 1, 0, 7, color, color2, 0) == 1) { return 1; }
        if (checker(4, 3, 3, 5, color, color2, 0) == 1) { return 1; }
        if (checker(4, 5, 1, 5, color, color2, 0) == 1) { return 1; }
        if (checker(4, 7, 2, 7, color, color2, 0) == 1) { return 1; }

        if (checker(5, 1, 0, 1, color, color2, 0) == 1) { return 3; }
        if (checker(5, 3, 3, 3, color, color2, 0) == 1) { return 3; }
        if (checker(5, 5, 1, 3, color, color2, 0) == 1) { return 3; }
        if (checker(5, 7, 2, 1, color, color2, 0) == 1) { return 3; }

        return -1;
    }
    public int findblock2(int color, int color2)
    {
        int face = -1;
        int[,] cube_block = new int[12, 5] {
         {  2 ,cube_arr[0, 5], cube_arr[1, 1],0,1},
         {  2 ,cube_arr[0, 3], cube_arr[3, 1],0,3},
         {  2 ,cube_arr[2, 3], cube_arr[3, 7],2,3},
         {  2 ,cube_arr[2, 5], cube_arr[1, 7],2,1},

         {  3 ,cube_arr[5, 1], cube_arr[0, 1],5,0},
         {  3 ,cube_arr[5, 5], cube_arr[1, 3],5,1},
         {  3 ,cube_arr[5, 7], cube_arr[2, 1],5,2},
         {  3 ,cube_arr[5, 3], cube_arr[3, 7],5,3},

         {  1 ,cube_arr[4, 1], cube_arr[0, 7],4,0},
         {  1 ,cube_arr[4, 5], cube_arr[1, 5],4,1},
         {  1 ,cube_arr[4, 7], cube_arr[2, 7],4,2},
         {  1 ,cube_arr[4, 3], cube_arr[3, 5],4,3}

        };
        for (int i = 0; i < 12; i++)
        {


            if (cube_block[i, 1] == color)

                for (int j = 0; j < 12; j++)
                {
                    if (cube_block[j, 2] == color2)
                        return cube_block[i, 0];
                }


            if (cube_block[i, 1] == color2)

                for (int j = 0; j < 12; j++)
                    if (cube_block[j, 2] == color)

                        return cube_block[i, 0];

        }




        return face;
    }
    public int findblock(int sec, int color, int color2)
    {

        int face = -1;
        for (int i = 1; i <= 7; i += 3)
        {
            for (int j = 0; j < 6; j++)
            {
                if (j == color)
                    continue;
                if (cube_arr[color, i] == color && cube_arr[j, sec] == color2)
                {
                    color_true = true;
                    Debug.Log(color + " " + i + ":" + j + "," + sec);
                    if (color == 1 && (j == 5))
                        return 3;
                    if (color == 1 && (j == 2 || j == 0))
                        return 2;
                    if (color == 1 && (j == 4))
                        return 1;


                }
                else if (cube_arr[color2, i] == color2 && cube_arr[j, sec] == color)
                {

                    color_true = false;
                    Debug.Log(color + " " + j + ":" + i + "," + sec);
                    if (i == 5)
                        return 3;
                    if (i == 2 || i == 3 || i == 0 || i == 1)
                        return 2;
                    if (i == 4)
                        return 1;
                }

            }
        }
        return -1;
        /*   for (int i = 0; i < 12; i++)
           {
               Debug.Log(cube_block[i, 1] + "," + cube_block[i, 2]);
           }*/

    }
    public int checker(int tmp, int tmp2, int tmp3, int tmp4, int color, int color2, int ex)
    {
        if (cube_arr[tmp, tmp2] == color && cube_arr[tmp3, tmp4] == color2)
        {
            color_true = true;
            if (ex > 0)
                return tmp;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color2 && cube_arr[tmp3, tmp4] == color)
        {
            color_true = false;
            if (ex > 0)
                return tmp;
            return 1;
        }
        else
            return 0;
    }
    public int checker2(int tmp, int tmp2, int tmp3, int tmp4, int tmp5, int tmp6, int color, int color2, int color3)
    {
      //  Debug.Log(cube_arr[tmp, tmp2] + "" + cube_arr[tmp3, tmp4] + "" + cube_arr[tmp5, tmp6]);
        if (cube_arr[tmp, tmp2] == color && cube_arr[tmp3, tmp4] == color2 && cube_arr[tmp5, tmp6] == color3)
        {
            cube_angle_colortrue = true;

            return 1;
        }
        if (cube_arr[tmp, tmp2] == color && cube_arr[tmp3, tmp4] == color3 && cube_arr[tmp5, tmp6] == color2)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color2 && cube_arr[tmp3, tmp4] == color3 && cube_arr[tmp5, tmp6] == color)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color2 && cube_arr[tmp3, tmp4] == color && cube_arr[tmp5, tmp6] == color3)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color3 && cube_arr[tmp3, tmp4] == color && cube_arr[tmp5, tmp6] == color2)
        {
            cube_angle_colortrue = false;
            return 1;
        }
        if (cube_arr[tmp, tmp2] == color3 && cube_arr[tmp3, tmp4] == color2 && cube_arr[tmp5, tmp6] == color)
        {
            cube_angle_colortrue = false;
            return 1;
        }


        else
        {
      
            return 0;
        }
    }

    public int find_blockwho(int color, int color2)
    {
        if (checker(0, 5, 1, 1, color, color2, 0) == 1)
            return 5;

        if (checker(1, 7, 2, 5, color, color2, 0) == 1)
            return 1;

        if (checker(2, 3, 3, 7, color, color2, 0) == 1)
            return 4;

        if (checker(0, 3, 3, 1, color, color2, 0) == 1)
            return 0;

        return -1;
    }
    public void cross(int face, int color, int color2)
    {
        if (face == 1)
        {
        }
    }
    public void first()
    {
        //不管顏色正確 先把目標面轉到 主要面
        if (findblock3(0, 5, -1) == 3 && (checker(0, 1, 5, 1, 0, 5, 0)) == 1 && color_true == true)
            return;
        if (findblock3(0, 5, -1) == 1)
        {
            while ((checker(0, 7, 4, 1, 0, 5, 0)) == 0)
                StartCoroutine(Move(3, true, true, false));
            if ((checker(0, 7, 4, 1, 0, 5, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                StartCoroutine(Move(5, true, true, false));
            }
            else if ((checker(0, 7, 4, 1, 0, 5, 0)) == 1 && color_true == true)
            {
                while ((checker(0, 1, 5, 1, 0, 5, 0)) == 0)
                    StartCoroutine(Move(0, true, true, false));
            }
        }
        if (findblock3(0, 5, -1) == 2)
        {
            StartCoroutine(Move(find_blockwho(0, 5), false, true, false));

        }
        if (findblock3(0, 5, -1) == 3)
        {
            while ((checker(0, 1, 5, 1, 0, 5, 0)) == 0)
                StartCoroutine(Move(2, true, true, false));
            if ((checker(0, 1, 5, 1, 0, 5, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(0, true, true, false));

            }
        }
        first();
    }
    public bool Cross(int first, int sec, int first_arrnumber, int first_arrnumber_2
        , int sec_arrnumber, int under, int face, int next)
    {      //不管顏色正確 先把目標面轉到 主要面
        if (findblock3(first, sec, -1) == 3 && (checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 1 && color_true == true)
            return true;
        if (findblock3(first, sec, -1) == 1)
        {
            while ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 0)
            {
                StartCoroutine(Move(3, true, true, false));
                ans_count++;
            }
            if ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(face, false, true, false));
                StartCoroutine(Move(next, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(next, true, true, false));

                ans_count += 4;
            }
            else if ((checker(first, first_arrnumber_2, 4, under, first, sec, 0)) == 1 && color_true == true)
            {
                while ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 0)
                {
                    StartCoroutine(Move(face, true, true, false));
                    ans_count++;
                }
            }
        }
        if (findblock3(first, sec, -1) == 2)
        {
            int tmp = find_blockwho(first, sec);
            StartCoroutine(Move(tmp, false, true, false));
            StartCoroutine(Move(3, true, true, false));
            StartCoroutine(Move(tmp, true, true, false));
            ans_count += 3;
        }
        if (findblock3(first, sec, -1) == 3)
        {
            while ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 0)
            {
                StartCoroutine(Move(2, true, true, false));
                ans_count++;
            }
            if ((checker(first, first_arrnumber, sec, sec_arrnumber, first, sec, 0)) == 1 && color_true == false)
            {

                StartCoroutine(Move(face, false, true, false));

                ans_count++;
            }
        }

        Cross(first, sec, first_arrnumber, first_arrnumber_2,
            sec_arrnumber, under, face, next);
        return false;
    }

    public void sceand()

    {//不管顏色正確 先把目標面轉到 主要面
        if (findblock3(1, 5, -1) == 3 && (checker(1, 3, 5, 5, 1, 5, 0)) == 1 && color_true == true)
            return;
        if (findblock3(1, 5, -1) == 1)
        {
            while ((checker(1, 5, 4, 5, 1, 5, 0)) == 0)
                StartCoroutine(Move(3, true, true, false));
            if ((checker(1, 5, 4, 5, 1, 5, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(1, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                StartCoroutine(Move(1, true, true, false));
            }
            else if ((checker(1, 5, 4, 5, 1, 5, 0)) == 1 && color_true == true)
            {
                while ((checker(1, 3, 5, 5, 1, 5, 0)) == 0)
                    StartCoroutine(Move(5, true, true, false));
            }
        }
        if (findblock3(1, 5, -1) == 2)
        {
            StartCoroutine(Move(find_blockwho(1, 5), false, true, false));

        }
        if (findblock3(1, 5, -1) == 3)
        {
            while ((checker(1, 3, 5, 5, 1, 5, 0)) == 0)
                StartCoroutine(Move(2, true, true, false));
            if ((checker(1, 3, 5, 5, 1, 5, 0)) == 1 && color_true == false)
            {
                StartCoroutine(Move(5, true, true, false));

            }
        }
        sceand();
    }
    /*

        Cross(0, 5, 1, 7, 1, 1, 0, 5);
        Cross(1, 5, 3, 5, 5, 5, 5, 1);
        Cross(2, 5, 1, 7, 7, 7, 1, 4);
        Cross(3, 5, 3, 5, 3, 3, 4, 0);
        while (Cross(0, 5, 1, 7, 1, 1, 0, 5) == false) ;
        while (Cross(1, 5, 3, 5, 5, 5, 5, 1) == false) ;
        while (Cross(2, 5, 1, 7, 7, 7, 1, 4) == false) ;
        while (Cross(3, 5, 3, 5, 3, 3, 4, 0) == false) ;
        while (cube_arr[0, 4] != 0)
            StartCoroutine(Move(6, true, false, false));
        find_angle1(0, 1, 5);
        StartCoroutine(Move(2, false, true, false));
        find_angle1(3, 0, 5);
        StartCoroutine(Move(2, false, true, false));
        find_angle1(2, 3, 5);
        StartCoroutine(Move(2, false, true, false));
        find_angle1(1, 2, 5);
        StartCoroutine(Move(2, false, true, false));
     * */
    public bool find_angle1(int color, int color2, int color3)
    {
        int level = findblock4(color, color2, color3);

        if (level != -1)
        {

            if (checker2(0, 2, 1, 0, 5, 2, color, color2, color3) == 1 && level == 3 && cube_angle_colortrue == false)
            {
                while (true)
                {
                    if (checker2(0, 2, 1, 0, 5, 2, color, color2, color3) == 1 && cube_angle_colortrue == true)
                        return true;
                    StartCoroutine(Move(5, false, true, false));
                    StartCoroutine(Move(3, false, true, false));
                    StartCoroutine(Move(5, true, true, false));
                    StartCoroutine(Move(3, true, true, false));

                    StartCoroutine(Move(5, false, true, false));
                    StartCoroutine(Move(3, false, true, false));
                    StartCoroutine(Move(3, false, true, false));
                    StartCoroutine(Move(5, true, true, false));
                    StartCoroutine(Move(5, false, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(5, true, true, false));

                    ans_count += 11;
                    Debug.Log("ww");

                }
                return true;
            }


            if (level == 1 && (cube_angle_colortrue == false || cube_angle_colortrue == true))
            {
                while (checker2(0, 6, 3, 2, 4, 0, color, color2, color3) == 0)
                {
                    StartCoroutine(Move(3, true, true, false));
                    ans_count += 1;
                }
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                ans_count += 3;
                find_angle1(color, color2, color3);
            }

            if (level == 3 && cube_angle_face == 0)
            {
                if (cube_angle_trun == false)
                {
                    StartCoroutine(Move(4, true, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(4, false, true, false));
                    ans_count += 3;
                    find_angle1(color, color2, color3);
                }
            }
            else if (level == 3 && cube_angle_face == 2)
            {
                if (cube_angle_trun == false)
                {
                    StartCoroutine(Move(5, true, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(5, false, true, false));
                    ans_count += 3;
                }
                else if (cube_angle_trun == true)
                {
                    StartCoroutine(Move(4, false, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(4, true, true, false));
                    ans_count += 3;
                }
                find_angle1(color, color2, color3);
            }




        }
        //find_angle1(color, color2, color3);
        return false;
    }
    public void plus_check()
    {
        if (final_angle == 5)
            final_angle = 0;
        else
            final_angle++;
    }
    public bool find_angle2(int color, int color2, int color3)
    {
        if (final_angle == 6)
            final_angle = 0;
        if (findblock4(color, color2, color3) == 1)
        {
            while (checker2(0, 8, 1, 2, 4, 2, color, color2, color3) != 1)
            {
                StartCoroutine(Move(3, true, true, false));
                ans_count += 1;
                trun_number += 1;
            }
            StartCoroutine(Move(2, false, true, false));
            StartCoroutine(Move(0, false, true, false));
            StartCoroutine(Move(2, true, true, false));
            StartCoroutine(Move(0, true, true, false));
            ans_count += 4;
            plus_check();

            find_angle2(color, color2, color3);
        }

        if (findblock4(color, color2, color3) == 3)
        {
            while (trun_number != 0)
            {
                StartCoroutine(Move(3, false, true, false));
                ans_count += 1;
                trun_number--;
            }

            while (checker2(0, 8, 1, 2, 4, 2, color, color2, color3) == 0 || cube_angle_colortrue == false)
            {
                StartCoroutine(Move(2, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(2, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                ans_count += 4;
                plus_check();



            }
        }

        return false;
    }
    void show_all()
    {
        //5341
        //1234
        string tmpstr2 = "";
      //  Debug.Log("0:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[0, i];
      //  Debug.Log(tmpstr2);
        tmpstr2 = "";
      //  Debug.Log("1:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[1, i];
       // Debug.Log(tmpstr2);
        tmpstr2 = "";
       // Debug.Log("2:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[2, i];
     //   Debug.Log(tmpstr2);
        tmpstr2 = "";
      //  Debug.Log("3:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[3, i];
      //  Debug.Log(tmpstr2);
        tmpstr2 = "";
      //  Debug.Log("4:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[4, i];
     //   Debug.Log(tmpstr2);
        tmpstr2 = "";
      //  Debug.Log("5:");
        for (int i = 0; i < 9; i++)
            tmpstr2 += cube_arr[5, i];
     //   Debug.Log(tmpstr2);
        tmpstr2 = "";



    }
    public bool sec_block_level2(int tmp, int tmp2, int tmp3, int tmp4, int color, int color2)
    {

        if (findblock3(color, color2, -1) == 2)
        {//checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) == 1

            if (checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) == 1 && color_true == true)
            {

                return true;

            }
            if (checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) == 1 && color_true == false)
            {
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));

                StartCoroutine(Move(0, true, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                ans_count += 7;
                sec_block_level2(tmp, tmp2, tmp3, tmp4, color, color2);

            }
            if (checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) != 1)
            {
                while (checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) != 1)
                {
                    ans_count += 1;
                    StartCoroutine(Move(6, true, true, false));
                }
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                ans_count += 3;
                sec_block_level2(tmp, tmp2, tmp3, tmp4, color, color2);
            }

        }
        if (findblock3(color, color2, -1) == 1)
        {//checker(tmp, tmp2, tmp3, tmp4, color, color2, 0) == 1
            while (checker(0, 7, 4, 1, color, color2, 0) != 1)
            {
                StartCoroutine(Move(3, true, true, false));
                ans_count += 1;
            }

            while (cube_arr[0, 4] != color)
            {
                StartCoroutine(Move(6, true, true, false));
                ans_count += 1;
            }
            StartCoroutine(Move(5, false, true, false));
            StartCoroutine(Move(3, true, true, false));
            StartCoroutine(Move(5, true, true, false));
            ans_count += 3;
            sec_block_level2(tmp, tmp2, tmp3, tmp4, color, color2);
        }
        return false;
    }
    public bool find_block3_level3(int color, int color2, int tmp)
    {
        if (findblock3(color, color2, -1) == 2)
        {
            if (color_true == false)
            {
                StartCoroutine(Move(0, true, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                ans_count += 3;
            }

            return true;
        }
        if (findblock3(color, color2, -1) == 1)
        {
            while (checker(0, 7, 4, 1, color, color2, 0) != 1)
            {
                StartCoroutine(Move(3, true, true, false));
                ans_count += 1;
            }
            if (color_true == false)
            {
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                ans_count += 3;
                while (checker(0, 7, 4, 1, final_level3[tmp + 1, 0], final_level3[tmp + 1, 1], 0) != 1)
                {
                    StartCoroutine(Move(3, true, true, false));
                    ans_count += 1;
                }

                StartCoroutine(Move(0, true, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                ans_count += 3;
                while (checker(0, 7, 4, 1, color, color2, 0) != 1)
                {
                    StartCoroutine(Move(3, true, true, false));
                    ans_count += 1;
                }
                if (color_true == false)
                    find_block3_level3(color, color2, tmp);
            }

        }
        return false;
    }
    public bool fix_block3_level3()
    {
        for (int i = 0; i < 4; i++)
            if (findblock3(final_level3[i, 0], final_level3[i, 1], -1) == 2)
            {
                int tmp = 0, tmp2 = 0;
                bool tmp3 = color_true;
                for (int j = 0; j < 4; j++)
                {
                    if (findblock3(final_level3[j, 0], final_level3[j, 1], -1) == 1)
                    {
                        tmp = final_level3[j, 0];
                        tmp2 = final_level3[j, 1];
                        break;
                    }
                }
               // Debug.Log(tmp);
               // Debug.Log(tmp2);
               // Debug.Log(color_true);
                if (tmp3 == true)
                {

                    while (checker(0, 7, 4, 1, tmp, tmp2, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }
                    StartCoroutine(Move(0, true, true, false));
                    ans_count += 1;
                    while (checker(0, 7, 4, 1, 0, 1, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }
                    ans_count += 1;
                    StartCoroutine(Move(0, false, true, false));

                }
                if (tmp3 == false)
                {
                    while (checker(1, 5, 4, 5, tmp, tmp2, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }


                    StartCoroutine(Move(5, false, true, false));
                    ans_count += 1;
                    while (checker(1, 5, 4, 5, 0, 1, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }
                    ans_count += 1;
                    StartCoroutine(Move(5, true, true, false));

                }
                return true;
            }
        return false;
    }
    public void fix2_block3_level3()
    {
        int tmp = 0, tmp2 = 0;
        int tmp3 = 0, tmp4 = 0;

        if ((findblock3(0, 1, -1) == 2) && color_true == false)
        {

            for (int i = 0; i < 4; i++)
                if (findblock3(final_level3[i, 0], final_level3[i, 1], -1) == 1 && color_true == true)
                {
                    while (checker(0, 7, 4, 1, final_level3[i, 0], final_level3[i, 1], 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }

                    break;
                }
            StartCoroutine(Move(0, true, true, false));
            ans_count += 1;
            for (int i = 0; i < 4; i++)
                if (findblock3(final_level3[i, 0], final_level3[i, 1], -1) == 1 && color_true == false)
                {

                    while (checker(0, 7, 4, 1, final_level3[i, 0], final_level3[i, 1], 0) != 1)
                    {

                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }
                    break;
                }
            StartCoroutine(Move(0, false, true, false));
            ans_count += 1;

            /////////////////////////
            for (int i = 0; i < 4; i++)
                if (findblock3(final_level3[i, 0], final_level3[i, 1], -1) == 1 && color_true == true)
                {
                    while (checker(1, 5, 4, 5, final_level3[i, 0], final_level3[i, 1], 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }

                    break;
                }
            StartCoroutine(Move(5, false, true, false));
            ans_count += 1;
            while (checker(1, 5, 4, 5, 0, 1, 0) != 1)
            {
                ans_count += 1;
                StartCoroutine(Move(3, true, true, false));
            }


            StartCoroutine(Move(5, true, true, false));
            ans_count += 1;

        }

    }
    public bool five_check()
    {
        int count = 0;

        StartCoroutine(Move(3, true, true, false));
        ans_count += 1;
        if (cube_arr[0, 7] == 0)
            count += 1;
        if (cube_arr[3, 5] == 3)
            count += 1;
        if (cube_arr[2, 7] == 2)
            count += 1;
        if (cube_arr[1, 5] == 1)
            count += 1;
        if (count == 4)
            return true;
        return false;
    }
    public int check_five()
    {
        int count = 0;
        while (true)
        {
            count = 0;
            StartCoroutine(Move(3, true, true, false));
            ans_count += 1;
            if (cube_arr[0, 7] == 0)
                count += 1;
            if (cube_arr[3, 5] == 3)
                count += 1;
            if (cube_arr[2, 7] == 2)
                count += 1;
            if (cube_arr[1, 5] == 1)
                count += 1;

            if (count >= 2)
                break;
        }

        ///一字解決方案
        ///

        if (five_check() == true)
            return 0;
        if (count >= 2)
        {

            if ((cube_arr[0, 7] == 0 && cube_arr[2, 7] == 2) || (cube_arr[0, 7] == 2 && cube_arr[2, 7] == 0) ||
            (cube_arr[1, 5] == 0 && cube_arr[3, 5] == 2) || (cube_arr[1, 5] == 2 && cube_arr[3, 5] == 0))
            {

                while (checker(0, 7, 4, 1, 0, 4, 0) != 1)
                {
                    ans_count += 1;
                    StartCoroutine(Move(3, true, true, false));
                }
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(5, true, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                ans_count += 7;
                check_five();
            }
            else if ((cube_arr[0, 7] == 1 && cube_arr[2, 7] == 3) || (cube_arr[0, 7] == 3 && cube_arr[2, 7] == 1) ||
                (cube_arr[1, 5] == 1 && cube_arr[3, 5] == 3) || (cube_arr[1, 5] == 3 && cube_arr[3, 5] == 1))

            {
                while (checker(0, 7, 4, 1, 1, 4, 0) != 1)
                {
                    ans_count += 1;
                    StartCoroutine(Move(3, true, true, false));
                }
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(5, true, true, false));
                StartCoroutine(Move(3, false, true, false));
                StartCoroutine(Move(5, false, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(3, true, true, false));
                StartCoroutine(Move(5, true, true, false));
                ans_count += 7;
                check_five();
            }
            else
            {
                int count2 = -1;
                int who = -1;
                while (true)
                {
                    count2 = 0;

                    if (cube_arr[0, 7] == 0)
                    {
                        count2 += 1;
                        who = 0;
                    }
                    if (cube_arr[3, 5] == 3)
                    {
                        count2 += 1;
                        who = 3;
                    }
                    if (cube_arr[2, 7] == 2)
                    {
                        count2 += 1;
                        who = 2;
                    }
                    if (cube_arr[1, 5] == 1)
                    {
                        count2 += 1;
                        who = 1;
                    }

                    if (count2 == 1)
                        break;

                    StartCoroutine(Move(3, true, true, false));
                    ans_count += 1;
                }

                int count3 = 0;

                count = 0;
                StartCoroutine(Move(3, true, true, false));
                ans_count += 1;
                if (cube_arr[0, 7] == 0)
                    count3 += 1;
                if (cube_arr[3, 5] == 3)
                    count3 += 1;
                if (cube_arr[2, 7] == 2)
                    count3 += 1;
                if (cube_arr[1, 5] == 1)
                    count3 += 1;
                Debug.Log(count3);
                for (int i = 0; i < 2; i++)
                {
                    while (checker(0, 7, 4, 1, who, 4, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }
                    StartCoroutine(Move(5, false, true, false));
                    StartCoroutine(Move(3, false, true, false));
                    StartCoroutine(Move(5, true, true, false));
                    StartCoroutine(Move(3, false, true, false));
                    StartCoroutine(Move(5, false, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(3, true, true, false));
                    StartCoroutine(Move(5, true, true, false));
                    ans_count += 8;
                    while (checker(0, 7, 4, 1, 0, 4, 0) != 1)
                    {
                        ans_count += 1;
                        StartCoroutine(Move(3, true, true, false));
                    }

                   // Debug.Log("wedelet");
                }
            }
        }





        return 0;
    }
    public void final_check()
    {
        if (checker2(0, 2, 1, 0, 5, 2, 0, 1, 5) == 1 && cube_angle_colortrue == false)
        {
            StartCoroutine(Move(3, true, true, false));

            StartCoroutine(Move(2, false, true, false));
            StartCoroutine(Move(0, false, true, false));
            StartCoroutine(Move(2, true, true, false));
            StartCoroutine(Move(0, true, true, false));

            StartCoroutine(Move(3, false, true, false));

            ans_count += 6;
            while (checker2(0, 8, 1, 2, 4, 2, 0, 1, 4) == 0 || cube_angle_colortrue == false)
            {
                StartCoroutine(Move(2, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(2, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                ans_count += 4;
            }
            StartCoroutine(Move(3, true, true, false));

            while (checker2(0, 8, 1, 2, 4, 2, 3, 0, 4) == 0 || cube_angle_colortrue == false)
            {
                StartCoroutine(Move(2, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(2, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                ans_count += 4;
            }

            while (checker(0, 7, 4, 1, 0, 4, 0) != 1)
            {
                ans_count += 1;
                StartCoroutine(Move(3, true, true, false));
            }
        }
    }
	public void arrback()
	{
		
	}
    public void copy_arr(int[,] tmp, int[,] tmp2)
    {
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                tmp2[i, j] = tmp[i, j];
    }
    public void findans()
    {

        history.Clear();
        history2.Clear();

        controlLock = true;
        int ans_count = 0;
        restart_cube =true;

        copy_arr(cube_arr, cube_arr2);
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                if (i == cube_arr[i, j])
                    ans_count += 1;


        DateTime time_start = DateTime.Now;


        if (ans_count != 54)
        {

            //     find_angle1(0, 1, 5);
            // Debug.Log("level" +findblock4(0, 1, 5));

            countText.text = (ans_count).ToString();
            show_all();
            /// <summary>
            /// 十字
            /// </summary>
            /// 
            ///第三層十字
            defaults= history.Count;
            copy_arr(cube_arr, defaultarr);

            Cross(0, 5, 1, 7, 1, 1, 0, 5);
            Cross(1, 5, 3, 5, 5, 5, 5, 1);
            Cross(2, 5, 1, 7, 7, 7, 1, 4);
            Cross(3, 5, 3, 5, 3, 3, 4, 0);
            while (Cross(0, 5, 1, 7, 1, 1, 0, 5) == false) ;
            while (Cross(1, 5, 3, 5, 5, 5, 5, 1) == false) ;
            while (Cross(2, 5, 1, 7, 7, 7, 1, 4) == false) ;
            while (Cross(3, 5, 3, 5, 3, 3, 4, 0) == false) ;
            topcross = history.Count;
            copy_arr(cube_arr, topcrossarr);

            ///第三層角塊
            while (cube_arr[0, 4] != 0)
                StartCoroutine(Move(6, true, true, false));
            find_angle1(0, 1, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(3, 0, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(2, 3, 5);
            StartCoroutine(Move(2, false, true, false));
            find_angle1(1, 2, 5);
            StartCoroutine(Move(2, false, true, false));
            topangle = history.Count;
            copy_arr(cube_arr, topanglearr);
            //第二層邊塊
            sec_block_level2(0, 5, 1, 1, 0, 1);
            sec_block_level2(0, 5, 1, 1, 3, 0);
            sec_block_level2(0, 5, 1, 1, 2, 3);
            sec_block_level2(0, 5, 1, 1, 1, 2);
            //第二層邊塊歸位
            
            while (cube_arr[0, 4] != 0)
                StartCoroutine(Move(6, true, true, false));
            midblock = history.Count;
            copy_arr(cube_arr, midblockarr);
            //第一層十字
            find_block3_level3(0, 4, 0);
            find_block3_level3(1, 4, 1);
            find_block3_level3(2, 4, 3);
            find_block3_level3(3, 4, 2);
  
            //第一層角塊
            fix_block3_level3();
            fix2_block3_level3();
 
            check_five();

            downcross = history.Count;
            copy_arr(cube_arr, downcrossarr);

            //五角歸位
            find_angle2(0, 1, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(1, 2, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(2, 3, 4);
            StartCoroutine(Move(3, false, true, false));
            find_angle2(3, 0, 4);
            StartCoroutine(Move(3, false, true, false));

        //    Debug.Log(final_angle);

            while (final_angle != 0)
            {
                StartCoroutine(Move(2, false, true, false));
                StartCoroutine(Move(0, false, true, false));
                StartCoroutine(Move(2, true, true, false));
                StartCoroutine(Move(0, true, true, false));
                plus_check();
            }

            final_check();
            downangle = history.Count;
            copy_arr(cube_arr, downanglearr);
            ans_tmp = (int[])history.ToArray();
        }

        DateTime time_end = DateTime.Now;//計時結束 取得目前時間
                                         //後面的時間減前面的時間後 轉型成TimeSpan即可印出時間差
        string result2 = ((TimeSpan)(time_end - time_start)).TotalSeconds.ToString();

        countText.text = (history.Count).ToString() + "步," + "花了" + result2 + "秒";
        ans_count = history.Count;
        //fix2_block3_level3();
        controlLock = false;
        // fix2_block3_level3();
        /*
        StartCoroutine(Move(6, true, true, true));
        Debug.Log(findblock3(0, 1, -1));
        */
        /*
        if (findblock3(0, 1, -1) == 1)
        {while(checker(f, 0)) == 1)
        }*/

        //  Debug.Log(find_blockwho(1, 5));

        //  Debug.Log((findblock3(1, 5, -1)));
        //  Debug.Log(color_true);
        //first();

        /*

           if ((((findblock3(0, 5, -1) == 3 && (checker(0, 1, 5, 1, 0, 5, 0)) == 1) && color_true == true)
      )==false    || (((findblock3(1, 5, -1) == 3 && (checker(1, 3, 5, 5, 1, 5, 0)) == 1) && color_true == true)) == false)
               findans();*/


        // sceand();
        // Debug.Log(cube_arr[2, 1]);
        // Debug.Log(cube_arr[5, 7]);
        /*
        string tmp = null;
        for (int i = 0; i < 9; i++)
        {
            tmp += cube_arr[0, i];

        }
        Debug.Log(tmp);
        tmp = null;
        for (int i = 0; i < 9; i++)
        {

            tmp += cube_arr[1, i];

        }
        Debug.Log(tmp);
        tmp = null;
        for (int i = 0; i < 9; i++)
        {

            tmp += cube_arr[2, i];

        }
        Debug.Log(tmp);
        tmp = null;
        for (int i = 0; i < 9; i++)
        {

            tmp += cube_arr[3, i];

        }
        Debug.Log(tmp);
        tmp = null;
        for (int i = 0; i < 9; i++)
        {

            tmp += cube_arr[4, i];

        }
        Debug.Log(tmp);
        tmp = null;
        for (int i = 0; i < 9; i++)
        {

            tmp += cube_arr[5, i];
        }
        Debug.Log(tmp);
        */

        //



        // StartCoroutine(Move(0, true, true, false));
    }
    public IEnumerator set_anscount(object[] tok)
    {
 

        /*
        for (int i = 0; i < 2; i++)
            msgArrCS[i] = tok[i];
            */

        //     targetObject = GameObject.Find("Main Camera");
        //    targetObject.SendMessage("arr_change", msgArrCS);
       chancemode = (int)tok[0];
        /*
        GameObject test;
        test = GameObject.Find("MagicCube");
        Quaternion quate = Quaternion.identity;
        for(int j=0;j<4;j++)
        for (int i=0;i<180;i+=60)
        {

            float time =0.001f;
            float current = 0;

            while (current < time)
            {
                current += 0.001f;
                yield return new WaitForEndOfFrame();
                           quate.eulerAngles = new Vector3(0, i, 0);
                test.transform.rotation = quate;
            }
         

            }*/

           StartCoroutine(OnRollback());

        Debug.Log("set_anscount");
        yield return new WaitForEndOfFrame();

    }
    IEnumerator Example(int i)
    {

        float time = 0.25f;
        float current = 0;
        while (current < time)
        {
            current += Time.deltaTime;
            GameObject test;
            test = GameObject.Find("MagicCube");
            Quaternion quate = Quaternion.identity;
            quate.eulerAngles = new Vector3(0, i, 0);
            test.transform.rotation = quate;
            yield return new WaitForEndOfFrame();
        }



    }
    IEnumerator OnReset()
    {


        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 9; j++)
                cube_arr2[i, j] = i;
            Cub_reset();

        show_double();
        System.Random crandom = new System.Random();

        controlLock = true;

        for (int i = 0; i < 27; i++)
        {

            StartCoroutine(Move(crandom.Next(0, 7), true, false, false));
        }
        controlLock = true;

        yield return new WaitForEndOfFrame();

        controlLock = false;
        restart_cube = false;

    }

    public void restart()
    {
        restart_cube = true;
    }
    public void OnReset(UnityEngine.EventSystems.BaseEventData data)
    {
        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock && !moveLock)
        {
            root.parent = transform;
            root.localPosition = Vector3.zero;

            count = 0;
            if (countText)
            {
                countText.text = count.ToString();
            }

            history.Clear();
            for (int i = 0; i < 27; ++i)
            {
                indexMapping[i] = i;
                cubes[i].localPosition = savedPositions[i];
                cubes[i].localRotation = Quaternion.identity;
            }
            HilightGroup();

            StartCoroutine(OnReset());
        }

    }

   public  IEnumerator OnRollback()
    {

        controlLock = true;
    
       

        int count = 0;
        int chance_tmp = 0;
        //   if (chancemode == 0) { chance_tmp = history2.Count; }
        if (chancemode == 0) { chance_tmp = defaults; }
        if (chancemode == 1) { chance_tmp = topcross; }
        if (chancemode == 2) { chance_tmp = topangle; }
        if (chancemode == 3) { chance_tmp = midblock; }
       if (chancemode == 4) { chance_tmp = downcross; }
      if (chancemode == 5) { chance_tmp = downangle; }
        show_double();
        foreach (int count_tmp in history)
        {

            if (count >= chance_tmp)
                break;
            else { 
            history2.Add(count_tmp);
            count++;
        }
        }
        count = 0;
        ans_count = history2.Count;
   

        int tmp = 0;
        while (tmp < ans_count)
        {
            if (!moveLock)
            {

                int vaule = ans_tmp[tmp];

                StartCoroutine(Move(vaule % 10, (vaule >= 10), false, true));
                tmp++;

                count++;
                countText.text = count.ToString();
            }
            yield return new WaitForEndOfFrame();
        }

        while (history2.Count > 0)
        {
            if (!moveLock)
            {
                if (history2.Count > 0)
                {
                    int vaule = history2[history2.Count - 1];

                    history2.RemoveAt(history2.Count - 1);
                    // StartCoroutine(Move(vaule % 10, !(vaule >= 10), false, false));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        controlLock = false;

    }

    public void OnRollback(UnityEngine.EventSystems.BaseEventData data)
    {
        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock)
        {
            StartCoroutine(OnRollback());
        }
    }
    public void Onshowsetting(UnityEngine.EventSystems.BaseEventData data)
    {
        isShowing = true;

    }
    public void OnPrevGroup(UnityEngine.EventSystems.BaseEventData data)
    {

        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock && !moveLock)
        {
            GameObject tmp;
            tmp = GameObject.Find("cube_view");
            if (groupIndex - 1 == -1)
                groupIndex = 6;
            else
                groupIndex--;
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + groupIndex);
            Debug.Log(groupIndex);
            HilightGroup();
        }
    }

    public void OnNextGroup(UnityEngine.EventSystems.BaseEventData data)
    {
        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock && !moveLock)
        {
 
            GameObject tmp;
            tmp = GameObject.Find("cube_view");
            // Debug.Log(Resources.LoadAll<Sprite>("Sprites/1"));
            if (groupIndex + 1 >= 7)
                groupIndex = 0;
            else
                groupIndex++;
    
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + groupIndex);
            Debug.Log(groupIndex);
            HilightGroup();
        }
    }

    public void OnRotateLeft(UnityEngine.EventSystems.BaseEventData data)
    {
        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock && !moveLock)
        {
            StartCoroutine(Move(groupIndex, false, true, true));
        }
    }

    public void OnRotateRight(UnityEngine.EventSystems.BaseEventData data)
    {
        if (buttonClip)
        {
            AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        }

        if (!controlLock && !moveLock)
        {
            StartCoroutine(Move(groupIndex, true, true, true));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void Awake()
    {

        this.screenHistory = new List<GameObject> { this.startScreen };
    }

    public void ToScreen(GameObject target)
    {
        Application.LoadLevel(0);

    }

    public void GoBack()
    {

        if (this.screenHistory.Count > 1)
        {

            int currentIndex = this.screenHistory.Count - 1;
            this.PlayScreen(this.screenHistory[currentIndex], this.screenHistory[currentIndex - 1], true, currentIndex - 2);
            this.screenHistory.RemoveAt(currentIndex);
        }
    }

    private void PlayScreen(GameObject current, GameObject target, bool isBack, int order)
    {

        current.GetComponent<Animator>().SetTrigger(this.outTrigger);

        if (isBack)
        {

            current.GetComponent<Canvas>().sortingOrder = order;

        }
        else
        {

            current.GetComponent<Canvas>().sortingOrder = order - 1;
            target.GetComponent<Canvas>().sortingOrder = order;
        }

        target.SetActive(true);
    }

}
