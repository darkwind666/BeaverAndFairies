using UnityEngine;
using System.Collections;

public class GameEndPopUpController : MonoBehaviour {

	public FadingScript fadingController;
	public GameGlobalSettings gameSettings;
	public NativeShare shareController;
	public GameLogicController gameLogicController;

	void Start () {
	
	}

	void Update () {
	
	}

	public void backPressed()
	{
		fadingController.startFade(gameSettings.mainMenuScreenName, false);
	}

	public void replayPressed()
	{
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void sharePressed ()
	{
		string shareTextTemplate = SmartLocalization.LanguageManager.Instance.GetTextValue (gameSettings.inviteTextKey);
		string shareText = string.Format (shareTextTemplate, gameLogicController._score);
		shareController.ShareScreenshotWithText (shareText);
	}
}
