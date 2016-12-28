using System;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

// Example script showing how to invoke the Appodeal Ads Unity plugin.
public class AppodealDemo : MonoBehaviour, IInterstitialAdListener, IBannerAdListener, INonSkippableVideoAdListener, ISkippableVideoAdListener, IRewardedVideoAdListener, IPermissionGrantedListener
{

	#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
		string appKey = "";
	#elif UNITY_ANDROID
		string appKey = "fee50c333ff3825fd6ad6d38cff78154de3025546d47a84f";
	#elif UNITY_IPHONE
		string appKey = "722fb56678445f72fe2ec58b2fa436688b920835405d3ca6";
	#else
		string appKey = "";
	#endif

	public Toggle LoggingToggle, TestingToggle, ConfirmToggle;

	void Awake ()
	{
		Appodeal.requestAndroidMPermissions(this);
	}

	public void Init() {
		//Example for UserSettings usage
		UserSettings settings = new UserSettings ();
		settings.setUserId("1234567890").setAge(25).setBirthday ("01/01/1990").setAlcohol(UserSettings.Alcohol.NEUTRAL)
			    .setSmoking(UserSettings.Smoking.NEUTRAL).setEmail("hi@appodeal.com")
				.setGender(UserSettings.Gender.OTHER).setRelation(UserSettings.Relation.DATING)
				.setInterests("gym, cars, cinema, science").setOccupation(UserSettings.Occupation.WORK);
		
		if (LoggingToggle.isOn) Appodeal.setLogging(true);
		if (TestingToggle.isOn) Appodeal.setTesting(true);
		if (ConfirmToggle.isOn) Appodeal.confirm(Appodeal.SKIPPABLE_VIDEO);

		Appodeal.setSmartBanners(false);
		Appodeal.setBannerAnimation(false);
		Appodeal.setBannerBackground(false);
		Appodeal.initialize (appKey, Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.SKIPPABLE_VIDEO | Appodeal.REWARDED_VIDEO);

		Appodeal.setBannerCallbacks (this);
		Appodeal.setInterstitialCallbacks (this);
		Appodeal.setSkippableVideoCallbacks (this);
		Appodeal.setRewardedVideoCallbacks(this);

		Appodeal.setCustomRule("newBoolean", true);
		Appodeal.setCustomRule("newInt", 1234567890);
		Appodeal.setCustomRule("newDouble", 123.123456789);
		Appodeal.setCustomRule("newString", "newStringFromSDK");
	}

	public void showInterstitial() {
		Appodeal.show (Appodeal.INTERSTITIAL, "interstitial_button_click");
	}

	public void showSkippableVideo() {
		Appodeal.show (Appodeal.SKIPPABLE_VIDEO, "skippable_video_button_click");
	}

	public void showRewardedVideo() {
		Appodeal.show (Appodeal.REWARDED_VIDEO);
	}

	public void showBanner() {
		Appodeal.show (Appodeal.BANNER_BOTTOM, "banner_button_click");
	}

	public void showInterstitialOrVideo() {
		Appodeal.show (Appodeal.INTERSTITIAL | Appodeal.SKIPPABLE_VIDEO);
	}

	public void hideBanner() {
		Appodeal.hide (Appodeal.BANNER);
	}

	#region Banner callback handlers

	public void onBannerLoaded() { Debug.Log("Banner loaded"); }
	public void onBannerFailedToLoad() { Debug.Log("Banner failed"); }
	public void onBannerShown() { Debug.Log("Banner opened"); }
	public void onBannerClicked() { Debug.Log("banner clicked"); }

	#endregion

	#region Interstitial callback handlers
	
	public void onInterstitialLoaded() { Debug.Log("Interstitial loaded"); }
	public void onInterstitialFailedToLoad() { Debug.Log("Interstitial failed"); }
	public void onInterstitialShown() { Debug.Log("Interstitial opened"); }
	public void onInterstitialClicked() { Debug.Log("Interstitial clicked"); }
	public void onInterstitialClosed() { Debug.Log("Interstitial closed"); }
	
	#endregion

	#region Video callback handlers

	public void onSkippableVideoLoaded() { Debug.Log("Skippable Video loaded"); }
	public void onSkippableVideoFailedToLoad() { Debug.Log("Skippable Video failed"); }
	public void onSkippableVideoShown() { Debug.Log("Skippable Video opened"); }
	public void onSkippableVideoClosed() { Debug.Log("Skippable Video closed"); }
	public void onSkippableVideoFinished() { Debug.Log("Skippable Video finished"); }

	#endregion

	#region Non Skippable Video callback handlers
	
	public void onNonSkippableVideoLoaded() { Debug.Log("NonSkippable Video loaded"); }
	public void onNonSkippableVideoFailedToLoad() { Debug.Log("NonSkippable Video failed"); }
	public void onNonSkippableVideoShown() { Debug.Log("NonSkippable Video opened"); }
	public void onNonSkippableVideoClosed() { Debug.Log("NonSkippable Video closed"); }
	public void onNonSkippableVideoFinished() { Debug.Log("NonSkippable Video finished"); }
	
	#endregion

	#region Rewarded Video callback handlers
	
	public void onRewardedVideoLoaded() { Debug.Log("Rewarded Video loaded"); }
	public void onRewardedVideoFailedToLoad() { Debug.Log("Rewarded Video failed"); }
	public void onRewardedVideoShown() { Debug.Log("Rewarded Video opened"); }
	public void onRewardedVideoClosed() { Debug.Log("Rewarded Video closed"); }
	public void onRewardedVideoFinished(int amount, string name) { Debug.Log("Rewarded Video Reward: " + amount + " " + name); }
	
	#endregion

	#region Permission Grant callback handlers
	
	public void writeExternalStorageResponse(int result) { 
		if(result == 0) {
			Debug.Log("WRITE_EXTERNAL_STORAGE permission granted"); 
		} else {
			Debug.Log("WRITE_EXTERNAL_STORAGE permission grant refused"); 
		}
	}
	public void accessCoarseLocationResponse(int result) { 
		if(result == 0) {
			Debug.Log("ACCESS_COARSE_LOCATION permission granted"); 
		} else {
			Debug.Log("ACCESS_COARSE_LOCATION permission grant refused"); 
		}
	}
	
	#endregion
}