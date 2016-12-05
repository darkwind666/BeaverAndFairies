using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightningBonusController : MonoBehaviour {

	public FairiesDataList gameBalanceData;
	public GameLogicController gameLogicController;
	public GameObject gameShopPopUp;
	public Text damageBonusCountLabel;
	public Image circularRechargeIndicator;

	GamePlayerDataController _playerData;
	int _currentBonusTime;


	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentBonusTime = gameBalanceData.damageBonusRechargeTime;
		circularRechargeIndicator.fillAmount = 0f;
	}

	void Update () {
		damageBonusCountLabel.text = _playerData.damageBonusCount.ToString();
		float fillAmount = ((float)gameBalanceData.damageBonusRechargeTime - _currentBonusTime) / (float)gameBalanceData.damageBonusRechargeTime;
		circularRechargeIndicator.fillAmount = fillAmount;

		_currentBonusTime++;
		if(_currentBonusTime >= gameBalanceData.damageBonusRechargeTime)
		{
			_currentBonusTime = gameBalanceData.damageBonusRechargeTime;
		}
	}

	public void useLightningBonus()
	{
		if (_playerData.damageBonusCount > 0) {

			if(_currentBonusTime >= gameBalanceData.damageBonusRechargeTime)
			{
				int explosionsCount = gameBalanceData.damageExplosionsBonusCount;
				if (gameLogicController._currentBlocks.Count < gameBalanceData.damageExplosionsBonusCount) {
					explosionsCount = gameLogicController._currentBlocks.Count;
				}

				for (int explosionIndex = 0; explosionIndex < explosionsCount; explosionIndex++) {
					GameObject firstBlock = gameLogicController._currentBlocks.Dequeue ();
					Destroy (firstBlock);
				}

				gameLogicController.startMoveUpBlocks ();
				_currentBonusTime = 0;
				_playerData.damageBonusCount--;
			}

		} else {
			gameShopPopUp.SetActive(true);
			gameLogicController.pauseGame();
		}
	}
}
