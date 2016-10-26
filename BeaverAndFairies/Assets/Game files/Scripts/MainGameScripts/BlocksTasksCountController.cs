using UnityEngine;
using System.Collections;

public class BlocksTasksCountController : MonoBehaviour {

	public int maxTasksCount;
	public GameLogicController gameLogicController;
	public int blocksTaskCountInterval;

	int _tasksTimeInterval;
	int _currentTime;

	void Start () {
		_tasksTimeInterval = blocksTaskCountInterval;
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
