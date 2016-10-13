using UnityEngine;
using System.Collections;

public class LightningBonusController : MonoBehaviour {

	public int explosionsBonusCount;
	public int maxLightningBounsCount;
	public int bonusRechargeTime;
	public GameLogicController gameLogicController;

	int _currentLightningBounsCount;
	int _currentBonusTime;

	void Start () {
		_currentLightningBounsCount = maxLightningBounsCount;
		_currentBonusTime = 0;
	}

	void Update () {

	}

	public void useLightningBonus()
	{
		if(_currentLightningBounsCount > 0)
		{
			int explosionsCount = explosionsBonusCount;
			if(gameLogicController._currentBlocks.Count < explosionsBonusCount)
			{
				explosionsCount = gameLogicController._currentBlocks.Count;
			}

			for(int explosionIndex = 0; explosionIndex < explosionsCount; explosionIndex++)
			{
				GameObject firstBlock = gameLogicController._currentBlocks.Dequeue();
				Destroy(firstBlock);
			}

			gameLogicController.startMoveUpBlocks();
			_currentLightningBounsCount--;
		}
	}
}
