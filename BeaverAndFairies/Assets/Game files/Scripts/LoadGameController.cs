using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using Fabric.Crashlytics;

public class LoadGameController : MonoBehaviour {

    public float time;
    public FadingScript fadingController;
    public GameAnaliticsController gameAnaliticsController;
	public GameGlobalSettings gameSettings;
	public Image englishGameName;
	public Image russianGameName;
	public int startGameLoadingInterval;

    Image circularLoader;
	bool _startGameLoading;
	int _currentTimeInterval;

	void Awake ()
	{
		if (!FB.IsInitialized) {
			FB.Init(InitCallback, OnHideUnity);
		} else {
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			FB.ActivateApp();
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

    void Start ()
    {
		_currentTimeInterval = 0;
		_startGameLoading = false;
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
		//deviceCulture.languageCode = "ru";
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
		checkStartGameLoading();
		checkLoadIndicatorFillAmmount();
	}

	void checkStartGameLoading()
	{
		if(_startGameLoading == false)
		{
			_currentTimeInterval++;
			if(_currentTimeInterval == startGameLoadingInterval)
			{
				_startGameLoading = true;
			}
		}
	}

	void checkLoadIndicatorFillAmmount()
	{
		if(_startGameLoading == true)
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
