using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class MainGameFbController : MonoBehaviour {

	public GameGlobalSettings gameSettings;
	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
	}

	void Update () {
	
	}

	public void sendInFbPlayerScore(int aScore)
	{
		if (FB.IsLoggedIn == true) {
			Dictionary<string,string> data = new Dictionary<string, string>();
			data["score"] = aScore.ToString();
			FB.API("/me/scores",HttpMethod.POST,Callback,data);
		} 
	}

	void Callback(IGraphResult result){}

	public void logInFb()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends", "publish_actions"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			getPlayerRewardForLogIn();
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

	public void joinFbGroup()
	{
		if (FB.IsLoggedIn == true) {
			if(_playerData.inFbGameGroup == false)
			{
				FB.GameGroupJoin(
					gameSettings.fbGameGroupId,
					fbGroupJoinCallBack);
			}
		} 
		else 
		{
			logInFb();
		}
	}

	void fbGroupJoinCallBack (IGroupJoinResult result) {
		Debug.Log(result.RawResult);

		_playerData.playerScore += gameSettings.joinGroupReward;
		_playerData.inFbGameGroup = true;
		_playerData.savePlayerData();

	}

	public void inviteFbFriends()
	{
		if (FB.IsLoggedIn == true) {
			FB.Mobile.AppInvite(new Uri("https://fb.me/" + FB.AppId));
		} 
		else 
		{
			logInFb();
		}
	}

}
