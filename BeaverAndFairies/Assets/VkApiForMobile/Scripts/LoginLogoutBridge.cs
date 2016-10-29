using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using System.Runtime.InteropServices;


public class LoginLogoutBridge  {

    //VkSettings vsetts=VkApi.VkSetts;

#if UNITY_WSA && !UNITY_EDITOR
	public delegate void LoginDelegate(List<string>scope,int appid,bool forceOAuth);
	public delegate void LogoutDelegate();

	public LogoutDelegate OnWSALogout{get;set;}
	public LoginDelegate OnWSALogin{get;set;}

	public void Login()
	{
		VkSettings vsetts=VkApi.VkSetts;
		if (vsetts.forceOAuth) {
			WSA_WebViewLogin ();
			return;
		}
		if (OnWSALogin != null) {
			OnWSALogin(vsetts.scope,vsetts.VkAppId,vsetts.forceOAuth);
		}
	}
	public void WSA_WebViewLogin(){
		WebViewAuth ();
	}

	public void Logout()
	{
		if (OnWSALogout != null) {
			VkApi.VkApiInstance.onLoggedOut ();
			OnWSALogout();
		}
	}
#endif

#if UNITY_IOS && !UNITY_EDITOR
	private bool loginInProgress;
	[DllImport("__Internal")]
	private static extern void _VkAuthorization(string authUrl);
	
	[DllImport("__Internal")]
	private static extern void _doLogOutUser();

	[DllImport("__Internal")]
	private static extern bool _IsVkAppPresent();

	private void BackToAppFix(){
		try {
			if (!VkApi.VkSetts.forceOAuth || _IsVkAppPresent())
			{
				VkApi.VkApiInstance.LoggedIn -= BackToAppFix;
			}

		} catch (Exception ex) {
			
		}
		loginInProgress = false;
	}
	
	public void OnApplicationFocus(bool focus,GameObject mVkApi){
		if (focus && loginInProgress) {
			mVkApi.GetComponent<MessageHandler>().AccessDeniedMessage("1#AuthorizationFailed");
		}	
	}
	public void Login()
	{
		if (VkApi.VkSetts.forceOAuth || !_IsVkAppPresent())
		{
			WebViewAuth();
			return;
		}
		VkApi.VkApiInstance.LoggedIn += BackToAppFix;
		loginInProgress = true;
		_VkAuthorization (FormLoginUrl());
	}

	public void Logout()
	{
		VkApi.VkApiInstance.onLoggedOut ();
		_doLogOutUser ();
	}

#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	AndroidJavaObject jo;
	public void Login()
	{
	   
	    //com.playgenesis.vkunityplugin.
		jo = new AndroidJavaObject ("com.playgenesis.vkunityplugin.Initializer"); 
		var loginUrl = FormLoginUrl ();
		var isVkAppPresent =jo.CallStatic<bool>("isVkAppPresent");

		if (VkApi.VkSetts.forceOAuth || !isVkAppPresent)
		{
			WebViewAuth();
			return;
		}
		jo.Set<String> ("urlBase64", loginUrl);

		jo.Call ("Init");
	}

    public void Logout()
	{
		using (AndroidJavaObject jo = new AndroidJavaObject ("com.playgenesis.vkunityplugin.Initializer")) 
		{
			VkApi.VkApiInstance.onLoggedOut ();
			jo.Call ("Logout",VkApi.VkSetts.VkAppId.ToString());
		}
        
	}
#endif

#if UNITY_EDITOR

    public void Login()
	{
        
        var currentToken = VkApi.CurrentToken;
		var VkSetts = VkApi.VkSetts;
		VkSetts.ProcessAuthUrl ();
		String fakeSerializedResponseFormSdk=currentToken.access_token+"#"+currentToken.expires_in+"#"+currentToken.user_id;

		GameObject.FindObjectOfType<MessageHandler>().ReceiveNewTokenMessage(fakeSerializedResponseFormSdk);
		
	}
	public void Logout()
	{
		VkApi.VkApiInstance.onLoggedOut ();
	}
	#endif
	private void WebViewAuth()
	{
		var r = new WebViewRequest
		{
			NavigateToUrl = FormLoginUrl(),
			CloseWhenNavigatedToUrl = "https://oauth.vk.com/blank.html",
			CallbackAction = (w) =>{
				if (w.Error != null)
				{
					VkApi.VkApiInstance.SendMessage("AccessDeniedMessage", "-1#Canceled by user");
				}
				else
				{
					VkApi.VkApiInstance.SendMessage("ReceiveNewTokenMessage",
					                                VKToken.ParseFromAuthUrl(w.LastUrlWithParams));
				}
			}
		};
		WebView.Instance.Add(r);
	}
	public string FormLoginUrl()
	{
	    var VkSetts = Resources.Load<VkSettings>("VkSettings");
        var scope=string.Join(",", VkSetts.scope.ToArray());
		
		
		var url = "https://oauth.vk.com/authorize?client_id=" + VkSetts.VkAppId +
			"&scope=" + scope +
				"&redirect_uri=https://oauth.vk.com/blank.html&display=mobile" +
				"&forceOAuth="+ VkSetts.forceOAuth.ToString()+
				"&revokeAccess="+ VkSetts.revoke.ToString ()+
				"&v="+ VkSetts.apiVersion +
				"&response_type=token";
		return url;
		
	}
}
