using UnityEngine;
using System.Collections;

public class GameTutorialController : MonoBehaviour {

	public GameObject[] tutorialParts;
	public GameLogicController gameLogicController;

	GamePlayerDataController _playerData;
	int _currentTutorialIndex;
	bool _activeTutorial;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		_currentTutorialIndex = 0;
		if(_playerData.completedTutorial == false)
		{
			_activeTutorial = true;
			gameLogicController.pauseGame();
			tutorialParts[_currentTutorialIndex].SetActive (true);
		}
	}
		
	void Update () {
		if(_activeTutorial == true)
		{
			gameLogicController._swipeDirectionController.getTouchDirection();

			if (_currentTutorialIndex == 0 && gameLogicController._swipeDirectionController.mMessageIndex == 2) {
				goToNextTutorial();
			}

			if(_currentTutorialIndex == 1 && gameLogicController._swipeDirectionController.mMessageIndex == 1)
			{
				goToNextTutorial();
			}

			if(_currentTutorialIndex == 2 && gameLogicController._swipeDirectionController.mMessageIndex == 3)
			{
				goToNextTutorial();
			}

			if(_currentTutorialIndex == 3 && gameLogicController._swipeDirectionController.mMessageIndex == 4)
			{
				_activeTutorial = false;
				tutorialParts[_currentTutorialIndex].SetActive (false);
				_playerData.completedTutorial = true;
				gameLogicController.resumeGame();
				_playerData.savePlayerData();
				GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
				analiticsController.finishGameTutorial();
			}
		}
	}

	void goToNextTutorial()
	{
		tutorialParts[_currentTutorialIndex].SetActive (false);
		_currentTutorialIndex++;
		tutorialParts[_currentTutorialIndex].SetActive (true);
	}

}
