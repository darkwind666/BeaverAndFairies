using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Facebook.Unity;
using Facebook.MiniJSON;

public class FbSettingsPopUpController : MonoBehaviour {

	GamePlayerDataController _playerData;

	public Text playerName;
	public Image playerImage;

	public GameObject logInButton;
	public GameObject logInRewardText;

	public GameObject logOutButton;
	public GameObject inviteFriendsPopUp;
	public InviteFriendsController inviteFriendsController;

	public GameObject joinBeaverTimeGroupButton;
	public GameObject goToBeaverTimeGroupButton;

	public GameObject acceptOperationController;
	public Button acceptButton;

	public GameGlobalSettings gameSettings;

	void Start () {
	
		if (FB.IsLoggedIn == true) {
			logInButton.SetActive (false);
			logOutButton.SetActive (true);
			logInRewardText.SetActive (false);
			getUserInfo();
		} 
		else 
		{
			logInButton.SetActive (true);
			logOutButton.SetActive (false);
		}

	}

	void Update () {
	
	}

	void getUserInfo()
	{
		FB.API ("/me?fields=first_name", HttpMethod.GET, delegate (IGraphResult result) {
			if (result.ResultDictionary != null) {
				playerName.text = result.ResultDictionary ["first_name"].ToString();
			}

		});
	}

}
