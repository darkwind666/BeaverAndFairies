using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreLabelController : MonoBehaviour {

	public Text playerScoreLabel;

	GamePlayerDataController _playerData;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
	}

	void Update () {
		playerScoreLabel.text = _playerData.playerScore.ToString();
	}
}
