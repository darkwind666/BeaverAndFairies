using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameBalanceController : MonoBehaviour {

	public List1 gameBalaceData;
	public GameLogicController gameLogicController;
	public Text stageLabel;
	public MainGameSocialsController socialsController;

	int _currentStageIndex;
	int _currentPlayerLevel;
	int _currentStageTimeInterval;
	int _currentStageTime;

	bool randomStage;

	void Start () {
		_currentStageIndex = 0;
		_currentPlayerLevel = 0;
		_currentStageTime = 0;
	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentStageTime++;

			if(_currentStageTime >= _currentStageTimeInterval)
			{
				_currentStageTime = 0;

				if (randomStage == false) {
					_currentStageIndex++;

					if(_currentStageIndex >= gameBalaceData.dataArray.Length)
					{
						randomStage = true;
						_currentStageIndex = Random.Range(0, gameBalaceData.dataArray.Length);
					}

				} else {
					_currentStageIndex = Random.Range(0, gameBalaceData.dataArray.Length);
				}

				_currentPlayerLevel++;

				if((_currentPlayerLevel % 5) == 0)
				{
					socialsController.sendInVkPlayerLevel(_currentPlayerLevel);
				}

				stageLabel.text = _currentStageIndex.ToString();
				setNewBalance();
			}
		}
	}

	public void setNewBalance() 
	{
		List1Data currentStageData = gameBalaceData.dataArray[_currentStageIndex];

		gameLogicController.setBlocksSpeed(currentStageData.Speed);

		#if UNITY_IOS

		gameLogicController.setBlocksSpeed(currentStageData.Speed * 2);

		#endif

		int spawnTime = (int)((gameLogicController._blockHeight + currentStageData.Distancebetweeneblocks) / gameLogicController.blocksSpeed);
		gameLogicController.setBlocksSpawnTime(spawnTime);
		gameLogicController.blockTasksCount = currentStageData.Taskcount;
		gameLogicController.blockType = currentStageData.Blocktype;

		gameLogicController.randomTasksCount = true;
		if(currentStageData.Randomtaskcount == 0)
		{
			gameLogicController.randomTasksCount = false;
		}

		_currentStageTimeInterval = (int) (currentStageData.Timeinterval * 60);
	}
}
