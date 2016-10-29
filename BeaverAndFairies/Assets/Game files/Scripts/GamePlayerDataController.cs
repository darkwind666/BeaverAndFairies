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

    public int completedLevelsCount { get; set; }
    public int playerScore { get; set; }
    public int selectedLevelIndex { get; set; }
    public int completedTutorialsCount { get; set; }
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
	public bool notNowPressed { get; set; }


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
		string fileName = "/playerData.xml";

		#if UNITY_STANDALONE_OSX || UNITY_IOS
		fileName = "/playerData.bt";
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
            playerName = data.playerName;
            completedLevelsCount = data.completedLevelsCount;
            playerScore = data.playerScore;
			endlessLevelPlayedTime = data.endlessLevelPlayedTime;
            completedTutorialsCount = data.completedTutorialsCount;
            showReviewSuggestion = data.showReviewSuggestion;
			showJoinGroupSuggestion = data.showJoinGroupSuggestion;
			showInviteFriendsSuggestion = data.showInviteFriendsSuggestion;
			logInVk = data.logInVk;
			inVkGameGroup = data.inVkGameGroup;

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
        savingData.playerName = playerName;
        savingData.completedLevelsCount = completedLevelsCount;
        savingData.playerScore = playerScore;
		savingData.endlessLevelPlayedTime = endlessLevelPlayedTime;
        savingData.completedTutorialsCount = completedTutorialsCount;
        savingData.showReviewSuggestion = showReviewSuggestion;
		savingData.showJoinGroupSuggestion = showJoinGroupSuggestion;
		savingData.showInviteFriendsSuggestion = showInviteFriendsSuggestion;
		savingData.logInVk = logInVk;
		savingData.inVkGameGroup = inVkGameGroup;

        formatter.Serialize(file, savingData);
        file.Close();
    }

    public void createNewPlayerWithName(string aPlayerName)
    {
        playerExist = true;
        playerName = aPlayerName;
        completedLevelsCount = 0;
        playerScore = 60;
		endlessLevelPlayedTime = 0;
        completedTutorialsCount = 0;
        selectedLevelIndex = 0;
        completedTutorialsCount = 0;
        showReviewSuggestion = false;
		showJoinGroupSuggestion = false;
		showInviteFriendsSuggestion = false;
		logInVk = false;
		inVkGameGroup = false;
        savePlayerData();
    }

    public void cleanPlayer()
    {
        playerExist = false;
        playerName = "";
        completedLevelsCount = 0;
        playerScore = 0;
		endlessLevelPlayedTime = 0;
        gameMusicVolume = 0.5f;
        gameSoundEffectsVolume = 0.5f;
        selectedLevelIndex = 0;
        completedTutorialsCount = 0;
		showJoinGroupSuggestion = false;
		showReviewSuggestion = false;
		showInviteFriendsSuggestion = false;
		logInVk = false;
		inVkGameGroup = false;
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
    public int completedLevelsCount;
    public int playerScore;
	public int endlessLevelPlayedTime;
    public int completedTutorialsCount;
    public bool showReviewSuggestion;
	public bool showJoinGroupSuggestion;
	public bool showInviteFriendsSuggestion;
	public bool logInVk;
	public bool inVkGameGroup;
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
