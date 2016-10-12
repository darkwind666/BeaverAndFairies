using UnityEngine;
using System.Collections;

public class SlowdownBonusController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public float bonusGameSpeedSlowdownRate;
	int _maxBonusTime;
	int _currentBonusTime;
	float _startBonusGameSpeedSlowdown;

	int _currentBonusCount;
	bool _startBonus;

	void Start () {
		_currentBonusTime = 0;
	}

	void Update () {
	
		if(_startBonus == true)
		{
			_currentBonusTime++;
			if(_currentBonusTime >= _maxBonusTime)
			{
				_currentBonusTime = 0;
				_startBonus = false;
				gameLogicController.blocksSpeed = gameLogicController.blocksSpeed + _startBonusGameSpeedSlowdown;
			}
		}

	}

	public void useBonus()
	{
		if(_currentBonusCount > 0)
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
