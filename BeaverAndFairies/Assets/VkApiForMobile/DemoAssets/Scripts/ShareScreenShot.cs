using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
using System.Collections.Generic;
using System.IO;

public class ShareScreenShot : MonoBehaviour {
	VkApi vkapi;
	string _filePath;
	byte[] jpegScreenShotBytes;
	// Use this for initialization
	void Start () {
		vkapi = VkApi.VkApiInstance;

		if (!vkapi.IsUserLoggedIn) {
		
			vkapi.Login ();
		}
	}

	public void TakeScreenShot ()
	{
#if UNITY_WSA 
		var imagename="screnshot.png";
#else
		var imagename="screnshot.jpg";
#endif

		_filePath=Path.Combine( Application.persistentDataPath,imagename);
#if !UNITY_EDITOR && (UNITY_ANDROID  || UNITY_IOS)
		Application.CaptureScreenshot (imagename);
#else
		Application.CaptureScreenshot (_filePath);
#endif

		StartCoroutine (LoadScreenShot());

	}
	IEnumerator LoadScreenShot()
	{
		//wait some seconds to assure that Application.CaptureScreenshot has finished creating screenshot
		yield return new WaitForSeconds(3);
		while (!vkapi.IsUserLoggedIn) {
			yield return null;
		}
		WWW www = new WWW ("file:///"+_filePath);
		yield return www;

		if (string.IsNullOrEmpty (www.error)) {
			Texture2D tex=www.texture;
			 jpegScreenShotBytes= tex.EncodeToJPG();
			List<ShareImage> imgs=new List<ShareImage>();
			ShareImage screenshot=new ShareImage
								  {
									data=jpegScreenShotBytes,
									imageName="screenshot.jpg",
									imagetype=ImageType.Jpeg
								  };
			imgs.Add(screenshot);


			var vkShare=new VKShare(OnShareFinished,"Hello From VK Api",imgs,"http://u3d.as/8HK");
			vkShare.Share();
			   
		}


	}
	void OnShareFinished(VKRequest resp)
	{
		if (resp.error != null)
			return;

		Debug.Log("Succesfully finished sharing");

	}
	public void Back(){
		Application.LoadLevel ("StarterScene");
	}
	


}
