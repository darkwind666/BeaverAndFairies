using UnityEngine;
using UnityEngine.UI;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections.Generic;
using System;
using System.Collections;

public class VkSettingsPopUpController : MonoBehaviour, VKontakteInviteFriendsInterface {

	public Text playerName;
	public Image playerImage;

	public GameObject logInButton;
	public GameObject logInRewardText;

	public GameObject logOutButton;
	public GameObject inviteFriendsPopUp;
	public InviteFriendsController inviteFriendsController;

	public GameObject joinBeaverTimeGroupButton;
	public GameObject goToBeaverTimeGroupButton;

	public GameObject joinVKGamesGroupButton;
	public GameObject goToVKGamesGroupButton;

	public GameObject acceptOperationController;
	public Button acceptButton;

	public GameGlobalSettings gameSettings;

	public Text logInVkRewardLabel;
	public Text joinVkGroupRewardLabel;
	public Text inviteVkFriendsRewardLabel;

	VkApi _vkapi;
	VKUser _currentUser;
	GamePlayerDataController _playerData;
	Downloader _downloader;

	void Start () {

		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;
		_vkapi.LoggedOut += onLogout;
		_downloader = _vkapi.gameObject.GetComponent<Downloader> ();

		playerImage.enabled = false;

		if (_vkapi.IsUserLoggedIn == true) {
			logInButton.SetActive (false);
			logOutButton.SetActive (true);
			logInRewardText.SetActive (false);
			getUserInfo();
		} 
		else 
		{
			logInButton.SetActive (true);
			logOutButton.SetActive (false);
		}

		logInVkRewardLabel.text = gameSettings.logInReward.ToString();
		joinVkGroupRewardLabel.text = gameSettings.joinGroupReward.ToString();
		inviteVkFriendsRewardLabel.text = (gameSettings.inviteFriendReward * 4).ToString();
	}

	void Update () {

	}

	void OnDisable()
	{
		_vkapi.LoggedIn -= onVKLogin;
		_vkapi.LoggedOut -= onLogout;
	}


	public void logIn()
	{
		_vkapi.Login ();
	}

	public void logOut()
	{
		_vkapi.Logout ();
	}

	void onVKLogin()
	{
		VKRequest r = new VKRequest
		{
			url="users.get?",
			CallBackFunction=tryLogInVk
		};

		_vkapi.Call (r);
	}

	public void tryLogInVk (VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		logInButton.SetActive (false);
		logOutButton.SetActive (true);
		getUserInfo();
	}

	void onLogout()
	{
		VKRequest r = new VKRequest
		{
			url="users.get?",
			CallBackFunction=tryToLogOut
		};

		_vkapi.Call (r);
	}

	public void tryToLogOut (VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		logOutButton.SetActive (false);
		logInButton.SetActive (true);
		playerName.text = "";
		DestroyObject(playerImage.sprite);
		playerImage.sprite = null;
		playerImage.enabled = false;

		joinBeaverTimeGroupButton.SetActive (true);
		goToBeaverTimeGroupButton.SetActive (false);

		joinVKGamesGroupButton.SetActive (true);
		goToVKGamesGroupButton.SetActive (false);

		inviteFriendsController.friendsDataSource = new List<BeaverTimeVKFriend>();
	}


	public void inviteFriends()
	{
		if (_vkapi.IsUserLoggedIn) {
			inviteFriendsPopUp.SetActive(true);
		} else {
			logIn();
		}
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

	public void addDeveloperToFriends()
	{
		string developerToFriendText = SmartLocalization.LanguageManager.Instance.GetTextValue(gameSettings.addDeveloperToFriendKey);
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest (){
				url="friends.add?user_id="+gameSettings.vkDeveloperId+"&text="+developerToFriendText,
			};

			acceptOperationController.SetActive(true);
			acceptButton.onClick.AddListener(() => { 
				_vkapi.Call (r1);
			});
		} else {
			logIn();
		}
	}


