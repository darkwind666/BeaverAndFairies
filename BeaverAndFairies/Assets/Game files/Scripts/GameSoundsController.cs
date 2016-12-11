using UnityEngine;
using System.Collections;

public class GameSoundsController : MonoBehaviour {

	public AudioSource buttonClickEffect;
	public AudioSource mainMenuBackgroundSound;
	public AudioSource[] mainGameBackgroundSounds;
	public AudioSource removeBlockEffect;
	public AudioSource endGameBackgroundSound;

	public bool playMainMenuBackgroundSound;
	public bool playMainGameBackgroundSound;

	public AudioSource currentBackgroundSound;

	GamePlayerDataController _playerData;
	int _currentBackgroundSoundIndex;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		if(playMainMenuBackgroundSound)
		{
			currentBackgroundSound = mainMenuBackgroundSound;
		}

		if(playMainGameBackgroundSound)
		{
			_currentBackgroundSoundIndex = Random.Range(0, mainGameBackgroundSounds.Length);
			currentBackgroundSound = mainGameBackgroundSounds[_currentBackgroundSoundIndex];
			StartCoroutine(playEngineSound());
		}

		currentBackgroundSound.Play();

		if(_playerData.enableBackgroundSound == false)
		{
			currentBackgroundSound.volume = 0;
		}

	}
		
	void Update () {
	
	}

	IEnumerator playEngineSound()
	{
		yield return new WaitForSeconds(currentBackgroundSound.clip.length);
		_currentBackgroundSoundIndex = getNewBackgroundSoundIndex();
		currentBackgroundSound = mainGameBackgroundSounds[_currentBackgroundSoundIndex];

		if(_playerData.enableBackgroundSound == false)
		{
			currentBackgroundSound.volume = 0;
		}

		currentBackgroundSound.Play();
		StartCoroutine(playEngineSound());
	}

	int getNewBackgroundSoundIndex()
	{
		int newBackgroundSoundIndex = Random.Range(0, mainGameBackgroundSounds.Length);

		while(newBackgroundSoundIndex == _currentBackgroundSoundIndex)
		{
			newBackgroundSoundIndex = Random.Range(0, mainGameBackgroundSounds.Length);
		}

		return newBackgroundSoundIndex;
	}

	public void playButtonClick()
	{
		if(_playerData.enableSoundsEffects == true)
		{
			buttonClickEffect.Play();
		}
	}

}
