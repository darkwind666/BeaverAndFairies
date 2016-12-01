using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;
using Facebook.Unity;

public class GameAnaliticsController : MonoBehaviour {

	public GameAnaliticsMessages gameAnaliticsMessages;

    void Start () {

    }

	void Update () {

	}
	
    public void sendPlayerPlatformData()
    {
        SmartLocalization.LanguageManager languageManager = SmartLocalization.LanguageManager.Instance;
        SmartLocalization.SmartCultureInfo deviceCulture = languageManager.GetDeviceCultureIfSupported();

        Analytics.CustomEvent("Player data", new Dictionary<string, object> {
                                                            { "Player language", deviceCulture.nativeName},
                                                            { "Player device type", SystemInfo.deviceType.ToString()},
                                                            { "Player operation system", SystemInfo.operatingSystem},
                                                                              });
    }

    public void sendAnaliticMessage(string aMessage)
    {
        Analytics.CustomEvent(aMessage, new Dictionary<string, object> { });
		FB.LogAppEvent(aMessage);
    }

	public void sendFinishLevelWithScore(int aScore)
    {
		GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		Analytics.CustomEvent(gameAnaliticsMessages.finishGame, new Dictionary<string, object> {
			{ "Score", aScore},
			{ "LevelDifficulty", playerData.selectedLevelIndex},
		});
			
		FB.LogAppEvent(gameAnaliticsMessages.finishGame, parameters: new Dictionary<string, object> {
			{  AppEventParameterName.MaxRatingValue, aScore},
			{  AppEventParameterName.Level, playerData.selectedLevelIndex},
		});
    }