	void getUserInfo()
	{
		VKRequest r = new VKRequest
		{
			url="users.get?&fields=photo_50",
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

		setUpCurrentUser(request);
		string fullPlayerName = _currentUser.first_name + " " + _currentUser.last_name;
		playerName.text = fullPlayerName;
		setUpCurrentUserImage();
		setUpCurrentUserFriends();
		setUpVKGamesButton();
		getPlayerRewardForLogIn();
		getPlayerRewardForGroup();
	}

	void setUpCurrentUser(VKRequest request)
	{
		var dict=Json.Deserialize(request.response) as Dictionary<string,object>;
		var users=(List<object>)dict["response"];
		var user = VKUser.Deserialize (users [0]);
		_currentUser = user;
	}

	void setUpCurrentUserImage()
	{
		Action<DownloadRequest> doOnFinish =(downloadRequest)=>
		{
			Texture2D tex=downloadRequest.DownloadResult.texture;

			if (playerImage.sprite != null)
			{
				DestroyObject(playerImage.sprite);
			}

			playerImage.sprite=Sprite.Create(tex,new Rect(0,0,50,50),new Vector2(0.5f,0.5f));
			playerImage.enabled = true;
		};

		loadImageWithUrlAndCallback (_currentUser.photo_50, doOnFinish);
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

	void setUpCurrentUserFriends()
	{
		VKRequest r1 = new VKRequest (){
			url="apps.getFriendsList?extended=1&count=30&type=invite&fields=photo_50",
			CallBackFunction=getFriendsHandler
		};
		_vkapi.Call (r1);
	}

	void getPlayerRewardForLogIn()
	{
		if (_playerData.logInVk == false) {
			_playerData.playerScore += gameSettings.logInReward;
			_playerData.logInVk = true;
			_playerData.savePlayerData ();
		} 

		logInRewardText.SetActive (false);
	}

	void getPlayerRewardForGroup()
	{
		VKRequest r1 = new VKRequest (){
			url="groups.isMember?group_id=" + gameSettings.vkGameGroupId,
			CallBackFunction=onPlayerRewardForGroup
		};
		_vkapi.Call (r1);
	}

	void onPlayerRewardForGroup(VKRequest request)
	{
		var dict = Json.Deserialize(request.response) as Dictionary<string,object>;
		bool inGroup = Convert.ToBoolean(dict ["response"]);

		if (_playerData.inVkGameGroup == true) {
			joinBeaverTimeGroupButton.SetActive (false);
			goToBeaverTimeGroupButton.SetActive (true);
		} 
		else 
		{
			if (inGroup) {
				getRewardForBeaverTimeGroup ();
				joinBeaverTimeGroupButton.SetActive (false);
				goToBeaverTimeGroupButton.SetActive (true);
			} else {
				joinBeaverTimeGroupButton.SetActive (true);
				goToBeaverTimeGroupButton.SetActive (false);
			}
		}
	}






	public void joinVKGamesGroup()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest (){
				url="groups.join?group_id="+gameSettings.vkGamesOfficialGroupId,
				CallBackFunction=joinVKGamesHandler
			};

			acceptOperationController.SetActive(true);
			acceptButton.onClick.AddListener(() => { 
				_vkapi.Call (r1);
			});

		} else {
			logIn();
		}
	}

	void joinVKGamesHandler(VKRequest r)
	{
		if(r.error!=null)
		{
			return;
		}

		joinVKGamesGroupButton.SetActive (false);
		goToVKGamesGroupButton.SetActive (true);
	}

	public void joinBeaverTimeGroup()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "groups.join?group_id=" + gameSettings.vkGameGroupId,
				CallBackFunction = joinBeaverTimeGroupHandler
			};

			acceptOperationController.SetActive(true);
			acceptButton.onClick.AddListener(() => { 
				_vkapi.Call (r1);
			});

		} else {
			logIn();
		}
	}

	void joinBeaverTimeGroupHandler(VKRequest r)
	{
		if(r.error!=null)
		{
			return;
		}

		getRewardForBeaverTimeGroup();
		joinBeaverTimeGroupButton.SetActive (false);
		goToBeaverTimeGroupButton.SetActive (true);
	}

	void getRewardForBeaverTimeGroup()
	{
		_playerData.playerScore += gameSettings.joinGroupReward;
		_playerData.inVkGameGroup = true;
		_playerData.savePlayerData();
	}

	void setUpVKGamesButton()
	{
		VKRequest r1 = new VKRequest (){
			url="groups.isMember?group_id=" + gameSettings.vkGamesOfficialGroupId,
			CallBackFunction=onVkGamesOfficialGroup
		};
		_vkapi.Call (r1);
	}

	void onVkGamesOfficialGroup(VKRequest request)
	{
		if(request.error!=null)
		{
			return;
		}

		var dict = Json.Deserialize(request.response) as Dictionary<string,object>;
		bool inGroup = Convert.ToBoolean(dict ["response"]);

		if (inGroup) {
			joinVKGamesGroupButton.SetActive (false);
			goToVKGamesGroupButton.SetActive (true);
		} else {
			joinVKGamesGroupButton.SetActive (true);
			goToVKGamesGroupButton.SetActive (false);
		}
	}

	public void goToBeaverTimeGroup()
	{
		Application.OpenURL(gameSettings.vkURLTemplate + gameSettings.vkGameGroupId);
	}

	public void goToVkGamesGroup()
	{
		Application.OpenURL(gameSettings.vkURLTemplate + gameSettings.vkGamesOfficialGroupId);
	}

	public void allowMessagesFromGroup()
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "messages.allowMessagesFromGroup?group_id=" + gameSettings.vkGameGroupId
			};

			acceptOperationController.SetActive(true);
			acceptButton.onClick.AddListener(() => { 
				_vkapi.Call (r1);
				acceptOperationController.SetActive(false);
			});

		} else {
			logIn();
		}
	}

}
