using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;


public class SelectLevelUIController : MonoBehaviour {

    public FadingScript fadingController;

    public AudioSource backgroundSound;
    public AudioSource buttonClickEffect;

	public GameGlobalSettings gameSettings;

    GamePlayerDataController _playerData;

	void Start ()
    {
        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
        //setupSoundWithPlayerData(playerData);
        _playerData = playerData;
	}

    void setupSoundWithPlayerData(GamePlayerDataController aPlayerData)
    {
        backgroundSound.volume = aPlayerData.gameMusicVolume;
        buttonClickEffect.volume = aPlayerData.gameSoundEffectsVolume;
    }

    void Update () {
	
	}

	public void easyGameSelected()
	{
		_playerData.selectedLevelIndex = 0;
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void normalGameSelected()
	{
		_playerData.selectedLevelIndex = 1;
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void hardGameSelected()
	{
		_playerData.selectedLevelIndex = 2;
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void backPressed()
	{
		fadingController.startFade(gameSettings.mainMenuScreenName, false);
	}

}
