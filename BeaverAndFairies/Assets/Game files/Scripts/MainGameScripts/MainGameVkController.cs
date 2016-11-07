using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;

public class MainGameVkController : MonoBehaviour {

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
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=2?value=" + aScore.ToString()  
			};

			_vkapi.Call (r1);
		}
	}

	public void sendInVkPlayerLevel(int aLevel)
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=1?value=" + aLevel.ToString()  
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

}
