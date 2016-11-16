using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FairySlowdownController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public FairiesDataList gameBalanceDataSource;
	public Image lineRechargeIndicator;

	GamePlayerDataController _playerData;

	int _bonusRechargeTime;
	int _currentBonusTime;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentBonusTime = _bonusRechargeTime;
		lineRechargeIndicator.fillAmount = 0f;

		if(_playerData.selectedFairyIndex >= 0)
		{
			_playerData.slowBonusCount--;
			GameFairyModel fairyModel = gameBalanceDataSource.dataArray[_playerData.selectedFairyIndex];
			_bonusRechargeTime = fairyModel.fairyCreateSlowBonusTime;
		}
	}

	void Update () {
		float fillAmount = ((float)_bonusRechargeTime - _currentBonusTime) / (float)_bonusRechargeTime;
		lineRechargeIndicator.fillAmount = 1 - fillAmount;

		if(gameLogicController.stopGame == false && _playerData.selectedFairyIndex >= 0)
		{
			_currentBonusTime++;
			if(_currentBonusTime > _bonusRechargeTime)
			{
				_playerData.slowBonusCount++;
				_currentBonusTime = 0;
			}
		}
	}

}
