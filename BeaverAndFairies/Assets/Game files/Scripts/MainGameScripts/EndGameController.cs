using UnityEngine;
using System.Collections;

public class EndGameController : MonoBehaviour {

	public GameObject endGamePopUp;
	public MainGameVkController vkController;
	public MainGameFbController fbController;
	public GameLogicController gameLogicController;
	public GlobalLeaderboardController globalLeaderboardController;

	public GameObject joinGameGroupPopUp;
	public GameObject inviteFriendsPopUp;
	public GameObject rateGamePopUp;

	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
	}

	void Update () {
	
	}

	public void endMainGame()
	{
		_playerData.playerScore += gameLogicController._score;
		_playerData.savePlayerData();
		endGamePopUp.SetActive(true);
		vkController.sendInVkPlayerScore(gameLogicController._score);
		fbController.sendInFbPlayerScore(gameLogicController._score);
		globalLeaderboardController.sendPlayerRecord(gameLogicController._score);

		int showSocialPopUpIndex = Random.Range(0,2);

		if(showSocialPopUpIndex == 1)
		{
			showSocialPopUp();
		}
	}

	void showSocialPopUp()
	{
		if(_playerData.showJoinGroupSuggestion == false)
		{
			joinGameGroupPopUp.SetActive(true);
			_playerData.showJoinGroupSuggestion = true;
		} 
		else if (_playerData.showInviteFriendsSuggestion == false)
		{
			inviteFriendsPopUp.SetActive(true);
			_playerData.showInviteFriendsSuggestion = true;
		}
		else if(_playerData.showReviewSuggestion == false)
		{
			rateGamePopUp.SetActive(true);
			_playerData.showReviewSuggestion = true;
		}
	}

}
