using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightningBonusController : MonoBehaviour {

	public FairiesDataList gameBalanceData;
	public GameLogicController gameLogicController;
	public GameObject gameShopPopUp;
	public Text damageBonusCountLabel;

	GamePlayerDataController _playerData;
	int _currentBonusTime;


	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentBonusTime = 0;
	}

	void Update () {
		damageBonusCountLabel.text = _playerData.damageBonusCount.ToString();
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
		}
	}
}
