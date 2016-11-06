using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using Facebook.Unity;

public class MainGameSocialsController : MonoBehaviour {

	public GameGlobalSettings gameSettings;
	public GameObject acceptOperationController;
	public Button acceptButton;
	public GameObject inviteFriendsPopUp;
	public InviteFriendsController inviteFriendsController;

	VkApi _vkapi;
	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;
	}

	void Update () {
	
	}

	public void sendInVkPlayerScore(int aScore)
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=2?value=" + aScore.ToString()  
			};

			_vkapi.Call (r1);

		}
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

	public void sendInVkPlayerLevel(int aLevel)
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=1?value=" + aLevel.ToString()  
			};

			_vkapi.Call (r1);

		}
	}

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

	public void logInVk()
	{
		_vkapi.Login ();
	}

	void onVKLogin()
	{
		if (_playerData.logInVk == false) {
			_playerData.playerScore += gameSettings.logInReward;
			_playerData.logInVk = true;
			_playerData.savePlayerData ();
		}
	}

	public void joinVkGroup()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "groups.join?group_id=" + gameSettings.vkGameGroupId,
				CallBackFunction = joinVkGameGroupHandler
			};

			acceptOperationController.SetActive(true);
			acceptButton.onClick.AddListener(() => { 
				_vkapi.Call (r1);
			});

		} else {
			logInVk();
		}
	}

	void joinVkGameGroupHandler(VKRequest r)
	{
		if(r.error!=null)
		{
			return;
		}

		if(_playerData.inVkGameGroup == false)
		{
			_playerData.playerScore += gameSettings.joinGroupReward;
			_playerData.inVkGameGroup = true;
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

	public void inviteVkFriends()
	{
		if (_vkapi.IsUserLoggedIn) {
			setUpCurrentUserFriends();
			inviteFriendsPopUp.SetActive(true);
		} else {
			logInVk();
		}
	}

	void setUpCurrentUserFriends()
	{
		VKRequest r1 = new VKRequest (){
			url="apps.getFriendsList?extended=1&count=30&type=invite&fields=photo_50",
			CallBackFunction=getFriendsHandler
		};
		_vkapi.Call (r1);
	}

	void getFriendsHandler(VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		var dict = Json.Deserialize(request.response) as Dictionary<string,object>;
		var resp = (Dictionary<string,object>)dict["response"];
		var items = (List<object>)resp["items"];

		List<BeaverTimeVKFriend> friends = new List<BeaverTimeVKFriend>();

		foreach(var item in items)
		{
			BeaverTimeVKFriend friend = new BeaverTimeVKFriend();
			friend.friend = VKUser.Deserialize(item);
			friends.Add(friend);
		}

		inviteFriendsController.friendsDataSource = friends;

		if(inviteFriendsController.m_tableView.isActiveAndEnabled == true)
		{
			inviteFriendsController.m_tableView.ReloadData();
		}
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

	public void contactDeveloper()
	{
		string email = "darkwinddev@gmail.com";
		string subject = MyEscapeURL("FEEDBACK/SUGGESTION");
		string body = MyEscapeURL("Please Enter your message here\n\n\n\n" +
			"________" +
			"\n\nPlease Do Not Modify This\n\n" +
			"Model: "+SystemInfo.deviceModel+"\n\n"+
			"OS: "+SystemInfo.operatingSystem+"\n\n" +
			"________");
		Application.OpenURL ("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}

	string MyEscapeURL (string url) 
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}

	public void shareGameResult()
	{
		
	}

}
