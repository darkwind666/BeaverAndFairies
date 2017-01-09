using UnityEngine;
using UnityEngine.UI;

public class LoadGameController : MonoBehaviour {

    public float time;
    public FadingScript fadingController;
    public GameAnaliticsController gameAnaliticsController;
	public GameGlobalSettings gameSettings;
	public Image englishGameName;
	public Image russianGameName;

    Image circularLoader;

    void Start ()
    {
        circularLoader = GetComponent<Image>();
        circularLoader.fillAmount = 0f;
        gameAnaliticsController.sendPlayerPlatformData();
        setUpGameLanguage();
		setUpGameName();
    }

    void setUpGameLanguage()
    {
        SmartLocalization.LanguageManager languageManager = SmartLocalization.LanguageManager.Instance;
        SmartLocalization.SmartCultureInfo deviceCulture = languageManager.GetDeviceCultureIfSupported();
        if (deviceCulture != null)
        {
            languageManager.ChangeLanguage(deviceCulture);
        }
        SmartLocalization.LanguageManager.SetDontDestroyOnLoad();
    }

	void setUpGameName()
	{
		SmartLocalization.LanguageManager languageManager = SmartLocalization.LanguageManager.Instance;
		SmartLocalization.SmartCultureInfo deviceCulture = languageManager.CurrentlyLoadedCulture;

		if(deviceCulture.languageCode == "ru")
		{
			russianGameName.gameObject.SetActive(true);
			englishGameName.gameObject.SetActive(false);
		} else {
			russianGameName.gameObject.SetActive(false);
			englishGameName.gameObject.SetActive(true);
		}
	}

    void Update ()
    {
        if (circularLoader.fillAmount < 1f)
        {
            circularLoader.fillAmount += Time.deltaTime / time;
        }
        else
        {
			fadingController.startFade(gameSettings.mainMenuScreenName, false);
			ServicesLocator.setServiceForKey(gameSettings, typeof(GameGlobalSettings).Name);
            ServicesLocator.loadGameServices();
			createNewPlayer();
            enabled = false;
        }
	}

	void createNewPlayer()
	{
		GamePlayerDataController playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		if (playerData.playerExist == false) {
			playerData.createNewPlayer();
			playerData.savePlayerData();
		}
	}

}
