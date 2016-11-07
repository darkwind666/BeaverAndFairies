using UnityEngine;
using System.Collections;

public class GameEndPopUpController : MonoBehaviour {

	public FadingScript fadingController;
	public GameGlobalSettings gameSettings;
	public NativeShare shareController;

	void Start () {
	
	}

	void Update () {
	
	}

	public void backPressed()
	{
		fadingController.startFade(gameSettings.selectLevelSceneName, false);
	}

	public void replayPressed()
	{
		fadingController.startFade(gameSettings.mainGameScreenName, false);
	}

	public void sharePressed()
	{
		shareController.ShareScreenshotWithText("Sasha");
	}
}
