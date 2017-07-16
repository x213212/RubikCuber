using UnityEngine;
using System.Collections;

public class screen_roat : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //设置屏幕自动旋转， 并置支持的方向
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
    // Update is called once per frame
    void Update () {


    }
    void Awake()
    {

    }
    public void roat_screen()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void roat_screen2()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

}
