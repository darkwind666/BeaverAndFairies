using UnityEngine;
using System.Collections;

public class FinalChanceController : MonoBehaviour {

	public GameObject button;
	public AdsController adsController;
	public GameLogicController gameLogicController;
	public FairiesDataList gameBalanceData;

	int _currentTimeState;

	void Start () {
		_currentTimeState = 0;
	}

	void Update () {
		if(gameLogicController.stopGame == false && adsController.adAvailable())
		{
			if (button.activeSelf == false)
			{
				int randomExplosionIndex = Random.Range(0, 4);
				if(adsController.adAvailable() == true && randomExplosionIndex == 2)
				{
					button.SetActive (true);
				}
			}

			if(button.activeSelf)
			{
				_currentTimeState++;
				if(_currentTimeState > gameBalanceData.showButtonTime)
				{
					_currentTimeState = 0;
					button.SetActive(false);
				}
			}
		}
	}

	public void showAd()
	{
		_currentTimeState = 0;
		button.SetActive(false);
		adsController.showFinalChanceAd();
	}

	public void getReward()
	{
		int explosionsCount = gameBalanceData.finalChanceExplosionsBonusCount;
		if (gameLogicController._currentBlocks.Count < gameBalanceData.damageExplosionsBonusCount) {
			explosionsCount = gameLogicController._currentBlocks.Count;
		}

		for (int explosionIndex = 0; explosionIndex < explosionsCount; explosionIndex++) {
			GameObject firstBlock = gameLogicController._currentBlocks.Dequeue ();
			Destroy (firstBlock);
		}

		gameLogicController.startMoveUpBlocks();
	}
}
