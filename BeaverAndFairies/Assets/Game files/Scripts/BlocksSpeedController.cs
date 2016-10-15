using UnityEngine;
using System.Collections;

public class BlocksSpeedController : MonoBehaviour {

	public float slowSpeed;
	public float mediumSpeed;
	public float heightSpeed;

	public GameLogicController gameLogicController;

	public int speedTimeInterval;

	int _currentTime;
	int _blocksSpeedIndex;

	void Start () {
		_currentTime = 0;
		_blocksSpeedIndex = 0;
	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentTime++;

			if(_currentTime >= speedTimeInterval)
			{
				_currentTime = 0;

				_blocksSpeedIndex++;
				if(_blocksSpeedIndex > 2)
				{
					_blocksSpeedIndex = 0;
				}

				setNewBlocksSpeed();
			}
		}
	}

	public void setNewBlocksSpeed()
	{
		if(_blocksSpeedIndex == 0)
		{
			gameLogicController.setBlocksSpeed(slowSpeed);
		}

		if(_blocksSpeedIndex == 1)
		{
			gameLogicController.setBlocksSpeed(mediumSpeed);
		}

		if(_blocksSpeedIndex == 2)
		{
			gameLogicController.setBlocksSpeed(heightSpeed);
		}
	}

}
