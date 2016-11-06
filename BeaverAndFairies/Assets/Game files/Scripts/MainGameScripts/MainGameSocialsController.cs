using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using Facebook.Unity;

public class MainGameSocialsController : MonoBehaviour {

	VkApi _vkapi;

	void Start () {
		_vkapi = VkApi.VkApiInstance;
	}

	void Update () {
	
	}

	public void sendInVkPlayerScore(int aScore)
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=2?value=" + aScore.ToString()  
			};

			_vkapi.Call (r1);

		}
	}

	public void sendInFbPlayerScore(int aScore)
	{
		if (FB.IsLoggedIn == true) {
			Dictionary<string,string> data = new Dictionary<string, string>();
			data["score"] = aScore.ToString();
			FB.API("/me/scores",HttpMethod.POST,Callback,data);

		} 
	}

	void Callback(IGraphResult result){}

	public void sendInVkPlayerLevel(int aLevel)
	{
		if (_vkapi.IsUserLoggedIn) {
			VKRequest r1 = new VKRequest () {
				url = "secure.addAppEvent?activity_id=1?value=" + aLevel.ToString()  
			};

			_vkapi.Call (r1);

		}
	}
}
