using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using Facebook.Unity;

public class MainGameShareController : MonoBehaviour {

	public GameGlobalSettings gameSettings;
	public NativeShare shareController;

	void Start () {
	}

	void Update () {
	
	}

	public void shareGameResult()
	{
		shareController.ShareScreenshotWithText("Sasha");
	}

}
