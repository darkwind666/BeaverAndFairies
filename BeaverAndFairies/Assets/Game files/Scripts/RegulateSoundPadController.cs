using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegulateSoundPadController : MonoBehaviour {

	public GameSoundsController gameSoundsController;
	public Toggle backgroundMusicToggle;
	public Toggle soundEffectsToggle;

	GamePlayerDataController _playerData;
	bool firstSetUpBackgroundMusic;
	bool firstSetUpSoundEffects;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		if (_playerData.enableBackgroundSound == false) {
			backgroundMusicToggle.isOn = true;
		} else {
			backgroundMusicToggle.isOn = false;
		}

		firstSetUpBackgroundMusic = true;

		if (_playerData.enableSoundsEffects == false) {
			soundEffectsToggle.isOn = true;
		} else {
			soundEffectsToggle.isOn = false;
		}

		firstSetUpSoundEffects = true;
	}
	

	void Update () {
	
	}

	public void enableSoundEffects()
	{
		if (firstSetUpSoundEffects == true) {
			_playerData.enableSoundsEffects = !_playerData.enableSoundsEffects;
		}
	}

	public void enableBackgroundMusic()
	{
		if (firstSetUpBackgroundMusic == true) {
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

}
