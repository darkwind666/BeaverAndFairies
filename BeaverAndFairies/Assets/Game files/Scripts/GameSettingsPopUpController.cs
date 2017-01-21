using UnityEngine;
using System.Collections;

public class GameSettingsPopUpController : MonoBehaviour {

	public GameObject promoCodeButton;

	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		if(_playerData.playerUsePromocode == true)
		{
			promoCodeButton.SetActive(false);
		}
	}

	void Update () {
	
	}
}
