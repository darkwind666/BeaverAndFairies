using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameBalanceController : MonoBehaviour {
	
	public List1[] gameDifficultyModes;

	public GameLogicController gameLogicController;
	public Text stageLabel;
	public MainGameVkController vkController;

	int _currentStageIndex;
	int _currentPlayerLevel;
	int _currentStageTimeInterval;
	int _currentStageTime;

	List1 _gameBalaceData;

	bool randomStage;

	void Start () {
		GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_gameBalaceData = gameDifficultyModes[playerData.selectedLevelIndex];
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

					if(_currentStageIndex >= _gameBalaceData.dataArray.Length)
					{
						randomStage = true;
						_currentStageIndex = Random.Range(0, _gameBalaceData.dataArray.Length);
					}

				} else {
					_currentStageIndex = Random.Range(0, _gameBalaceData.dataArray.Length);
				}

				_currentPlayerLevel++;

				if((_currentPlayerLevel % 2) == 0)
				{
					vkController.sendInVkPlayerLevel(_currentPlayerLevel);
				}

				stageLabel.text = _currentStageIndex.ToString();
				setNewBalance();
			}
		}
	}

	public void setNewBalance() 
	{
		List1Data currentStageData = _gameBalaceData.dataArray[_currentStageIndex];

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
