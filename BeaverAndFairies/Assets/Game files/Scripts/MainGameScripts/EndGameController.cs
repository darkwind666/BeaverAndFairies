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
	public GameGlobalSettings gameGlobalSettings;

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
		gameLogicController.saveCollectedPlayerScore();
		endGamePopUp.SetActive(true);
		vkController.sendInVkPlayerScore(gameLogicController._score);
		fbController.sendInFbPlayerScore(gameLogicController._score);
		globalLeaderboardController.sendPlayerRecord(gameLogicController._score);
		gameResultScoreLabel.text = gameLogicController._score.ToString();
		GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
		analiticsController.sendFinishLevelWithScore(gameLogicController._score);

		int showSocialPopUpIndex = Random.Range(0,2);

		if (showSocialPopUpIndex == 1) {
			trySubscribeUsersForPushes();
			showSocialPopUp ();
		} else {
			adsController.tryShowInterstitial();
		}
	}

	void trySubscribeUsersForPushes()
	{
		#if UNITY_IOS

		if(gameLogicController._score >= 100) {
			if(gameGlobalSettings.paidGame){
				OneSignal.StartInit(gameGlobalSettings.hdOneSignalId, gameGlobalSettings.hdOneSignalProjectNumber).EndInit();
			} else {
				OneSignal.StartInit(gameGlobalSettings.freeOneSignalId, gameGlobalSettings.freeOneSignalProjectNumber).EndInit();;
			}
		}

		#endif
	}

	void showSocialPopUp()
	{
		GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
		
		if(_playerData.showJoinGroupSuggestion == false && (_playerData.inVkGameGroup == false || _playerData.inFbGameGroup == false) && gameLogicController._score >= 100)
		{
			joinGameGroupPopUp.SetActive(true);
			_playerData.showJoinGroupSuggestion = true;
			analiticsController.showJoinGroupMainGamePopUp();
		} 
		else if (_playerData.showInviteFriendsSuggestion == false && gameLogicController._score >= 100)
		{
			inviteFriendsPopUp.SetActive(true);
			_playerData.showInviteFriendsSuggestion = true;
			analiticsController.showInviteFriendsMainGamePopUp();
		}
		else if(_playerData.showReviewSuggestion == false && gameLogicController._score >= 100)
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
