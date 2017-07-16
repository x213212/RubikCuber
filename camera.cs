using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System.Reflection;
using System;
using System.IO;
using OpenCvSharp;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//一起動secScreen2表單啟動時候加載此腳本
//目前對抓取後的圖像存在   Texture2D  
//但是因為相機解析度的話 每個手機不一樣所以會有被畫面扭曲的問題
//然後又是用Texture2D的layout 是設定為根據表單大小而拉升然後去持續存取相機畫面所以 Texture2D是對應著表單大小而定
//目前的話因為不知道那表單的Scale怎樣縮放如果可以的話就可以進行因為表單拉伸而扭曲的畫面然後來進行 Texture2D的調整
//這樣的話我才可以再進行那個9宮格遮罩的對齊然後就是接下來的從畫面抓取魔術方塊的9個小方塊


public class camera : MonoBehaviour
{
    static int best_color_number =-1;
    static Color32[] tmp = new Color32[7];
    //渲染相片的相机.
    public Camera renderCamera;
    //显示摄像画面的对象.
    public GameObject imgObj;
    //显示拍照图片的对象.
    public GameObject photoObj;
    static public int mask=0;
    //摄像头名称.
    private static string deviceName;
    //拍摄的相片.
    private static Texture2D photo;
    //接收返回的图片数据.
    static WebCamTexture webTex;
    static bool isbool = false;
    static bool isbool2 = false;
    //相片的尺寸.
    int resWidth = 1360;
    int resHeight = 900;

    public static bool m_bIsFocus;
    public Color32 GetSimilarDegree(Texture2D t, int x, int y, int mod)
    {
        tmp[0] = new Color32(0, 0, 255, 255);//藍色
        tmp[1] = new Color32(255, 0, 0, 255);//紅色
        tmp[2] = new Color32(255, 255, 255, 255);//白色
        tmp[3] = new Color32(0, 255, 0, 255);//綠色
        tmp[4] = new Color32(255, 116, 21, 255);//橘色
        tmp[5] = new Color32(250, 244, 8, 255);//黃色
        decimal simdegree = 0;
        Double best = 0.0;
        Double new_color_r = 0.0;
        Double new_color_g = 0.0;
        Double new_color_b = 0.0;
        Color32 c1=new Color(0,0,0,0);
        if (mod == 0) { 
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
            {

                new_color_r += t.GetPixel(x + i, y + j).r;
                new_color_g += t.GetPixel(x + i, y + j).g;
                new_color_b += t.GetPixel(x + i, y + j).b;

            }

            c1 = new Color32(Convert.ToByte((new_color_r / 100) * 255), Convert.ToByte((new_color_g / 100) * 255), Convert.ToByte((new_color_b / 100) * 255), 255);
        photoObj.GetComponent<Text>().text = c1.ToString();
            
        }
        else if (mod == 1)
        {
            c1 = t.GetPixel(x, y);

        }
        Color32 best_color = tmp[3];
        for (int i = 0; i < 7; i++)
        {

            simdegree = 0;

            simdegree = 1 - Math.Round((decimal)(Math.Abs((c1.r - tmp[i].r) / 255.0) +
                                                                        Math.Abs((c1.g - tmp[i].g) / 255.0) +
                                                                        Math.Abs((c1.b - tmp[i].b) / 255.0)) / 3, 3);
     
            if (Convert.ToDouble(simdegree) >= best )
            {
                best = Convert.ToDouble(simdegree);
                best_color = tmp[i];
                best_color_number = i;
                //photoObj.GetComponent<Text>().text = i.ToString();

            }

        }
        photoObj.GetComponent<Text>().text = tmp[5].r.ToString();
        return best_color;

    }
    void Start()
    {
        //只讓相機加載一次
        if (isbool == false)
        {
            StartCoroutine(CallCamera());
            isbool = true;
            stop_camera();
                 int i = 0;

            tmp[0] = new Color32(0, 0, 255, 255);//藍色
            tmp[1] = new Color32(255, 0, 0, 255);//紅色
            tmp[2] = new Color32(255, 255, 255, 255);//白色
            tmp[3] = new Color32(0, 255, 0, 255);//綠色
            tmp[4] = new Color32(255, 116, 21, 255);//橘色
            tmp[5] = new Color32(250, 244, 8, 255);//黃色

            //   webTex.Stop();
        }
        m_bIsFocus = false;
        //開啟自動對焦
    
    }

