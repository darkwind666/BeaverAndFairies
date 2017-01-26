using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;


public class SelectLevelUIController : MonoBehaviour {

    public FadingScript fadingController;
	public GameGlobalSettings gameSettings;

    GamePlayerDataController _playerData;

	void Start ()
    {
        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
        _playerData = playerData;
	}

    void Update () {
	
	}

	public void easyGameSelected()
	{
		_playerData.selectedLevelIndex = 0;
		_playerData.savePlayerData();
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void normalGameSelected()
	{
		_playerData.selectedLevelIndex = 1;
		_playerData.savePlayerData();
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void hardGameSelected()
	{
		_playerData.selectedLevelIndex = 2;
		_playerData.savePlayerData();
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

}
