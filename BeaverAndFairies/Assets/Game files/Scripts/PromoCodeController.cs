using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PromoCodeController : MonoBehaviour {

	public InputField promoCodeField;
	public GameObject rightCodeSign;
	public GameObject wrongCodeSign;
	public GameObject doneButton;
	public GameGlobalSettings gameSettings;
	public GameAnaliticsController analiticsController;

	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		if (_playerData.playerUsePromocode == false) {
			rightCodeSign.SetActive (false);
			wrongCodeSign.SetActive (false);
			promoCodeField.gameObject.SetActive (true);
			doneButton.SetActive (true);
		} else {
			rightCodeSign.SetActive (true);
			wrongCodeSign.SetActive (false);
			promoCodeField.gameObject.SetActive(false);
			doneButton.SetActive (false);
		}
	}

	void Update () {
	
	}

	public void checkPromoCode()
	{
		if(_playerData.playerUsePromocode == false)
		{
			if (promoCodeField.text == gameSettings.promoCode) {
				_playerData.playerUsePromocode = true;
				_playerData.playerScore += gameSettings.promoCodeReward;
				_playerData.savePlayerData ();
				analiticsController.playerUsePromoCode ();
				rightCodeSign.SetActive (true);
				wrongCodeSign.SetActive (false);
				doneButton.SetActive(false);
				promoCodeField.gameObject.SetActive(false);
			} else {
				rightCodeSign.SetActive (false);
				wrongCodeSign.SetActive (true);
			}
		}
	}
}
