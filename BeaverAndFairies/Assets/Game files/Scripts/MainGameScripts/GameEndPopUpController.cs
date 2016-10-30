using UnityEngine;
using System.Collections;

public class GameEndPopUpController : MonoBehaviour {

	public FadingScript fadingController;
	public GameGlobalSettings gameSettings;

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

	}
}
