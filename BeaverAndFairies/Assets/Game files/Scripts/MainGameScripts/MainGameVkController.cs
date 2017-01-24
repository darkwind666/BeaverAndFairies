using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;

public class MainGameVkController : MonoBehaviour, VKontakteInviteFriendsInterface {

	public GameGlobalSettings gameSettings;
	public GameObject acceptOperationController;
	public Button acceptButton;
	public GameObject inviteFriendsPopUp;
	public InviteFriendsController inviteFriendsController;
	public Text joinVkGroupAward;
	public Text inviteVkFriendsAward;

	VkApi _vkapi;
	GamePlayerDataController _playerData;
	int _vkScore;
	int _vkLevel;
	String _vkClientKey;
	Downloader _downloader;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;
		_downloader = _vkapi.gameObject.GetComponent<Downloader>();

		joinVkGroupAward.text = "+" + gameSettings.joinGroupReward.ToString();
		inviteVkFriendsAward.text = "+" + (gameSettings.inviteFriendReward * 4).ToString();
	}

	void Update () {
	
	}

	void onVKLogin()
	{
		if (_playerData.logInVk == false) {
			_playerData.playerScore += gameSettings.logInReward;
			_playerData.logInVk = true;
			_playerData.savePlayerData ();
		}
	}

	void logInVk()
	{
		_vkapi.Login ();
	}

	public void sendInVkPlayerScore(int aScore)
	{ 
		if (_vkapi.IsUserLoggedIn) {
			_vkScore = aScore;
			string fullurl = "https://oauth.vk.com/access_token?client_id=" + VkApi.VkSetts.VkAppId.ToString() + "&client_secret=ar0NKJWK7df9f5czE1za" + "&v=5.60&grant_type=client_credentials";
			VKRequest r1 = new VKRequest () {
				url = fullurl,
				CallBackFunction = getClientKey,
				data = new Action[] {sendVKScore},
			};

			_vkapi.Call (r1);
		}
	}

	void sendVKScore()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "https://api.vk.com/method/secure.addAppEvent?user_id=" + VkApi.CurrentToken.user_id + "&activity_id=2&value=" + _vkScore.ToString() + "&client_secret=ar0NKJWK7df9f5czE1za" + "&v=5.50&access_token=" + _vkClientKey + "&https=1",
				CallBackFunction = checkRequest
			};

			_vkapi.Call (r1);
		}
	}

	void getClientKey(VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		var dict = Json.Deserialize(request.response) as Dictionary<string,object>;
		var resp = (Dictionary<string,object>)dict;
		var access_token = (String)resp["access_token"];
		_vkClientKey = access_token;

		Action callback = request.data[0] as Action;
		callback();
	}

	void checkRequest(VKRequest r)
	{
		if(r.error!=null)
		{
			return;
		}
	}

	public void sendInVkPlayerLevel(int aLevel)
	{
		if (_vkapi.IsUserLoggedIn) {
			_vkLevel = aLevel;
			string fullurl = "https://oauth.vk.com/access_token?client_id=" + VkApi.VkSetts.VkAppId.ToString() + "&client_secret=ar0NKJWK7df9f5czE1za" + "&v=5.60&grant_type=client_credentials";
			VKRequest r1 = new VKRequest () {
				url = fullurl,
				CallBackFunction = getClientKey,
				data = new Action[] {sendVKLevel},
			};

			_vkapi.Call (r1);
		}
	}

	void sendVKLevel()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "https://api.vk.com/method/secure.addAppEvent?user_id=" + VkApi.CurrentToken.user_id + "&activity_id=1&value=" + _vkLevel.ToString() + "&client_secret=ar0NKJWK7df9f5czE1za" + "&v=5.50&access_token=" + _vkClientKey + "&https=1",
				CallBackFunction = checkRequest
			};

			_vkapi.Call (r1);
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

	public void loadImageWithUrlAndCallback (string aUrl, Action<DownloadRequest> aCallback)
	{
		var request = new DownloadRequest
		{
			url = aUrl,
			onFinished = aCallback,
		};

		_downloader.download(request);
	}

	public void inviteFriend(string friendId, string friendName, Action aCallback)
	{
		string inviteTextTemplate = SmartLocalization.LanguageManager.Instance.GetTextValue(gameSettings.inviteTextKey);
		string inviteText = string.Format(inviteTextTemplate, friendName);

		VKRequest r1 = new VKRequest (){
			url="apps.sendRequest?user_id="+friendId+"&text=" + inviteText + "&type=invite&name=BeaverTime",
			CallBackFunction=inviteFriendHandler,
			data = new Action[] {aCallback},
		};

		_vkapi.Call (r1);
	}

	void inviteFriendHandler(VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		_playerData.playerScore += gameSettings.inviteFriendReward;
		_playerData.savePlayerData();
		Action callback = request.data[0] as Action;
		callback();
	}

}
