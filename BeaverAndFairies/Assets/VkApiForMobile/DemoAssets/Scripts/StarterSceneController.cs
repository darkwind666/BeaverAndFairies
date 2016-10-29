using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
public class StarterSceneController : MonoBehaviour {

	public void Start(){
		if (VkApi.VkApiInstance.IsUserLoggedIn) {
			return;
		}else{
			VkApi.VkApiInstance.Login();
		}
	}

	public void TestCaptcha(){
		VKRequest r = new VKRequest ()
		{
			url="captcha.force",
			CallBackFunction=OnCaptchaForse
			
		};
		VkApi.VkApiInstance.Call (r);
	}
	void OnCaptchaForse(VKRequest r)
	{
		if (r.error != null) {
			FindObjectOfType<GlobalErrorHandler>().Notification.Notify(r);
			return;
		}
		Debug.Log (r.response);
	}
	public void SendNotificationToAdmin(){
		Application.LoadLevel ("NotificationToAdmin");
	}
	public void FriendsGet(){
		Application.LoadLevel ("Friends");
	}
	public void ShareScreenShot(){
		Application.LoadLevel ("ScreenShotShareDemo");
	}
	public void Logout(){
		VkApi.VkApiInstance.Logout ();
		Application.LoadLevel ("LoginScene");
	}


}
