using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Facebook.MiniJSON;
using System;


public class FbSettingsPopUpController : MonoBehaviour {

	GamePlayerDataController _playerData;

	public Text playerName;
	public Image playerImage;

	public GameObject logInButton;
	public Text logInRewardText;

	public GameObject logOutButton;

	public GameObject joinBeaverTimeGroupButton;
	public GameObject goToBeaverTimeGroupButton;

	public GameGlobalSettings gameSettings;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		setUpFacebookSettins();
	}

	void setUpFacebookSettins()
	{
		logInRewardText.text = gameSettings.logInReward.ToString();

		if (FB.IsLoggedIn == true) {
			logInButton.SetActive (false);
			logOutButton.SetActive (true);
			logInRewardText.gameObject.SetActive (false);
			getUserInfo();
			getUserAvatar();
			getPlayerRewardForLogIn();
		} 
		else 
		{
			logInButton.SetActive (true);
			logOutButton.SetActive (false);
		}
	}

	void Update () {
	
	}

	void getUserInfo()
	{
		FB.API ("/me?fields=first_name", HttpMethod.GET, delegate (IGraphResult result) {
			if (result.ResultDictionary != null) {
				playerName.text = result.ResultDictionary ["first_name"].ToString();
			}

		});
	}

	void getUserAvatar()
	{
		FB.API("/me/picture", HttpMethod.GET, this.ProfilePhotoCallback);
	}

	void ProfilePhotoCallback(IGraphResult result)
	{
		if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
		{
			Texture2D tex=result.Texture;

			if (playerImage.sprite != null)
			{
				DestroyObject(playerImage.sprite);
			}

			playerImage.sprite=Sprite.Create(tex,new Rect(0,0,50,50),new Vector2(0.5f,0.5f));
			playerImage.enabled = true;
		}
	}

	public void logIn()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends", "publish_actions"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			getPlayerRewardForLogIn();
			setUpFacebookSettins();
		}
	}

	void getPlayerRewardForLogIn()
	{
		if (_playerData.logInFb == false) {
			_playerData.playerScore += gameSettings.logInReward;
			_playerData.logInFb = true;
			_playerData.savePlayerData();
		}
	}

	public void logOut()
	{
		FB.LogOut();
		setUpFacebookSettins();
	}

	public void inviteFriends()
	{
		if (FB.IsLoggedIn == true) {
			FB.Mobile.AppInvite(new Uri("https://fb.me/" + FB.AppId));
		} 
		else 
		{
			logIn();
		}

	}

	public void joinGameGroup()
	{
		if (FB.IsLoggedIn == true) {
			if(_playerData.inFbGameGroup == false)
			{
				FB.GameGroupJoin(
					gameSettings.fbGameGroupId,
					groupJoinCallBack);
			}
		} 
		else 
		{
			logIn();
		}
	}

	public void goToGameGroup()
	{
		Application.OpenURL(gameSettings.facebookPageURL);
	}

	void groupJoinCallBack (IGroupJoinResult result) {
		Debug.Log(result.RawResult);

		_playerData.playerScore += gameSettings.joinGroupReward;
		_playerData.inFbGameGroup = true;
		_playerData.savePlayerData();

		joinBeaverTimeGroupButton.SetActive (false);
		goToBeaverTimeGroupButton.SetActive (true);
	}

}
