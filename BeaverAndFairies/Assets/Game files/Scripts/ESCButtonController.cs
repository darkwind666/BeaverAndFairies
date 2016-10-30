using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ESCButtonController : MonoBehaviour {

    public FadingScript fadingController;
//	public EndlessLevelCondition endlessLevelCondition;
//	public AdsController adsController;

    const string escape = "Cancel";

    bool _escButtonPressed;

    void Start ()
    {
        _escButtonPressed = false;
    }

    void Update()
    {

        if (Input.GetButtonDown(escape) && _escButtonPressed == false)
        {
            _escButtonPressed = true;
            changeSceneForEscapeButtonClick();
        }
    }

    void changeSceneForEscapeButtonClick()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "GameLoadingScene" || currentSceneName == "MainMenuScreen")
        {
            Application.Quit();
        }
        else if (currentSceneName == "GameRecordsScene")
        {
            fadingController.goToScreen("MainMenuScreen");
        }
        else if (currentSceneName == "MainGameScreen")
        {
            exitFromMainGameScene();
        }
        else
        {
            goToPreviousScene();
        }
    }

    void exitFromMainGameScene()
    {
//        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
//        string previouseSceneName = playerData.popPreviousScene();
//		adsController.hideBottomBanner();
//
//        if (playerData.playerExist)
//        {
//			if (playerData.selectEndlessLevel) 
//			{
//				saveEndlessLevelResult();
//			} 
//
//			playerData.playerScore = playerData.playerStartLevelScore;
//			playerData.selectEndlessLevel = false;
//			playerData.savePlayerData();
//			fadingController.goToScreen(previouseSceneName);
//        }
//        else
//        {
//            fadingController.goToScreen("MainMenuScreen");
//        }
    }

	void saveEndlessLevelResult()
	{
//		GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
//		if (playerData.endlessLevelPlayedTime < endlessLevelCondition.playedTime) 
//		{
//			playerData.endlessLevelPlayedTime = endlessLevelCondition.playedTime;
//			PlayerRecordData newRecord = new PlayerRecordData (playerData.playerName, playerData.endlessLevelPlayedTime);
//			PlayersDatabaseController playersRecords = ServicesLocator.getServiceForKey (typeof(PlayersDatabaseController).Name) as PlayersDatabaseController;
//			playersRecords.saveNewPlayerRecord (newRecord);
//			globalLeaderboardController.sendPlayerRecord();
//		}
	}

    public void goToPreviousScene()
    {
//        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
//        string previouseSceneName = playerData.popPreviousScene();
//        fadingController.goToScreen(previouseSceneName);
    }

    public void pushCurrentSceneName()
    {
//        string currentSceneName = SceneManager.GetActiveScene().name;
//        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
//        playerData.pushCurrentSceneName(currentSceneName);
    }

    public void popPreviousSceneName()
    {
//        GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
//        playerData.popPreviousScene();
    }

}
