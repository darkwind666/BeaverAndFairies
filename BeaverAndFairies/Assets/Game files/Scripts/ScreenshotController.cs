using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public class TakeScreenshot
{
	[MenuItem("Tools/Take Screenshot")]
	static public void OnTakeScreenshot()
	{
		Application.CaptureScreenshot(EditorUtility.SaveFilePanel("Save Screenshot As", "", "", "png"));
	}
}

#endif

public class ScreenshotController : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}
}
