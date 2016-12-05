using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FairyLightningController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public FairiesDataList gameBalanceDataSource;
	public Image lineRechargeIndicator;

	GamePlayerDataController _playerData;

	int _bonusRechargeTime;
	int _currentBonusTime;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		lineRechargeIndicator.fillAmount = 0f;

		if(_playerData.selectedFairyIndex >= 0)
		{
			_playerData.damageBonusCount--;
			GameFairyModel fairyModel = gameBalanceDataSource.dataArray[_playerData.selectedFairyIndex];
			_bonusRechargeTime = fairyModel.fairyCreateSlowBonusTime;
		}

		_currentBonusTime = _bonusRechargeTime;
	}

	void Update () {
		float fillAmount = ((float)_bonusRechargeTime - _currentBonusTime) / (float)_bonusRechargeTime;
		lineRechargeIndicator.fillAmount = 1 - fillAmount;

		if(gameLogicController.stopGame == false && _playerData.selectedFairyIndex >= 0)
		{
			_currentBonusTime++;
			if(_currentBonusTime > _bonusRechargeTime)
			{
				_playerData.damageBonusCount++;
				_currentBonusTime = 0;
			}
		}
	}

}
