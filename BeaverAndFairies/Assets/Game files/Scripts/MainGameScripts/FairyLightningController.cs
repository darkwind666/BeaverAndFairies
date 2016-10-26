using UnityEngine;
using System.Collections;

public class FairyLightningController : MonoBehaviour {

	public int explosionsCount;
	public int bonusRechargeTime;
	public GameLogicController gameLogicController;

	int _currentBonusTime;

	void Start () {
		_currentBonusTime = 0;
	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentBonusTime++;
			if(_currentBonusTime > bonusRechargeTime)
			{
				useLightning();
				_currentBonusTime = 0;
			}
		}
	}

	void useLightning()
	{
		int explosionsCount = this.explosionsCount;
		if(gameLogicController._currentBlocks.Count < explosionsCount)
		{
			explosionsCount = gameLogicController._currentBlocks.Count;
		}

		for(int explosionIndex = 0; explosionIndex < explosionsCount; explosionIndex++)
		{
			GameObject firstBlock = gameLogicController._currentBlocks.Dequeue();
			Destroy(firstBlock);
		}

		gameLogicController.startMoveUpBlocks();
	}

}
