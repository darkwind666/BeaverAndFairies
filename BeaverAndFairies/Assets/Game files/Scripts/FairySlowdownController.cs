using UnityEngine;
using System.Collections;

public class FairySlowdownController : MonoBehaviour {

	public GameLogicController gameLogicController;
	public float bonusGameSpeedSlowdownRate;
	public int maxBonusTime;
	public int bonusRechargeTime;
	int _currentBonusTime;
	float _startBonusGameSpeedSlowdown;

	bool _startSlowdown;

	void Start () {
		_currentBonusTime = 0;
	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentBonusTime++;
			if (_startSlowdown == true) {
				if (_currentBonusTime >= maxBonusTime) {
					_currentBonusTime = 0;
					_startSlowdown = false;
					gameLogicController.setBlocksSpeed(gameLogicController.blocksSpeed + _startBonusGameSpeedSlowdown);
				}
			} else {
				if (_currentBonusTime >= bonusRechargeTime) {
					useBonus ();
				}
			}
		}
	}

	public void useBonus()
	{
		float newSpeed = gameLogicController.blocksSpeed - gameLogicController.blocksSpeed * bonusGameSpeedSlowdownRate;
		_startBonusGameSpeedSlowdown = gameLogicController.blocksSpeed * bonusGameSpeedSlowdownRate;
		gameLogicController.setBlocksSpeed(newSpeed);
		_currentBonusTime = 0;
		_startSlowdown = true;
	}

}
