using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using Facebook.Unity;

public class MainMenuController : MonoBehaviour {

    public FadingScript fadingController;
	public GameObject vkLogInPopUp;
	public GameGlobalSettings gameGlobalSettings;

	GamePlayerDataController _playerData;

	void Awake ()
	{
		if (!FB.IsInitialized) {
			FB.Init(InitCallback, OnHideUnity);
		} else {
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			FB.ActivateApp();
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

    void Start () {

        _playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		#if UNITY_ANDROID

		if(gameGlobalSettings.paidGame){
		OneSignal.StartInit(gameGlobalSettings.hdOneSignalId, gameGlobalSettings.hdOneSignalProjectNumber).EndInit();
		} else {
		OneSignal.StartInit(gameGlobalSettings.freeOneSignalId, gameGlobalSettings.freeOneSignalProjectNumber).EndInit();;
		}

		#endif
			
		if (_playerData.notNowPressed == false) {
			vkLogInPopUp.SetActive (true);
			GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
			analiticsController.showSocialsLogInPopUp();
		} 
    }

	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
	}

    void Update () {

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
		
}
