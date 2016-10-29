using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using com.playGenesis.VkUnityPlugin;
using System.Collections.Generic;

[CustomEditor(typeof(VkSettings))]
public class VkSettingsEditor : Editor {

	public static string auth_url{ 
		get{
			return PlayerPrefs.GetString("auth_url","");} 

		set{
			PlayerPrefs.SetString("auth_url",value);
		}}

	public bool myFold = true;
	public string status = "Select a GameObject";
	public VkSettings myScript;
	string token;


	public override void OnInspectorGUI()
	{
		myScript = (VkSettings)target;
		//DrawDefaultInspector();
		//EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
		myScript.VkAppId=EditorGUILayout.IntField("Vk App Id",myScript.VkAppId);

		if(myScript.VkAppId==0)
			EditorGUILayout.HelpBox("Plese enter you vk app id",MessageType.Warning);

		EditorGUILayout.HelpBox("Avoids using vk app for auth",MessageType.None);
		myScript.forceOAuth = EditorGUILayout.Toggle(new GUIContent("ForceOauth"),myScript.forceOAuth);

		EditorGUILayout.HelpBox("If checked user will have to confirm scope on each auth",MessageType.None);
		myScript.revoke = EditorGUILayout.Toggle(new GUIContent("Revoke"),myScript.revoke);

		myFold = EditorGUILayout.Foldout(myFold,"Scope");
		if(myFold)
		{
			myScript.ads = EditorGUILayout.Toggle(new GUIContent("Ads"),myScript.ads);
			myScript.audio=EditorGUILayout.Toggle(new GUIContent("Audio"),myScript.audio);
			myScript.docs= EditorGUILayout.Toggle(new GUIContent("Docs"),myScript.docs);
			myScript.friends=EditorGUILayout.Toggle(new GUIContent("Friends"),myScript.friends);
			myScript.groups=EditorGUILayout.Toggle(new GUIContent("Groups"),myScript.groups);
			myScript.messages=EditorGUILayout.Toggle(new GUIContent("Messages"),myScript.messages);
			myScript.nohttps=EditorGUILayout.Toggle(new GUIContent("Nohttps"),myScript.nohttps);
			myScript.notes=EditorGUILayout.Toggle(new GUIContent("Notes"),myScript.notes);
			myScript.notifications=EditorGUILayout.Toggle(new GUIContent("notifications"),myScript.notifications);
			myScript.notify=EditorGUILayout.Toggle(new GUIContent("Notify"),myScript.notify);
			myScript.offers=EditorGUILayout.Toggle(new GUIContent("Offers"),myScript.offers);
			myScript.offline=EditorGUILayout.Toggle(new GUIContent("Offline"),myScript.offline);
			myScript.pages=EditorGUILayout.Toggle(new GUIContent("Pages"),myScript.pages);
			myScript.photos=EditorGUILayout.Toggle(new GUIContent("Photos"),myScript.photos);
			myScript.questions=EditorGUILayout.Toggle(new GUIContent("Questions"),myScript.questions);
			myScript.stats=EditorGUILayout.Toggle(new GUIContent("Stats"),myScript.stats);
			myScript.status=EditorGUILayout.Toggle(new GUIContent("Status"),myScript.status);
			myScript.video=EditorGUILayout.Toggle(new GUIContent("Video"),myScript.video);
			myScript.wall=EditorGUILayout.Toggle(new GUIContent("Wall"),myScript.wall);
		}
		EditorGUILayout.HelpBox("for example 5.33",MessageType.Info);
		myScript.apiVersion=EditorGUILayout.TextField("api version",myScript.apiVersion);

		EditorGUILayout.HelpBox("To make the plugin work in editor you need click to the button below," +
			"it will open the web browser and after you confirm you will be redirected to the blanck page, " +
			"copy the url and paste it to the field \"auth url\"",MessageType.Info);
		auth_url=EditorGUILayout.TextField("auth url",auth_url);
		if(GUILayout.Button("Connect editor to vk"))
		{

			if(myScript.VkAppId!=0)
			{
				myScript.generateScope();
				var url="https://oauth.vk.com/authorize?client_id="+myScript.VkAppId +
					"&scope="+string.Join(",",myScript.scope.ToArray())+"&" +
					"redirect_uri=https://oauth.vk.com/blank.html&" +
					"display=popup&" +
					"v=5.29&" +
					"response_type=token";
				url=Uri.EscapeUriString(url);
				EditorUtility.DisplayDialog("Connection to vk","If you change scope you need to reconnect","ok");
				Application.OpenURL(url);
			}else
			{
				EditorUtility.DisplayDialog("Error","Please,enter vk app id","ok");
			}
		}

		if(GUILayout.Button("Check if it works"))
		{
			var token=parseToenFromString().access_token;
			 var s="https://api.vk.com/method/users.get?fields=photo_200&v=5.29&access_token="+token;
			Application.OpenURL(s);
				
		}

		if(GUI.changed)
		{
			EditorUtility.SetDirty(myScript);
		}
	}
	public VKToken parseToenFromString()
	{
		var authUrl=auth_url;
		string[] firstsplit=authUrl.Split('#');
		string[] secondsplit=firstsplit[1].Split('&');
		
		var tokeninfo = new Dictionary<string,string> ();
		
		foreach (var secondsplitemevent in secondsplit)
		{
			string[] thirdsplit=secondsplitemevent.Split('=');
			tokeninfo.Add(thirdsplit[0],thirdsplit[1]);
		}
		VKToken ti1=new VKToken();
		
		int outvar = 99999999;
		ti1.access_token = tokeninfo ["access_token"];
		ti1.expires_in = int.TryParse (tokeninfo ["expires_in"], out outvar) ? outvar : outvar;
		if(outvar==0)
		{
			ti1.expires_in=9999999;
		}
		ti1.user_id = tokeninfo ["user_id"];
		ti1.tokenRecievedTime = DateTime.Now;
		
		return ti1;
	
	}

}
