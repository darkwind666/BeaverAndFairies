using UnityEngine;
using UnityEngine.UI;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Facebook.Unity;

public class SocialsLogInPopUpController : MonoBehaviour {

	public GameGlobalSettings gameGlobalSettings;
	public GameObject vkButton;
	public GameObject fbButton;
	public GameObject promoCodeButton;
	public Text vklLogInReward;
	public Text fblLogInReward;
	public Text promoCodeReward;
	public GameAnaliticsController gameAnaliticsController;

	GamePlayerDataController _playerData;
	VkApi _vkapi;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;
		vklLogInReward.text = "+" + gameGlobalSettings.logInReward.ToString();
		fblLogInReward.text = "+" + gameGlobalSettings.logInReward.ToString();
		promoCodeReward.text = "+" + gameGlobalSettings.promoCodeReward.ToString();

		if(_playerData.playerUsePromocode == true)
		{
			promoCodeButton.SetActive(false);
		}
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
			_playerData.notNowPressed = true;
			_playerData.savePlayerData();
			gameObject.SetActive(false);
		}
	}

	public void logInVk()
	{
		_vkapi.Login();
	}

	public void logInFacebook()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {

		if(result.Error != null)
		{
			return;
		}

		if (FB.IsLoggedIn) {
			_playerData.playerScore += gameGlobalSettings.logInReward;
			_playerData.logInFb = true;
			_playerData.savePlayerData();
			fbButton.SetActive(false);
			FB.LogInWithPublishPermissions(new List<string>() {"publish_actions"}, logInWithPublishPermissionsCallback);
		}

		if(vkButton.activeSelf == false)
		{
			_playerData.notNowPressed = true;
			_playerData.savePlayerData();
			gameObject.SetActive(false);
		}
	}

	private void logInWithPublishPermissionsCallback (ILoginResult result) {
		if (AccessToken.CurrentAccessToken.Permissions.Contains("publish_actions")) {
			gameAnaliticsController.playerAcceptPublishPermissions();
		}
	}

	public void notNowPressed()
	{
		_playerData.notNowPressed = true;
		_playerData.savePlayerData();
		gameObject.SetActive(false);
	}
}
