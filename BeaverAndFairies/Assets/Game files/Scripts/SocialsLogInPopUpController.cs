using UnityEngine;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class SocialsLogInPopUpController : MonoBehaviour {

	public GameGlobalSettings gameGlobalSettings;

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

		var dict=Json.Deserialize(request.response) as Dictionary<string,object>;
		var users=(List<object>)dict["response"];
		var user = VKUser.Deserialize (users [0]);
		_playerData.playerScore += gameGlobalSettings.logInReward;
		_playerData.logInVk = true;
		_playerData.savePlayerData();
		gameObject.SetActive(false);
	}

	public void logInFacebook()
	{

	}

	public void notNowPressed()
	{
		_playerData.notNowPressed = true;
		gameObject.SetActive(false);
	}
}
