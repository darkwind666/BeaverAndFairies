using UnityEngine;
using System.Collections;

public class BlocksSpawnTimeController : MonoBehaviour {

	public int slowSpawn;
	public int mediumSpawn;
	public int heightSpawn;

	public GameLogicController gameLogicController;
	public int spawnTimeInterval;

	int _currentTime;

	void Start () {
		_currentTime = 0;
		setNewBlocksSpawnTime();
	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentTime++;

			if(_currentTime >= spawnTimeInterval)
			{
				_currentTime = 0;
				setNewBlocksSpawnTime();
			}
		}
	}

	void setNewBlocksSpawnTime()
	{
		int blocksSpawnIndex = Random.Range(0, 3);

		if(blocksSpawnIndex == 0)
		{
			gameLogicController.setBlocksSpawnTime(slowSpawn);
		}

		if(blocksSpawnIndex == 1)
		{
			gameLogicController.setBlocksSpawnTime(mediumSpawn);
		}

		if(blocksSpawnIndex == 2)
		{
			gameLogicController.setBlocksSpawnTime(heightSpawn);
		}
	}

}
