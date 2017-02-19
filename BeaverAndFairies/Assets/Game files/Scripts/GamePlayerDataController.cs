using UnityEngine;
using System.IO;

using System.Collections.Generic;
using System;

using System.Runtime.Serialization;
using System.Xml.Serialization;

#if UNITY_STANDALONE_OSX || UNITY_IOS
using System.Runtime.Serialization.Formatters.Binary;
#endif

public class GamePlayerDataController {

	string playerDataFileName;

    public bool playerExist { get; set; }
    public float gameMusicVolume { get; set; }
    public float gameSoundEffectsVolume { get; set; }
    public string playerName { get; set; }

    public int playerScore { get; set; }
    public int selectedLevelIndex { get; set; }
	public bool completedTutorial { get; set; }
    public bool showReviewSuggestion { get; set; }
	public bool showJoinGroupSuggestion { get; set; }
	public bool showInviteFriendsSuggestion { get; set; }

    public int playerStartLevelScore { get; set; }
	public bool selectEndlessLevel { get; set; }
	public int endlessLevelPlayedTime { get; set; }
	public int globalHeightScore { get; set; }
	public bool simplifyLevel { get; set; }
	public bool logInVk { get; set; }
	public bool inVkGameGroup { get; set; }
	public bool logInFb { get; set; }
	public bool inFbGameGroup { get; set; }
	public bool notNowPressed { get; set; }
	public int selectedFairyIndex { get; set; }
	public int slowBonusCount { get; set; }
	public int damageBonusCount { get; set; }
	public bool blockAdsInAppBought { get; set; }
	public bool enableSoundsEffects { get; set; }
	public bool enableBackgroundSound { get; set; }
	public bool playerUsePromocode { get; set; }

	public List<int> playerFairies;

    string _dataPath;

    List<string> _previousScenes;

    public GamePlayerDataController()
    {
		playerDataFileName = getPlayerDataFileName();
        _dataPath = Application.persistentDataPath + playerDataFileName;
        _previousScenes = new List<string>();
        loadPlayerData();
        selectedLevelIndex = 0;
    }

	string getPlayerDataFileName()
	{
		GameGlobalSettings globalSettings = ServicesLocator.getServiceForKey(typeof(GameGlobalSettings).Name) as GameGlobalSettings;
		string fileName = "/";

		if(globalSettings.paidGame)
		{
			fileName += "paid";
		}

		#if UNITY_STANDALONE_OSX || UNITY_IOS
		fileName += "playerData.bt";
		#else
		fileName += "bamPlayerData.xml";
		#endif

		return fileName;
	}

	IFormatter getDataFormatter()
	{
		IFormatter formatter = new PlayerDataXMLFormatter();

		#if UNITY_STANDALONE_OSX || UNITY_IOS
		formatter = new BinaryFormatter();
		#endif

		return formatter;
	}

    void loadPlayerData()
    {
        if(File.Exists(_dataPath))
        {
			IFormatter formatter = getDataFormatter();
            FileStream file = File.Open(_dataPath, FileMode.Open);
            PlayerData data = formatter.Deserialize(file) as PlayerData;

            playerExist = data.playerExist;
            gameMusicVolume = data.gameMusicVolume;
            gameSoundEffectsVolume = data.gameSoundEffectsVolume;
            playerScore = data.playerScore;
			endlessLevelPlayedTime = data.endlessLevelPlayedTime;
			completedTutorial = data.completedTutorial;
            showReviewSuggestion = data.showReviewSuggestion;
			showJoinGroupSuggestion = data.showJoinGroupSuggestion;
			showInviteFriendsSuggestion = data.showInviteFriendsSuggestion;
			notNowPressed = data.notNowPressed;
			logInVk = data.logInVk;
			logInFb = data.logInFb;
			inVkGameGroup = data.inVkGameGroup;
			inFbGameGroup = data.inFbGameGroup;
			selectedFairyIndex = data.selectedFairyIndex;
			playerFairies = data.playerFairies;
			slowBonusCount = data.slowBonusCount;
			damageBonusCount = data.damageBonusCount;
			blockAdsInAppBought = data.blockAdsInAppBought;
			enableSoundsEffects = data.enableSoundsEffects;
			enableBackgroundSound = data.enableBackgroundSound;
			playerUsePromocode = data.playerUsePromocode;

			if (playerFairies == null)
			{
				playerFairies = new List<int>();
			}

            file.Close();
        }
    }