	public void createNewPlayer() { sendAnaliticMessage(gameAnaliticsMessages.createNewPlayer);}
	public void playPressed() { sendAnaliticMessage(gameAnaliticsMessages.playPressed);}
	public void recordsPressed() { sendAnaliticMessage(gameAnaliticsMessages.recordsPressed);}
	public void shopPressed() { sendAnaliticMessage(gameAnaliticsMessages.shopPressed);}
	public void settingsPressed() { sendAnaliticMessage(gameAnaliticsMessages.settingsPressed);}
	public void exitPressed() { sendAnaliticMessage(gameAnaliticsMessages.exitPressed);}
	public void showSocialsLogInPopUp() { sendAnaliticMessage(gameAnaliticsMessages.showSocialsLogInPopUp);}
	public void logInVkPressed() { sendAnaliticMessage(gameAnaliticsMessages.logInVkPressed);}
	public void logInFbPressed() { sendAnaliticMessage(gameAnaliticsMessages.logInFbPressed);}
	public void notNowPressed() { sendAnaliticMessage(gameAnaliticsMessages.notNowPressed);}
	public void rateGamePressed() { sendAnaliticMessage(gameAnaliticsMessages.rateGamePressed);}
	public void vkSettingsPressed() { sendAnaliticMessage(gameAnaliticsMessages.vkSettingsPressed);}
	public void fbSettingsPressed() { sendAnaliticMessage(gameAnaliticsMessages.fbSettingsPressed);}
	public void buyBlockAdPressed() { sendAnaliticMessage(gameAnaliticsMessages.buyBlockAdPressed);}
	public void showAdForBlockAdPressed() { sendAnaliticMessage(gameAnaliticsMessages.showAdForBlockAdPressed);}
	public void goToVkGameGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.goToVkGameGroupPressed);}
	public void joinVkGameGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.joinVkGameGroupPressed);}
	public void inviteVkFriendsPressed() { sendAnaliticMessage(gameAnaliticsMessages.inviteVkFriendsPressed);}
	public void allowMessagesPressed() { sendAnaliticMessage(gameAnaliticsMessages.allowMessagesPressed);}
	public void goToVkGamesGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.goToVkGamesGroupPressed);}
	public void joinVkGamesGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.joinVkGamesGroupPressed);}
	public void addDeveloperToFriendPressed() { sendAnaliticMessage(gameAnaliticsMessages.addDeveloperToFriendPressed);}
	public void logOutVkPressed() { sendAnaliticMessage(gameAnaliticsMessages.logOutVkPressed);}
	public void goToFbGameGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.goToFbGameGroupPressed);}
	public void joinFbGameGroupPressed() { sendAnaliticMessage(gameAnaliticsMessages.joinFbGameGroupPressed);}
	public void inviteFbFriendsPressed() { sendAnaliticMessage(gameAnaliticsMessages.inviteFbFriendsPressed);}
	public void logOutFbPressed() { sendAnaliticMessage(gameAnaliticsMessages.logOutFbPressed);}
	public void buySlowBonusPressed() { sendAnaliticMessage(gameAnaliticsMessages.buySlowBonusPressed);}
	public void buyDamageBonusPressed() { sendAnaliticMessage(gameAnaliticsMessages.buyDamageBonusPressed);}
	public void buyFairyPressed() { sendAnaliticMessage(gameAnaliticsMessages.buyFairyPressed);}
	public void showVideoForScoresPressed() { sendAnaliticMessage(gameAnaliticsMessages.showVideoForScoresPressed);}
	public void goToInAppPurchesesShopPressed() { sendAnaliticMessage(gameAnaliticsMessages.goToInAppPurchesesShopPressed);}
	public void buyScoresCount1Pressed() { sendAnaliticMessage(gameAnaliticsMessages.buyScoresCount1Pressed);}
	public void buyScoresCount2Pressed() { sendAnaliticMessage(gameAnaliticsMessages.buyScoresCount2Pressed);}
	public void buyScoresCount3Pressed() { sendAnaliticMessage(gameAnaliticsMessages.buyScoresCount3Pressed);}
	public void buyScoresCount4Pressed() { sendAnaliticMessage(gameAnaliticsMessages.buyScoresCount4Pressed);}
	public void buyScoresCount5Pressed() { sendAnaliticMessage(gameAnaliticsMessages.buyScoresCount5Pressed);}
	public void userBuyScoresCount1() { sendAnaliticMessage(gameAnaliticsMessages.userBuyScoresCount1);}
	public void userBuyScoresCount2() { sendAnaliticMessage(gameAnaliticsMessages.userBuyScoresCount2);}
	public void userBuyScoresCount3() { sendAnaliticMessage(gameAnaliticsMessages.userBuyScoresCount3);}
	public void userBuyScoresCount4() { sendAnaliticMessage(gameAnaliticsMessages.userBuyScoresCount4);}
	public void userBuyScoresCount5() { sendAnaliticMessage(gameAnaliticsMessages.userBuyScoresCount5);}
	public void userBuyBlockAd() { sendAnaliticMessage(gameAnaliticsMessages.userBuyBlockAd);}
	public void restoreInAppPurchesesPressed() { sendAnaliticMessage(gameAnaliticsMessages.restoreInAppPurchesesPressed);}
	public void acceptPressed() { sendAnaliticMessage(gameAnaliticsMessages.acceptPressed);}
	public void selectEasyGameMode() { sendAnaliticMessage(gameAnaliticsMessages.selectEasyGameMode);}
	public void selectMediumGameMode() { sendAnaliticMessage(gameAnaliticsMessages.selectMediumGameMode);}
	public void selectHardGameMode() { sendAnaliticMessage(gameAnaliticsMessages.selectHardGameMode);}
	public void mainGamePausePressed() { sendAnaliticMessage(gameAnaliticsMessages.mainGamePausePressed);}
	public void resumeGamePressed() { sendAnaliticMessage(gameAnaliticsMessages.resumeGamePressed);}
	public void replayGamePressed() { sendAnaliticMessage(gameAnaliticsMessages.replayGamePressed);}
	public void shareGameResultPressed() { sendAnaliticMessage(gameAnaliticsMessages.shareGameResultPressed);}
	public void doubleScorePressed() { sendAnaliticMessage(gameAnaliticsMessages.doubleScorePressed);}
	public void finalChancePressed() { sendAnaliticMessage(gameAnaliticsMessages.finalChancePressed);}
	public void useSlowBonusPressed() { sendAnaliticMessage(gameAnaliticsMessages.useSlowBonusPressed);}
	public void useDamageBonusPressed() { sendAnaliticMessage(gameAnaliticsMessages.useDamageBonusPressed);}
	public void finishGameTutorial() { sendAnaliticMessage(gameAnaliticsMessages.finishGameTutorial);}

	public void joinVkGroupFromMainGamePressed() { sendAnaliticMessage(gameAnaliticsMessages.joinVkGroupFromMainGamePressed);}
	public void joinFbGroupFromMainGamePressed() { sendAnaliticMessage(gameAnaliticsMessages.joinFbGroupFromMainGamePressed);}

	public void showJoinGroupMainGamePopUp() { sendAnaliticMessage(gameAnaliticsMessages.showJoinGroupMainGamePopUp);}
	public void showInviteFriendsMainGamePopUp() { sendAnaliticMessage(gameAnaliticsMessages.showInviteFriendsMainGamePopUp);}
	public void showRateGameMainGamePopUp() { sendAnaliticMessage(gameAnaliticsMessages.showRateGameMainGamePopUp);}

	public void inviteVkFriendsFromMainGame() { sendAnaliticMessage(gameAnaliticsMessages.inviteVkFriendsFromMainGame);}
	public void inviteFbFriendsFromMainGame() { sendAnaliticMessage(gameAnaliticsMessages.inviteFbFriendsFromMainGame);}

	public void rateGameFromMainGame() { sendAnaliticMessage(gameAnaliticsMessages.rateGameFromMainGame);}
	public void rateGameFromGameSettings() { sendAnaliticMessage(gameAnaliticsMessages.rateGameFromGameSettings);}
	public void contactDevelopersFromMainGame() { sendAnaliticMessage(gameAnaliticsMessages.contactDevelopersFromMainGame);}
	public void contactDevelopersFromGameSettings() { sendAnaliticMessage(gameAnaliticsMessages.contactDevelopersFromGameSettings);}
	public void showDevelopersPressed() { sendAnaliticMessage(gameAnaliticsMessages.showDevelopersPressed);}

}
