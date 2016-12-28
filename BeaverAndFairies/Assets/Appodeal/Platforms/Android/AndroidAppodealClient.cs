using System;
using System.Collections.Generic;

using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

#if UNITY_ANDROID
namespace AppodealAds.Unity.Android
{
	public class AndroidAppodealClient : IAppodealAdsClient 
	{

		AndroidJavaClass appodealClass;
		AndroidJavaObject userSettings;
		AndroidJavaObject activity;

		public AndroidJavaClass getAppodealClass() {
			if (appodealClass == null) {
				appodealClass = new AndroidJavaClass("com.appodeal.ads.Appodeal");
			}
			return appodealClass;
		}

		public AndroidJavaObject getActivity() {
			if (activity == null) {
				AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			return activity;
		}

		public void initialize(string appKey, int adTypes) 
		{
			getAppodealClass().CallStatic("initialize", getActivity(), appKey, adTypes);
		}


		public void setInterstitialCallbacks(IInterstitialAdListener listener) 
		{
			getAppodealClass().CallStatic("setInterstitialCallbacks", new AppodealInterstitialCallbacks(listener));
		}
		
		public void setSkippableVideoCallbacks(ISkippableVideoAdListener listener)
		{
			getAppodealClass().CallStatic("setSkippableVideoCallbacks", new AppodealSkippableVideoCallbacks(listener));
		}

		public void setNonSkippableVideoCallbacks(INonSkippableVideoAdListener listener)
		{
			getAppodealClass().CallStatic("setNonSkippableVideoCallbacks", new AppodealNonSkippableVideoCallbacks(listener));
		}

		public void setRewardedVideoCallbacks(IRewardedVideoAdListener listener)
		{
			getAppodealClass().CallStatic("setRewardedVideoCallbacks", new AppodealRewardedVideoCallbacks(listener));
		}
		
		public void setBannerCallbacks(IBannerAdListener listener)
		{
			getAppodealClass().CallStatic("setBannerCallbacks", new AppodealBannerCallbacks(listener));
		}

		public void confirm(int adTypes)
		{
			getAppodealClass().CallStatic("confirm", adTypes);
		}
		
		public void cache(int adTypes)
		{
			getAppodealClass().CallStatic("cache", getActivity(), adTypes);
		}
		
		public Boolean isLoaded(int adTypes) 
		{
			return getAppodealClass().CallStatic<Boolean>("isLoaded", adTypes);
		}
		
		public Boolean isPrecache(int adTypes) 
		{
			return getAppodealClass().CallStatic<Boolean>("isPrecache", adTypes);
		}
		
		public Boolean show(int adTypes)
		{
			return getAppodealClass().CallStatic<Boolean>("show", getActivity(), adTypes);
		}

		public Boolean show(int adTypes, string placement)
		{
			return getAppodealClass().CallStatic<Boolean>("show", getActivity(), adTypes, placement);
		}
		
		public void hide(int adTypes)
		{
			getAppodealClass().CallStatic("hide", getActivity(), adTypes);
		}
		
		public void setAutoCache(int adTypes, Boolean autoCache) 
		{
			getAppodealClass().CallStatic("setAutoCache", adTypes, autoCache);	
		}
		
		public void setOnLoadedTriggerBoth(int adTypes, Boolean onLoadedTriggerBoth) 
		{
			getAppodealClass().CallStatic("setOnLoadedTriggerBoth", adTypes, onLoadedTriggerBoth);
		}

		public void disableNetwork(String network) 
		{
			getAppodealClass().CallStatic("disableNetwork", getActivity(), network);
		}

		public void disableNetwork(String network, int adTypes) 
		{
			getAppodealClass().CallStatic("disableNetwork", getActivity(), network, adTypes);
		}
		
		public void disableLocationPermissionCheck() 
		{
			getAppodealClass().CallStatic("disableLocationPermissionCheck");
		}

		public void disableWriteExternalStoragePermissionCheck() 
		{
			getAppodealClass().CallStatic("disableWriteExternalStoragePermissionCheck");
		}

		public void requestAndroidMPermissions(IPermissionGrantedListener listener) 
		{
			getAppodealClass().CallStatic("requestAndroidMPermissions", getActivity(), new AppodealPermissionCallbacks(listener));
		}
		
		public void orientationChange()
		{
			getAppodealClass().CallStatic("orientationChange");
		}

		public void setTesting(Boolean test)
		{
			getAppodealClass().CallStatic("setTesting", test);
		}

		public void setLogging(Boolean logging)
		{
			if(logging) {
				getAppodealClass().CallStatic("setLogLevel", new AndroidJavaClass("com.appodeal.ads.utils.Log$LogLevel").GetStatic<AndroidJavaObject>("verbose"));
			} else {
				getAppodealClass().CallStatic("setLogLevel", new AndroidJavaClass("com.appodeal.ads.utils.Log$LogLevel").GetStatic<AndroidJavaObject>("none"));
			}
		}
		
		public string getVersion()
		{
			return getAppodealClass().CallStatic<string>("getVersion");
		}

		public void trackInAppPurchase(double amount, string currency)
		{
			getAppodealClass().CallStatic("trackInAppPurchase", getActivity(), amount, currency);
		}

		public void setCustomRule(string name, Boolean value) {
			getAppodealClass().CallStatic("setCustomRule", name, value);
		}

		public void setCustomRule(string name, int value) {
			getAppodealClass().CallStatic("setCustomRule", name, value);
		}

		public void setCustomRule(string name, double value) {
			getAppodealClass().CallStatic("setCustomRule", name, value);
		}

		public void setCustomRule(string name, string value) {
			getAppodealClass().CallStatic("setCustomRule", name, value);
		}

		public void setSmartBanners(Boolean value) {
			getAppodealClass().CallStatic("setSmartBanners", value);
		}

		public void setBannerAnimation(bool value) {
			getAppodealClass().CallStatic("setBannerAnimation", value);
		}

		public void setBannerBackground(bool value) {
			//getAppodealClass().CallStatic("setBannerBackground", value);
		}

		//User Settings

		public void getUserSettings() 
		{
			userSettings = getAppodealClass().CallStatic<AndroidJavaObject>("getUserSettings", getActivity());
		}

		public void setUserId(string id) 
		{
			userSettings.Call<AndroidJavaObject>("setUserId", id);
		}

		public void setAge(int age) 
		{
			userSettings.Call<AndroidJavaObject>("setAge", age);
		}

		public void setBirthday(string bDay)
		{
			userSettings.Call<AndroidJavaObject> ("setBirthday", bDay);
		}

		public void setEmail(String email)
		{
			userSettings.Call<AndroidJavaObject> ("setEmail", email);
		}

		public void setGender(int gender)
		{
			switch(gender) 
			{
				case 1:
				{
					userSettings.Call<AndroidJavaObject> ("setGender", new AndroidJavaClass("com.appodeal.ads.UserSettings$Gender").GetStatic<AndroidJavaObject>("OTHER"));	
					break;
				} 
				case 2:
				{
					userSettings.Call<AndroidJavaObject> ("setGender", new AndroidJavaClass("com.appodeal.ads.UserSettings$Gender").GetStatic<AndroidJavaObject>("MALE"));
					break;
				} 
				case 3:
				{
					userSettings.Call<AndroidJavaObject> ("setGender", new AndroidJavaClass("com.appodeal.ads.UserSettings$Gender").GetStatic<AndroidJavaObject>("FEMALE"));
					break;
				}
			}
		}

		public void setInterests(String interests)
		{
			userSettings.Call<AndroidJavaObject> ("setInterests", interests);
		}

		public void setOccupation(int occupation)
		{
			switch(occupation) 
			{
				case 1:
				{
					userSettings.Call<AndroidJavaObject> ("setOccupation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Occupation").GetStatic<AndroidJavaObject>("OTHER"));
					break;
				} 
				case 2:
				{
					userSettings.Call<AndroidJavaObject> ("setOccupation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Occupation").GetStatic<AndroidJavaObject>("WORK"));
					break;
				} 
				case 3:
				{
					userSettings.Call<AndroidJavaObject> ("setOccupation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Occupation").GetStatic<AndroidJavaObject>("SCHOOL"));
					break;
				}
				case 4:
				{
					userSettings.Call<AndroidJavaObject> ("setOccupation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Occupation").GetStatic<AndroidJavaObject>("UNIVERSITY"));
					break;
				}
			}
		}

		public void setRelation(int relation)
		{
			switch(relation) 
			{
				case 1:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("OTHER"));
					break;
				} 
				case 2:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("SINGLE"));
					break;
				} 
				case 3:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("DATING"));
					break;
				} 
				case 4:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("ENGAGED"));
					break;
				} 
				case 5:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("MARRIED"));
					break;
				} 
				case 6:
				{
					userSettings.Call<AndroidJavaObject> ("setRelation", new AndroidJavaClass("com.appodeal.ads.UserSettings$Relation").GetStatic<AndroidJavaObject>("SEARCHING"));
					break;
				} 
			}
		}

		public void setAlcohol(int alcohol)
		{
			switch(alcohol) 
			{
				case 1:
				{
					userSettings.Call<AndroidJavaObject> ("setAlcohol", new AndroidJavaClass("com.appodeal.ads.UserSettings$Alcohol").GetStatic<AndroidJavaObject>("NEGATIVE"));
					break;
				} 
				case 2:
				{
					userSettings.Call<AndroidJavaObject> ("setAlcohol", new AndroidJavaClass("com.appodeal.ads.UserSettings$Alcohol").GetStatic<AndroidJavaObject>("NEUTRAL"));
					break;
				} 
				case 3:
				{
					userSettings.Call<AndroidJavaObject> ("setAlcohol", new AndroidJavaClass("com.appodeal.ads.UserSettings$Alcohol").GetStatic<AndroidJavaObject>("POSITIVE"));
					break;
				}
			}
		}

		public void setSmoking(int smoking)
		{
			switch(smoking) 
			{
				case 1:
				{
					userSettings.Call<AndroidJavaObject> ("setSmoking", new AndroidJavaClass("com.appodeal.ads.UserSettings$Smoking").GetStatic<AndroidJavaObject>("NEGATIVE"));
					break;
				} 
				case 2:
				{
					userSettings.Call<AndroidJavaObject> ("setSmoking", new AndroidJavaClass("com.appodeal.ads.UserSettings$Smoking").GetStatic<AndroidJavaObject>("NEUTRAL"));
					break;
				} 
				case 3:
				{
					userSettings.Call<AndroidJavaObject> ("setSmoking", new AndroidJavaClass("com.appodeal.ads.UserSettings$Smoking").GetStatic<AndroidJavaObject>("POSITIVE"));
					break;
				}
			}
		}


	}
}
#endif