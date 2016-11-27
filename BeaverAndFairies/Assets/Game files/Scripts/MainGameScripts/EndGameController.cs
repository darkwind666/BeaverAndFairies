using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameController : MonoBehaviour {

	public GameObject endGamePopUp;
	public MainGameVkController vkController;
	public MainGameFbController fbController;
	public GameLogicController gameLogicController;
	public GlobalLeaderboardController globalLeaderboardController;
	public AdsController adsController;
	public Button doubleScoreButton;
	public Text gameResultScoreLabel;

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
		gameResultScoreLabel.text = gameLogicController._score.ToString();
		GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
		analiticsController.sendFinishLevelWithScore(gameLogicController._score);

		int showSocialPopUpIndex = Random.Range(0,2);

		if(showSocialPopUpIndex == 1)
		{
			showSocialPopUp();
		}
	}

	void showSocialPopUp()
	{
		GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
		
		if(_playerData.showJoinGroupSuggestion == false)
		{
			joinGameGroupPopUp.SetActive(true);
			_playerData.showJoinGroupSuggestion = true;
			analiticsController.showJoinGroupMainGamePopUp();
		} 
		else if (_playerData.showInviteFriendsSuggestion == false)
		{
			inviteFriendsPopUp.SetActive(true);
			_playerData.showInviteFriendsSuggestion = true;
			analiticsController.showInviteFriendsMainGamePopUp();
		}
		else if(_playerData.showReviewSuggestion == false)
		{
			rateGamePopUp.SetActive(true);
			_playerData.showReviewSuggestion = true;
			analiticsController.showRateGameMainGamePopUp();
		}
	}

	public void doubleScorePressed()
	{
		adsController.showAdditionalScoreAd();
	}

	public void getAdditionalScores()
	{
		_playerData.playerScore += gameLogicController._score;
		doubleScoreButton.gameObject.SetActive(false);
		_playerData.savePlayerData();
	}

}