    //调用外部摄像头.
    public IEnumerator CallCamera()
    {
        //授权.
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            var devices = WebCamTexture.devices;
            Debug.Log(devices.Length.ToString());
            if (devices.Length > 0)
            {
                //使用第一个摄像头.
                deviceName = devices[0].name;
                webTex = new WebCamTexture(deviceName,resHeight , resWidth, 1);
                //开始摄像.
                if (isbool == false)
                {
                    webTex.Stop();
              
                }
                else
                    webTex.Play();

                imgObj.transform.GetComponent<Renderer>().material.mainTexture = webTex;
                imgObj.transform.GetComponent<Image>().material = imgObj.transform.GetComponent<Renderer>().material;
             
            }
        }


    }
    void Updata()
    {

    }
    //调用外部摄像头.

    //拍照并保存照片.

    public void TakePhoto()
    {
	 float intensity = 0.35f;
		 Color drawColor = new Color32(255, 255, 255, 255);
	Color defaultColor = new Color32(0, 0, 0, 0);
        //相机渲染.

        GameObject targetObject;
        /*
        targetObject = GameObject.Find("mask1");
        Debug.Log(targetObject.transform.position.x.ToString() + "\n");
        Debug.Log(targetObject.transform.position.y.ToString());
        GameObject targetObject2;
        targetObject2 = GameObject.Find("mask9");
        Debug.Log(targetObject2.transform.position.x.ToString() + "\n");

        Debug.Log(targetObject2.transform.position.y.ToString());
        webTex.Play();
        */
        Texture2D t = new Texture2D(webTex.width, webTex.height);
	
		Texture2D tmp = new Texture2D(webTex.width, webTex.height);
        t.SetPixels(webTex.GetPixels());
        tmp.SetPixels(webTex.GetPixels());
        /* tmp.SetPixels(webTex.GetPixels(Mathf.FloorToInt(targetObject.transform.position.y), Mathf.FloorToInt(targetObject.transform.position.x)
             , webTex.width, webTex.height));
             */
        for (int y = 1; y < tmp.height-1; y++){
			for(int x = 1; x < tmp.width-1; x++){
				float g = t.GetPixel(x, y).grayscale;
				float gL = t.GetPixel(x-1, y).grayscale;
				float gR = t.GetPixel(x+1, y).grayscale;
				float gT = t.GetPixel(x, y-1).grayscale;
				float gB = t.GetPixel(x, y+1).grayscale;
				if(Mathf.Abs(g-gL) > intensity){
					tmp.SetPixel(x, y, drawColor);
				}else if(Mathf.Abs(g-gR) > intensity){
					tmp.SetPixel(x, y, drawColor);
				}else if(Mathf.Abs(g-gT) > intensity){
					tmp.SetPixel(x, y, drawColor);
				}else if(Mathf.Abs(g-gB) > intensity){
					tmp.SetPixel(x, y, drawColor);
				}else{
					tmp.SetPixel(x, y, defaultColor);
				}
			}
		} 
		List<Point3i> tl=new		List<Point3i>();
		int pox, poy=-1;
		for (int y = 40; y < tmp.height - 1; y++) {
			for (int x = 40; x < tmp.width - 1; x++) {
				if (tmp.GetPixel (x, y) == new  Color32 (255, 255, 255, 255) ) {
					tl.Add(new Point3i(x, y ,0));
				//	Circle(tmp,x,y, 50,new Color32(255, 0,0, 255));
				//	tmp.SetPixel(x, y,new Color32(255, 0,0, 255));
				}
			}

		}
		int x1=-1, y1=-1;
		int x2=-1, y2=-1;
		foreach(Point3i test in tl)
		{if (x1 == -1) {
				x1 = test.X;
				x2 = test.X;
				y1 = test.Y;
				y2 = test.Y;
			}
			if (x1 > test.X) {
				x1 = test.X;
			}
			if (x2 < test.X) {
				x2 = test.X;
			}
			if (y1 > test.Y) {
				y1 = test.Y;
			}
			if (y2 < test.Y) {
				y2 = test.Y;
			}
	
		}
		/* 抓4角
		Circle(tmp,x1,y1, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x2,y2, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1,y2, 50,new Color32(255, 0,0, 255));
		Circle(tmp,x2,y1, 50,new Color32(255, 0,0, 255));
				Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*1), 50,new Color32(255, 0,0, 255));

		Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*2), 50,new Color32(255, 0,0, 255));

		Circle(tmp,x1+(((x2-x1)/4)*1),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		Circle(tmp,x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*3), 50,new Color32(255, 0,0, 255));
		*/
		int test1=((((x2-x1)/4)*1))/2;
		int test2=((((y2-y1)/4)*1))/2;

        //int test2=;
        Circle(t,-test1+x1+(((x2-x1)/4)*1),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), -test2 + y1 + (((y2 - y1) / 4) * 1),0));
		Circle(t,x1+(((x2-x1)/4)*2),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), -test2 + y1 + (((y2 - y1) / 4) * 1), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),-test2+y1+(((y2-y1)/4)*1), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), -test2 + y1 + (((y2 - y1) / 4) * 1), 0));

        Circle(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 0));
        Circle(t,x1+(((x2-x1)/4)*2),y1+(((y2-y1)/4)*2), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), y1 + (((y2 - y1) / 4) * 2), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),y1+(((y2-y1)/4)*2), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), y1 + (((y2 - y1) / 4) * 2), 0));

        Circle(t,-test1+x1+(((x2-x1)/4)*1),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), test2 + y1 + (((y2 - y1) / 4) * 3), 0));
        Circle(t,x1+(((x2-x1)/4)*2),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), test2 + y1 + (((y2 - y1) / 4) * 3), 0));
        Circle(t,test1+x1+(((x2-x1)/4)*3),test2+y1+(((y2-y1)/4)*3), 30, GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), test2 + y1 + (((y2 - y1) / 4) * 3), 0));

      Set_color("input1", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), -test2 + y1 + (((y2 - y1) / 4) * 1), 1),0);
        Set_color("input2", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), y1 + (((y2 - y1) / 4) * 2), 1), 1);
        Set_color("input3", GetSimilarDegree(t, -test1 + x1 + (((x2 - x1) / 4) * 1), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 2);


        Set_color("input4", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), -test2 + y1 + (((y2 - y1) / 4) * 1), 1), 3);
        Set_color("input5", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), y1 + (((y2 - y1) / 4) * 2), 1), 4);
        Set_color("input6", GetSimilarDegree(t, x1 + (((x2 - x1) / 4) * 2), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 5);


        Set_color("input7", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), -test2 + y1 + (((y2 - y1) / 4) * 1), 1), 6);
        Set_color("input8", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), y1 + (((y2 - y1) / 4) * 2), 1), 7);
        Set_color("input9", GetSimilarDegree(t, test1 + x1 + (((x2 - x1) / 4) * 3), test2 + y1 + (((y2 - y1) / 4) * 3), 1), 8);

        t.Apply ();
       
        //把图片数据转换为byte数组
        byte[] byt =  t.EncodeToJPG();
        //如果Photo文件夹不存在，则创建一个.
        if (!Directory.Exists(Application.persistentDataPath + "/DCIM/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/DCIM/");
        }



        //然后保存为图片
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/DCIM/" + Time.time + ".jpg", byt);
    }

    public void open_mask()
    {
        GameObject gm = GameObject.Find("mask5");
        RectTransform rt = gm.GetComponent<RectTransform>();
        if (mask == 0) { 
        rt.sizeDelta = new Vector2(0, 70);
            mask = 1;
    }else
        {
            mask = 0;
            rt.sizeDelta = new Vector2(70, 70);
        }
    }

    public void Set_color(string name, Color32 col,int select)
    {
        tmp[0] = new Color32(0, 0, 255, 255);//藍色
        tmp[1] = new Color32(255, 0, 0, 255);//紅色
        tmp[2] = new Color32(255, 255, 255, 255);//白色
        tmp[3] = new Color32(0, 255, 0, 255);//綠色
        tmp[4] = new Color32(255, 116, 21, 255);//橘色
        tmp[5] = new Color32(250, 244, 8, 255);//黃色
        if (best_color_number == 6)
            best_color_number = 3;
  
            GameObject game_tmp = GameObject.Find(name);
       Image tx=game_tmp.GetComponent<Image>();
        tx.color =tmp[best_color_number];
        object[] msgArrCS = new object[2];
        msgArrCS[0] = select;
        msgArrCS[1] = best_color_number;
        game_tmp = GameObject.Find("setting_main");
        game_tmp.SendMessage("setcolor", msgArrCS);
       

    }// Update is called once per fram
	public void Circle(Texture2D tex, int cx, int cy, int r, Color32 col)
	{
		int x, y, px, nx, py, ny, d;

		for (x = 0; x <= r; x++)
		{
			d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
			for (y = 0; y <= d; y++)
			{
				px = cx + x;
				nx = cx - x;
				py = cy + y;
				ny = cy - y;

				tex.SetPixel(px, py, col);
				tex.SetPixel(nx, py, col);

				tex.SetPixel(px, ny, col);
				tex.SetPixel(nx, ny, col);

			}
		}    
	}
    //九宮格遮罩
    public void ovset_camera()
    {/*
        GameObject targetObject;
        if (isbool2 == false)
        {


            int basic_x = 280;
            int basic_y = 280;


            for (int i = 1; i <= 3; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(30, 280 + (100 * (i - 1)), 0);
            }
            for (int i = 4; i <= 6; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(130, 280 + (100 * (i - 4)), 0);
            }
            for (int i = 7; i <= 9; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(230, 280 + (100 * (i - 7)), 0);
            }
            isbool2 = true;
        }
        else
        {
            for (int i = 1; i <= 3; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(-10000, 0, 0);
            }
            for (int i = 4; i <= 6; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(-10000, 0, 0);
            }
            for (int i = 7; i <= 9; i++)
            {
                targetObject = GameObject.Find("mask" + i);
                targetObject.transform.position = new Vector3(-10000, 0, 0);
            }
            isbool2 = false;
        }*/
    }
    //相機開始監控
    public void call_camera()
    {

        isbool = true;
        webTex.Play();


    }
    //相機停止監控
    public void stop_camera()
    {

        isbool = false;
        webTex.Stop();
    }


}