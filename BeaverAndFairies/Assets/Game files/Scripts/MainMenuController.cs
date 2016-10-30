using UnityEngine;
using UnityEngine.UI;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System;
using System.Collections.Generic;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public FadingScript fadingController;

    public GameObject gameSettingsPopUp;
	public GameObject vkLogInPopUp;
	public GameObject gameShopPopUp;

    public Slider musicSlider;
    public Slider effectsSlider;

    public AudioSource buttonClickEffect;
    public AudioSource backgroundSound;

	public GameGlobalSettings gameGlobalSettings;

	GamePlayerDataController _playerData;
	VkApi _vkapi;

    void Start () {

        _playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_vkapi = VkApi.VkApiInstance;
		_vkapi.LoggedIn += onVKLogin;

		if(gameGlobalSettings.paidGame){
			OneSignal.StartInit(gameGlobalSettings.hdOneSignalId, gameGlobalSettings.hdOneSignalProjectNumber).EndInit();;
		} else {
			OneSignal.StartInit(gameGlobalSettings.freeOneSignalId, gameGlobalSettings.freeOneSignalProjectNumber).EndInit();;
		}
			
		if (_playerData.playerExist == false && _playerData.notNowPressed == false) {
			vkLogInPopUp.SetActive (true);
		} 

       // setupAudio();
    }

	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
	}

    void setupAudio()
    {
        musicSlider.value = _playerData.gameMusicVolume;
        effectsSlider.value = _playerData.gameSoundEffectsVolume;

        backgroundSound.volume = _playerData.gameMusicVolume;
        buttonClickEffect.volume = _playerData.gameSoundEffectsVolume;
    }

	void OnDisable()
	{
		_vkapi.LoggedIn -= onVKLogin;
	}

    void Update () {

	}

    public void goToMapButtonPressed()
    {
		fadingController.startFade(gameGlobalSettings.selectLevelSceneName, false);
    }

    public void exitButtonPressed()
    {
        Application.Quit();
    }

    public void gameMusicVolumeChanged(float aVolume)
    {
        _playerData.gameMusicVolume = aVolume;
        _playerData.savePlayerData();
    }

    public void gameSoundEffectsVolumeChanged(float aVolume)
    {
        _playerData.gameSoundEffectsVolume = aVolume;
        _playerData.savePlayerData();
    }

	public void notNowPressed()
	{
		_playerData.notNowPressed = true;
	}

	public void logIn()
	{
		_vkapi.Login();
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

		var dict=Json.Deserialize(request.response) as Dictionary<string,object>;
		var users=(List<object>)dict["response"];
		var user = VKUser.Deserialize (users [0]);
		string fullPlayerName = user.first_name + " " + user.last_name;
		_playerData.createNewPlayerWithName (fullPlayerName);
		_playerData.playerScore += gameGlobalSettings.logInReward;
		_playerData.logInVk = true;
		_playerData.savePlayerData();
		vkLogInPopUp.SetActive(false);
	}

	public void gameRecordsPressed()
	{
		
	}
}
