using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
public class LoginController : MonoBehaviour {
	VkApi vkapi;

	public void Start(){
		vkapi=VkApi.VkApiInstance;
		if(vkapi.IsUserLoggedIn)
			Application.LoadLevel("StarterScene");
	}

	public void LoginToVK()
	{
		VkApi.VkSetts.forceOAuth = false;
		vkapi.LoggedIn += onVKLogin;
		vkapi.Login ();
	}
	public void LoginVKOauth()
	{
		vkapi.LoggedIn += onLogin;
		vkapi.LoggedOut += onLogout;

		VkApi.VkSetts.forceOAuth = true;
		vkapi.Logout ();
	}
	public void onLogin(){
		vkapi.LoggedIn -= onLogin;
		Application.LoadLevel("StarterScene");
	}
	public void onVKLogin(){
		vkapi.LoggedIn -= onVKLogin;
		Application.LoadLevel("StarterScene");
	}
	public void onLogout(){
		vkapi.LoggedOut -= onLogout;

		vkapi.Login ();
	}
}
