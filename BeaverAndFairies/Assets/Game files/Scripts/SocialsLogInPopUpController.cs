using UnityEngine;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class SocialsLogInPopUpController : MonoBehaviour {

	public GameGlobalSettings gameGlobalSettings;
	public GameObject vkButton;
	public GameObject fbButton;

	GamePlayerDataController _playerData;
	VkApi _vkapi;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;
	}

	void OnDisable()
	{
		_vkapi.LoggedIn -= onVKLogin;
	}

	void Update () {
	
	}

	void onVKLogin()
	{
		VKRequest r = new VKRequest
		{
			url="users.get?",
			CallBackFunction=OnGetUserInfo
		};
		_vkapi.Call (r);
	}

	public void logInVk()
	{
		_vkapi.Login();
	}

	public void OnGetUserInfo (VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		_playerData.playerScore += gameGlobalSettings.logInReward;
		_playerData.logInVk = true;
		_playerData.savePlayerData();
		vkButton.SetActive(false);

		if(fbButton.activeSelf == false)
		{
			gameObject.SetActive(false);
		}
	}

	public void logInFacebook()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			_playerData.playerScore += gameGlobalSettings.logInReward;
			_playerData.logInFb = true;
			_playerData.savePlayerData();
			fbButton.SetActive(false);
		}

		if(vkButton.activeSelf == false)
		{
			gameObject.SetActive(false);
		}
	}

	public void notNowPressed()
	{
		_playerData.notNowPressed = true;
		gameObject.SetActive(false);
	}
}
