using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegulateSoundPadController : MonoBehaviour {

	public GameSoundsController gameSoundsController;
	public Toggle backgroundMusicToggle;
	public Toggle soundEffectsToggle;

	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		if(_playerData.enableBackgroundSound == false)
		{
			backgroundMusicToggle.isOn = false;
		}

		if(_playerData.enableSoundsEffects == false)
		{
			soundEffectsToggle.isOn = false;
		}
	}
	

	void Update () {
	
	}

	public void enableSoundEffects()
	{
		_playerData.enableSoundsEffects = !_playerData.enableSoundsEffects;
	}

	public void enableBackgroundMusic()
	{
		_playerData.enableBackgroundSound = !_playerData.enableBackgroundSound;

		if(_playerData.enableBackgroundSound == true)
		{
			gameSoundsController.currentBackgroundSound.volume = 1.0f;
		} 
		else 
		{
			gameSoundsController.currentBackgroundSound.volume = 0;
		}
	}

}
