using UnityEngine;
using System.Collections;

public class BlocksTasksCountController : MonoBehaviour {

	public int maxTasksCount;
	public GameLogicController gameLogicController;
	public BlocksSpeedController blocksSpeedController;

	int _currentTasksCount;

	int _tasksTimeInterval;
	int _currentTime;

	void Start () {
		_currentTasksCount = 0;
		_tasksTimeInterval = blocksSpeedController.speedTimeInterval * 3;

	}

	void Update () {
		if(gameLogicController.stopGame == false && gameLogicController.blockTasksCount <= maxTasksCount)
		{
			_currentTime++;

			if(_currentTime >= _tasksTimeInterval)
			{
				_currentTime = 0;
				gameLogicController.blockTasksCount++;
			}
		}
	}

	public void setStartTasksCount()
	{
		gameLogicController.blockTasksCount++;
	}

}
