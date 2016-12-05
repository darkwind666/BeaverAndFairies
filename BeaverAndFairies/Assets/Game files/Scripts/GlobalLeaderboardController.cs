using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class GlobalLeaderboardController : MonoBehaviour {

	public GameGlobalSettings gameSettings;
	public bool startGame;

	GamePlayerDataController _playerData;

	void Start () {

		#if UNITY_IOS
		gameSettings.googlePlayLeaderboardId = "grp." + gameSettings.googlePlayLeaderboardId;
		#endif 

		#if UNITY_ANDROID
		GooglePlayGames.PlayGamesPlatform.Activate();
		#endif

		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		if(startGame)
		{
			Social.localUser.Authenticate (logInDebug);
		}
	}

	void Update () {
	
	}

	void logInDebug(bool success)
	{
		Debug.Log("success: " + success);

		if(success)
		{
			setHeightsScore();
		}
	}

	public void setHeightsScore()
	{
		ILeaderboard  highScoresBoard = Social.CreateLeaderboard();
		highScoresBoard.id = gameSettings.googlePlayLeaderboardId;
		highScoresBoard.LoadScores((bool success) => {

			if(highScoresBoard.scores.Length > 0)
			{
				int score = (int)highScoresBoard.scores[0].value;
				_playerData.globalHeightScore = score;
			}

		});
	}

	public void sendPlayerRecord(int aRecord)
	{
		Social.ReportScore(aRecord, gameSettings.googlePlayLeaderboardId, onReportScore);
	}

	private void onReportScore(bool result)
	{
		Debug.Log("success: " + result);
	}

	public void showGlobalLeaderboard()
	{
		#if UNITY_ANDROID
		(Social.Active as GooglePlayGames.PlayGamesPlatform).SetDefaultLeaderboardForUI(gameSettings.googlePlayLeaderboardId);
		#endif

		Social.ShowLeaderboardUI();
	}
}
