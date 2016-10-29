package com.playgenesis.vkunityplugin;


import android.R.string;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.vk.sdk.VKAccessToken;
import com.vk.sdk.VKAccessTokenTracker;
import com.vk.sdk.VKCallback;
import com.vk.sdk.VKSdk;
import com.vk.sdk.VKUIHelper;
import com.vk.sdk.api.VKError;
import com.vk.sdk.dialogs.VKCaptchaDialog;

public class LoginLogout extends Activity
{
  String[] scope;
  private boolean isResumed = false;
  private boolean canceledByUser;
  VKAccessTokenTracker vkAccessTokenTracker = new VKAccessTokenTracker() {
      @Override
      public void onVKAccessTokenChanged(VKAccessToken oldToken, VKAccessToken newToken) {
          if (newToken == null) {
              Toast.makeText(LoginLogout.this, "AccessToken invalidated", Toast.LENGTH_LONG).show();
          }
      }
  };
  
  protected void onCreate(Bundle savedInstanceState)
  {
    super.onCreate(savedInstanceState);
    vkAccessTokenTracker.startTracking();
    
    Intent arg0 = getIntent();
    String appId = arg0.getStringExtra("appId");
    String scopeSerialized = arg0.getStringExtra("scope");
    if(scopeSerialized!=null){
    	scope=scopeSerialized.split(",");
    }
    Boolean forceOAuth = Boolean.valueOf(arg0.getBooleanExtra("forceOAuth", true));
    Boolean revoke = Boolean.valueOf(arg0.getBooleanExtra("revoke", true));
    Boolean logout = Boolean.valueOf(arg0.getBooleanExtra("logout", false));
    
    VKSdk.customInitialize(LoginLogout.this,Integer.parseInt(appId),"5.40").withPayments();
    
    if(!logout){
	    VKSdk.wakeUpSession(this, new VKCallback<VKSdk.LoginState>() {
	        @Override
	        public void onResult(VKSdk.LoginState res) {
	            if (isResumed) {
	                switch (res) {
	                    case LoggedOut:
	                    	ShowLogin();
	                        break;
	                    case LoggedIn:
	                        SendTokenToUnity();
	                        break;
	                    case Pending:
	                        break;
	                    case Unknown:
	                        break;
	                }
	            }
	        }
	
	        @Override
	        public void onError(VKError error) {
	        	onAccessDenied(error);
	        }
	    });
    }else
    {
    	try {
	    	  VKSdk.logout();
		} catch (Exception e) {
			
		}
        finish();
    }
  }
  
  public void SendTokenToUnity()
  {
	VKAccessToken token =VKSdk.getAccessToken();
	String tokenserialized = token.accessToken + "#" + 
		                       token.expiresIn + "#" + 
		                       token.userId;
		    
	UnityPlayer.UnitySendMessage("VkApi", "ReceiveNewTokenMessage", tokenserialized);
	LoginLogout.this.finish();
   
  }
  
  public void ShowLogin()
  {
	  canceledByUser=true;
	  VKSdk.login(LoginLogout.this, scope);
	 
  }
  
  public void onAccessDenied(VKError authorizationError)
  {
    String error_serialized = authorizationError.errorCode + "#" + 
      authorizationError.errorMessage;
    UnityPlayer.UnitySendMessage("VkApi", "AccessDeniedMessage", error_serialized);
    LoginLogout.this.finish();
  }
  
  
  @Override
  protected void onResume()
  {
	  super.onResume();
      isResumed = true;
      if (VKSdk.isLoggedIn()) {
    	  SendTokenToUnity();
      } else if(canceledByUser) {
    	  
    	  String error_serialized = "-1" + "#" + "Canceled by user";
    	  UnityPlayer.UnitySendMessage("VkApi", "AccessDeniedMessage", error_serialized);
    	  LoginLogout.this.finish();
      }else
      {
    	  ShowLogin();
      }
    
  }
  
  @Override
  protected void onPause() {
      isResumed = false;
      super.onPause();
  }
  @Override
  protected void onDestroy()
  {
    super.onDestroy();
    
  } 
}
