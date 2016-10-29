using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections.Generic;

public class vkcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (VkApi.VkApiInstance.IsUserLoggedIn) {
			onLoggedIn ();
		} else {
			VkApi.VkApiInstance.LoggedIn+=onLoggedIn;
			VkApi.VkApiInstance.Login ();
		}


	}
	public void onLoggedIn(){
		try {
			VkApi.VkApiInstance.LoggedIn-=onLoggedIn;
		} catch (System.Exception ex) {
			
		}
		VKRequest r = new VKRequest
		{
			url="users.get?user_ids=205387401&photo_50",
			CallBackFunction=OnGotUserInfo
		};
		VkApi.VkApiInstance.Call (r);

	}
	public void OnGotUserInfo (VKRequest r)
	{
		if(r.error!=null)
		{
			//hande error here
			Debug.Log(r.error.error_msg);
			return;
		}

		//now we need to deserialize response in json from vk server
		var dict=Json.Deserialize(r.response) as Dictionary<string,object>;
		var users=(List<object>)dict["response"];
		var user = VKUser.Deserialize (users [0]);

		Debug.Log ("user id is " + user.id);
		Debug.Log ("user name is " + user.first_name);
		Debug.Log ("user last name is " + user.last_name);

	}

}
