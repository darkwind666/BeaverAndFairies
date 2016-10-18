using UnityEngine;
using System.Collections;

public class BlocksSpawnTimeController : MonoBehaviour {

	public float bigGap;
	public float mediumGap;
	public float smallGap;

	int bigGapTimeInterval;
	int mediumGapTimeInterval;
	int smallGapTimeInterval;

	public GameLogicController gameLogicController;
	public BlocksSpeedController blocksSpeedController;

	int _spawnTimeInterval;
	int _currentTime;
	int _blocksSpawnIndex;

	void Start () {
		_currentTime = 0;
		_blocksSpawnIndex = 0;
	}

	void Update () {

		if(gameLogicController.stopGame == false)
		{
			_currentTime++;
			if(_currentTime >= _spawnTimeInterval)
			{
				_currentTime = 0;

				_blocksSpawnIndex++;
				if(_blocksSpawnIndex > 2)
				{
					_blocksSpawnIndex = 0;
				}

				setNewBlocksSpawnTime();
			}
		}
	}

	public void setNewBlocksSpawnTime()
	{
		int spawnTime = 0;
		if(_blocksSpawnIndex == 0)
		{
			spawnTime = (int)((gameLogicController._blockHeight + bigGap) / gameLogicController.blocksSpeed);
			_spawnTimeInterval = blocksSpeedController.speedTimeInterval / 3;
		}

		if(_blocksSpawnIndex == 1)
		{
			spawnTime = (int)((gameLogicController._blockHeight + mediumGap) / gameLogicController.blocksSpeed);
			_spawnTimeInterval = blocksSpeedController.speedTimeInterval / 5;
		}

		if(_blocksSpawnIndex == 2)
		{
			spawnTime = (int)((gameLogicController._blockHeight + smallGap) / gameLogicController.blocksSpeed);
			_spawnTimeInterval = blocksSpeedController.speedTimeInterval / 7;
		}
		gameLogicController.setBlocksSpawnTime(spawnTime);
	}

}
