using UnityEngine;
using System.Collections;

public class BlocksSpeedController : MonoBehaviour {

	public float slowSpeed;
	public float mediumSpeed;
	public float heightSpeed;

	public int slowSpeedTimeInterval;
	public int mediumSpeedTimeInterval;
	public int heightSpeedTimeInterval;

	public GameLogicController gameLogicController;

	public int speedTimeInterval;

	int _currentSpeedTimeInterval;

	int _currentTime;
	int _blocksSpeedIndex;

	void Start () {
		_currentTime = 0;
		_blocksSpeedIndex = 0;
		_currentSpeedTimeInterval = slowSpeedTimeInterval;
		speedTimeInterval = slowSpeedTimeInterval + mediumSpeedTimeInterval + heightSpeedTimeInterval;

		#if UNITY_IOS

		slowSpeed *= 2;
		mediumSpeed *= 2;
		heightSpeed *= 2;

		#endif

	}

	void Update () {
		if(gameLogicController.stopGame == false)
		{
			_currentTime++;
			if(_currentTime >= _currentSpeedTimeInterval)
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
		speedTimeInterval = slowSpeedTimeInterval + mediumSpeedTimeInterval + heightSpeedTimeInterval;
		if(_blocksSpeedIndex == 0)
		{
			gameLogicController.setBlocksSpeed(slowSpeed);
			_currentSpeedTimeInterval = slowSpeedTimeInterval;
		}

		if(_blocksSpeedIndex == 1)
		{
			gameLogicController.setBlocksSpeed(mediumSpeed);
			_currentSpeedTimeInterval = mediumSpeedTimeInterval;
		}

		if(_blocksSpeedIndex == 2)
		{
			gameLogicController.setBlocksSpeed(heightSpeed);
			_currentSpeedTimeInterval = heightSpeedTimeInterval;
		}
	}

}
