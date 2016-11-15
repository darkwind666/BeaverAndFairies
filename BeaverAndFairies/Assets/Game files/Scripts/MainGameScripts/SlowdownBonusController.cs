using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlowdownBonusController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public GameObject gameShopPopUp;
	public Text slowBonusCountLabel;
	public FairiesDataList gameBalanceData;
    int _bonusRechargeTime;
	int _currentBonusTime;
	float _startBonusGameSpeedSlowdown;

	GamePlayerDataController _playerData;
	bool _startBonus;

	void Start () {
		_bonusRechargeTime = gameBalanceData.slowBonusRechargeTime + gameBalanceData.slowBonusMaxTime;
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentBonusTime = 0;
	}

	void Update () {
		slowBonusCountLabel.text = _playerData.slowBonusCount.ToString();

		_currentBonusTime++;
		if(_currentBonusTime >= _bonusRechargeTime)
		{
			_currentBonusTime = _bonusRechargeTime;
		}

		if(_startBonus == true)
		{
			if(_currentBonusTime >= gameBalanceData.slowBonusMaxTime)
			{
				_startBonus = false;
				gameLogicController.setBlocksSpeed(gameLogicController.blocksSpeed + _startBonusGameSpeedSlowdown);
			}
		}
	}

	public void useBonus()
	{
		if(_playerData.slowBonusCount > 0)
		{
			if(_currentBonusTime >= _bonusRechargeTime)
			{
				float newSpeed = gameLogicController.blocksSpeed - gameLogicController.blocksSpeed * gameBalanceData.bonusGameSpeedSlowdownRate;
				_startBonusGameSpeedSlowdown = gameLogicController.blocksSpeed * gameBalanceData.bonusGameSpeedSlowdownRate;
				gameLogicController.setBlocksSpeed(newSpeed);
				_currentBonusTime = 0;
				_startBonus = true;
				_playerData.slowBonusCount--;
			}
				
		} else {
			gameShopPopUp.SetActive(true);
		}
	}
}
