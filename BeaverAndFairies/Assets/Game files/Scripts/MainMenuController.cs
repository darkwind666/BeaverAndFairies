using UnityEngine;
using UnityEngine.UI;
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

    void Start () {

        _playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

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

	public void gameRecordsPressed()
	{
		
	}

}
