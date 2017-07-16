using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoFocusSet : MonoBehaviour {

	// Use this for initialization
	private bool autoFocusSetok;

	void Awake()
	{
		autoFocusSetok = false;
	}

	public static bool enableAutoFocus()
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass metaioSDKAndroid = new AndroidJavaClass("com.metaio.sdk.jni.IMetaioSDKAndroid");
		object[] args = {currentActivity};
		AndroidJavaObject camera = metaioSDKAndroid.CallStatic<AndroidJavaObject>("getCamera",args);

		if(camera != null)
		{
			AndroidJavaObject cameraParameters = camera.Call<AndroidJavaObject>("getParameters");
			object[] focusMode = {cameraParameters.GetStatic<string>("FOCUS_MODE_CONTINUOUS_PICTURE")};
			cameraParameters.Call("setFocusMode",focusMode);
			object[] newParameters = {cameraParameters};
			camera.Call("setParameters",newParameters);
			return true;
		}
		else
		{
			Debug.LogError("metaioSDK.enableAutoFocus:Camera not available");
			return false;
		}
	}

	public void callfocus()
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
		{
			Application.Quit();
		}

		if(Time.time > 1f && !autoFocusSetok)
		{
			autoFocusSetok = enableAutoFocus();
		}
	}
}
