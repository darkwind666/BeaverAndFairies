using UnityEngine;
using System.Collections;

public class GameLevelBackgroundController : MonoBehaviour {

	public float scrollingSpeed;
	public GameObject[] levelBackgrounds;

	GameObject _currentLevelBackground;

	void Start () {
	
		GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentLevelBackground = levelBackgrounds[playerData.selectedLevelIndex];
		_currentLevelBackground.SetActive(true);

	}

	void Update () {
		Vector2 offset = new Vector2(0, Time.time * scrollingSpeed);
		Renderer renderer = _currentLevelBackground.GetComponent<Renderer>();
		renderer.material.mainTextureOffset = offset;
	}
}
