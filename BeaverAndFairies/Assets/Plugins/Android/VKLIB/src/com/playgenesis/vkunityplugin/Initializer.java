package com.playgenesis.vkunityplugin;

import android.app.Activity;
import android.content.Intent;
import android.text.TextUtils;

import java.util.Dictionary;
import java.util.HashMap;
import java.util.Map;

import com.unity3d.player.UnityPlayer;
import com.vk.sdk.util.VKUtil;

public class Initializer
{
	
	boolean forceOAuth; 
	boolean webViewIsBusy;
  public String urlBase64; 
  private static Initializer defaultInitializer; 
  public static Initializer GetDefaultInitializer(){
	if(defaultInitializer==null){
		defaultInitializer=new Initializer();
	}  
	return defaultInitializer;
  }
  public void Init()//(String _data)
  {
	 String _data=urlBase64;
	 
   // Activity current = UnityPlayer.currentActivity;
    String[] parameters = _data.split("\\?");
    String[] paramAndValue=parameters[1].split("&");
    Map<String, String> initData = new HashMap<String, String>();
    for(int i=0;i<paramAndValue.length;i++){
    	initData.put(paramAndValue[i].split("=")[0],paramAndValue[i].split("=")[1]);
    }
    forceOAuth=Boolean.parseBoolean(initData.get("forceOAuth"));
    
    Intent i = new Intent(UnityPlayer.currentActivity, LoginLogout.class);
    i.putExtra("forceOAuth", Boolean.parseBoolean(initData.get("forceOAuth")));
    i.putExtra("scope", initData.get("scope"));
    i.putExtra("appId", initData.get("client_id"));
    i.putExtra("revoke", Boolean.parseBoolean(initData.get("revokeAccess")));
    if(forceOAuth){
    	String openUrl="https://oauth.vk.com/authorize?client_id="+initData.get("client_id")
    			+ "&display=mobile"
    			+ "&redirect_uri=https://oauth.vk.com/blank.html"
    			+ "&response_type=token"
    			+ "&v=5.40"
    			+ "&scope="+initData.get("forceOAuth");
    	String closeUrl="https://oauth.vk.com/blank.html";
    	webViewIsBusy=true;
    	OpenWebView(openUrl, closeUrl);
    			
    }else{
    	UnityPlayer.currentActivity.startActivity(i);
    }
    
  }
  public static boolean isVkAppPresent(){
	  String VK_APP_PACKAGE_ID = "com.vkontakte.android";
	  return VKUtil.isAppInstalled(UnityPlayer.currentActivity.getApplicationContext(), VK_APP_PACKAGE_ID);
	  
  }
  public void Logout(String message)
  {
	
    Intent i = new Intent(UnityPlayer.currentActivity, LoginLogout.class);
    i.putExtra("logout", true);
    i.putExtra("appId", message);
    UnityPlayer.currentActivity.startActivity(i);
  }
  public void OpenWebView(String openUrl,String closeUrl )
  {
	
    Intent i = new Intent(UnityPlayer.currentActivity, WebViewActivity.class);
    i.putExtra("openUrl", openUrl);
    i.putExtra("closeUrl", closeUrl);
    UnityPlayer.currentActivity.startActivity(i);
  }
}