    public void savePlayerData()
    {
		IFormatter formatter = getDataFormatter();
        FileStream file = File.Create(_dataPath);
        PlayerData savingData = new PlayerData();

        savingData.playerExist = playerExist;
        savingData.gameMusicVolume = gameMusicVolume;
        savingData.gameSoundEffectsVolume = gameSoundEffectsVolume;
        savingData.playerScore = playerScore;
		savingData.endlessLevelPlayedTime = endlessLevelPlayedTime;
		savingData.completedTutorial = completedTutorial;
        savingData.showReviewSuggestion = showReviewSuggestion;
		savingData.showJoinGroupSuggestion = showJoinGroupSuggestion;
		savingData.showInviteFriendsSuggestion = showInviteFriendsSuggestion;
		savingData.notNowPressed = notNowPressed;
		savingData.logInVk = logInVk;
		savingData.logInFb = logInFb;
		savingData.inVkGameGroup = inVkGameGroup;
		savingData.inFbGameGroup = inFbGameGroup;
		savingData.selectedFairyIndex = selectedFairyIndex;
		savingData.playerFairies = playerFairies;
		savingData.slowBonusCount = slowBonusCount;
		savingData.damageBonusCount = damageBonusCount;
		savingData.blockAdsInAppBought = blockAdsInAppBought;
		savingData.enableSoundsEffects = enableSoundsEffects;
		savingData.enableBackgroundSound = enableBackgroundSound;
		savingData.playerUsePromocode = playerUsePromocode;

        formatter.Serialize(file, savingData);
        file.Close();
    }

    public void createNewPlayer()
    {
        playerExist = true;
        playerScore = 60;
		endlessLevelPlayedTime = 0;
		completedTutorial = false;
        selectedLevelIndex = 0;
		selectedFairyIndex = -1;
		playerFairies = new List<int>();
        showReviewSuggestion = false;
		showJoinGroupSuggestion = false;
		showInviteFriendsSuggestion = false;
		notNowPressed = false;
		logInVk = false;
		logInFb = false;
		inVkGameGroup = false;
		inFbGameGroup = false;
		blockAdsInAppBought = false;
		enableSoundsEffects = true;
		enableBackgroundSound = true;
		playerUsePromocode = false;
		slowBonusCount = 2;
		damageBonusCount = 2;
        savePlayerData();
    }

    public void pushCurrentSceneName(string aSceneName)
    {
        _previousScenes.Add(aSceneName);
    }

    public string popPreviousScene()
    {
        string previousSceneName = _previousScenes[_previousScenes.Count - 1];
        _previousScenes.RemoveAt(_previousScenes.Count - 1);
        return previousSceneName;
    }
}

[Serializable]
public class PlayerData
{
    public bool playerExist;
    public float gameMusicVolume;
    public float gameSoundEffectsVolume;
    public string playerName;
	public bool completedTutorial;
    public int playerScore;
	public int endlessLevelPlayedTime;
    public int completedTutorialsCount;
    public bool showReviewSuggestion;
	public bool showJoinGroupSuggestion;
	public bool showInviteFriendsSuggestion;
	public bool notNowPressed;
	public bool logInVk;
	public bool logInFb;
	public bool inVkGameGroup;
	public bool inFbGameGroup;
	public int selectedFairyIndex;
	public List<int> playerFairies;
	public int slowBonusCount;
	public int damageBonusCount;
	public bool blockAdsInAppBought;
	public bool enableSoundsEffects;
	public bool enableBackgroundSound;
	public bool playerUsePromocode;
}

public class PlayerDataXMLFormatter: IFormatter
{
	public ISurrogateSelector SurrogateSelector { get; set; }
	public SerializationBinder Binder { get; set; }
	public StreamingContext Context { get; set; }

	public void Serialize(Stream serializationStream, object graph)
	{
		XmlSerializer formatter = new XmlSerializer(typeof(PlayerData));
		formatter.Serialize(serializationStream, graph);
	}

	public object Deserialize(Stream serializationStream)
	{
		XmlSerializer formatter = new XmlSerializer(typeof(PlayerData));
		PlayerData data = formatter.Deserialize(serializationStream) as PlayerData;
		return data;
	}
}
