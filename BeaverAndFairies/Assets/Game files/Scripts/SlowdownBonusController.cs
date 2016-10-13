using UnityEngine;
using System.Collections;

public class SlowdownBonusController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public int maxSlowdownBonusCount;
	public float bonusGameSpeedSlowdownRate;
	public int maxBonusTime;
	int _currentBonusTime;
	float _startBonusGameSpeedSlowdown;

	int _currentBonusCount;
	bool _startBonus;

	void Start () {
		_currentBonusCount = maxSlowdownBonusCount;
		_currentBonusTime = 0;
	}

	void Update () {
	
		if(_startBonus == true)
		{
			_currentBonusTime++;
			if(_currentBonusTime >= maxBonusTime)
			{
				_currentBonusTime = 0;
				_startBonus = false;
				gameLogicController.blocksSpeed = gameLogicController.blocksSpeed + _startBonusGameSpeedSlowdown;
			}
		}

	}

	public void useBonus()
	{
		if(_currentBonusCount > 0 && _startBonus == false)
		{
			float newSpeed = gameLogicController.blocksSpeed - gameLogicController.blocksSpeed * bonusGameSpeedSlowdownRate;
			_startBonusGameSpeedSlowdown = gameLogicController.blocksSpeed * bonusGameSpeedSlowdownRate;
			gameLogicController.blocksSpeed = newSpeed;
			_currentBonusTime = 0;
			_startBonus = true;
			_currentBonusCount--;
		}
	}
}
