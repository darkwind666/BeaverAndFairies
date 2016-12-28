using UnityEngine;
using System;
using System.Collections;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Api {
	public class Appodeal {

		public const int NONE                = 0;
		public const int INTERSTITIAL        = 1;
		public const int SKIPPABLE_VIDEO     = 2;
		public const int BANNER              = 4;
		public const int BANNER_BOTTOM       = 8;
		public const int BANNER_TOP          = 16;
		public const int REWARDED_VIDEO      = 128;
		#if UNITY_ANDROID || UNITY_EDITOR
		public const int NON_SKIPPABLE_VIDEO = 128;
		#elif UNITY_IPHONE
		public const int NON_SKIPPABLE_VIDEO = 256;
		#endif

		private static IAppodealAdsClient client;
		private static IAppodealAdsClient getInstance() {
			if (client == null) {
				client = AppodealAdsClientFactory.GetAppodealAdsClient();
			}
			return client;
		}

		public static void initialize(string appKey, int adTypes)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().initialize(appKey, adTypes);
			#endif
		}

		public static void setInterstitialCallbacks(IInterstitialAdListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setInterstitialCallbacks (listener);
			#endif
		}
		
		public static void setSkippableVideoCallbacks(ISkippableVideoAdListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setSkippableVideoCallbacks (listener);
			#endif
		}

		public static void setNonSkippableVideoCallbacks(INonSkippableVideoAdListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setNonSkippableVideoCallbacks (listener);
			#endif
		}

		public static void setRewardedVideoCallbacks(IRewardedVideoAdListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setRewardedVideoCallbacks (listener);
			#endif
		}
		
		public static void setBannerCallbacks(IBannerAdListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setBannerCallbacks (listener);
			#endif
		}
		
		public static void cache(int adTypes)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().cache (adTypes);
			#endif
		}

		public static void confirm(int adTypes)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().confirm (adTypes);
			#endif
		}
		
		public static bool isLoaded(int adTypes) 
		{
			bool isLoaded = false;
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			isLoaded = getInstance().isLoaded (adTypes);
			#endif
			return isLoaded;
		}
		
		public static bool isPrecache(int adTypes) 
		{
			bool isPrecache = false;
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			isPrecache = getInstance().isPrecache (adTypes);
			#endif
			return isPrecache;
		}
		
		public static bool show(int adTypes)
		{
			bool show = false;
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			show = getInstance().show (adTypes);
			#endif
			return show;
		}

		public static bool show(int adTypes, string placement)
		{
			bool show = false;
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR && !UNITY_EDITOR
			show = getInstance().show (adTypes, placement);
			#endif
			return show;
		}
		
		public static void hide(int adTypes)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().hide (adTypes);
			#endif
		}
		
		public static void orientationChange()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().orientationChange ();
			#endif
		}
		
		public static void setAutoCache(int adTypes, bool autoCache) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setAutoCache (adTypes, autoCache);
			#endif
		}
		
		public static void setOnLoadedTriggerBoth(int adTypes, bool onLoadedTriggerBoth) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setOnLoadedTriggerBoth (adTypes, onLoadedTriggerBoth);
			#endif
		}
		
		public static void disableNetwork(string network) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().disableNetwork (network);
			#endif
		}

		public static void disableNetwork(string network, int adType) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().disableNetwork (network, adType);
			#endif
		}
		
		public static void disableLocationPermissionCheck() 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().disableLocationPermissionCheck ();
			#endif
		}	

		public static void disableWriteExternalStoragePermissionCheck() 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().disableWriteExternalStoragePermissionCheck ();
			#endif
		}

		public static void requestAndroidMPermissions(IPermissionGrantedListener listener)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			getInstance().requestAndroidMPermissions (listener);
			#endif
		}
		
		public static void setTesting(bool test) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setTesting (test);
			#endif
		}

		public static void setLogging(bool logging) 
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setLogging (logging);
			#endif
		}
		
        public static string getVersion()
        {
            string version = null;
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
            version = getInstance().getVersion();
            #endif
            return version;
        }

		public static void trackInAppPurchase(double amount, string currency)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().trackInAppPurchase(amount, currency);
			#endif
		}

		public static void setCustomRule(string name, bool value)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setCustomRule(name, value);
			#endif
		}

		public static void setCustomRule(string name, int value)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setCustomRule(name, value);
			#endif
		}

		public static void setCustomRule(string name, double value)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setCustomRule(name, value);
			#endif
		}

		public static void setCustomRule(string name, string value)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setCustomRule(name, value);
			#endif
		}

		public static void setSmartBanners(Boolean value)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setSmartBanners(value);
			#endif
		}

		public static void setBannerBackground(bool value) {
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setBannerBackground(value);
			#endif
		}

		public static void setBannerAnimation(bool value) {
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setBannerAnimation(value);
			#endif
		}

	}

	public class UserSettings
	{

		private static IAppodealAdsClient client;
		private static IAppodealAdsClient getInstance() {
			if (client == null) {
				client = AppodealAdsClientFactory.GetAppodealAdsClient();
			}
			return client;
		}

		public enum Gender {
			OTHER, MALE, FEMALE
		}
		
		public enum Occupation {
			OTHER, WORK, SCHOOL, UNIVERSITY
		}
		
		public enum Relation {
			OTHER, SINGLE, DATING, ENGAGED, MARRIED, SEARCHING
		}
		
		public enum Smoking {
			NEGATIVE, NEUTRAL, POSITIVE
		}
		
		public enum Alcohol {
			NEGATIVE, NEUTRAL, POSITIVE
		}
				
		public UserSettings ()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().getUserSettings();
			#endif
		}

		public UserSettings setUserId(string id)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setUserId(id);
			#endif
			return this;
		}
		
		public UserSettings setAge(int age)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setAge(age);
			#endif
			return this;
		}
		
		public UserSettings setBirthday(string bDay)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setBirthday(bDay);
			#endif
			return this;
		}
		
		public UserSettings setEmail(string email)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setEmail(email);
			#endif
			return this;
		}

		public UserSettings setGender(Gender gender)
		{
			switch(gender) {
				case Gender.OTHER:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setGender(1);
					#endif
					return this;
				} 
				case Gender.MALE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setGender(2);
					#endif
					return this;
				} 
				case Gender.FEMALE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setGender(3);
					#endif
					return this;
				}
			}
			return null;
		}
		
		public UserSettings setInterests(string interests)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
			getInstance().setInterests(interests);
			#endif
			return this;
		}
		
		public UserSettings setOccupation(Occupation occupation)
		{
			switch(occupation) {
				case Occupation.OTHER:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setOccupation(1);
					#endif
					return this;
				} 
				case Occupation.WORK:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setOccupation(2);
					#endif
					return this;
				} 
				case Occupation.SCHOOL:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setOccupation(3);
					#endif
					return this;
				}
				case Occupation.UNIVERSITY:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setOccupation(4);
					#endif
					return this;;
				}
			}
			return null;
		}
		
		public UserSettings setRelation(Relation relation)
		{
			switch(relation) {
				case Relation.OTHER:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(1);
					#endif
					return this;
				} 
				case Relation.SINGLE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(2);
					#endif	
					return this;
				} 
				case Relation.DATING:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(3);
					#endif
					return this;
				} 
				case Relation.ENGAGED:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(4);
					#endif
					return this;
				} 
				case Relation.MARRIED:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(5);
					#endif	
					return this;
				} 
				case Relation.SEARCHING:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setRelation(6);
					#endif
					return this;
				} 
			}
			return null;
		}
		
		public UserSettings setAlcohol(Alcohol alcohol)
		{
			switch(alcohol) {
				case Alcohol.NEGATIVE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setAlcohol(1);
					#endif
					return this;
				} 
				case Alcohol.NEUTRAL:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setAlcohol(2);
					#endif
					return this;
				} 
				case Alcohol.POSITIVE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setAlcohol(3);
					#endif
					return this;
				}
			}
			return null;
		}
		
		public UserSettings setSmoking(Smoking smoking)
		{
			switch(smoking) {
				case Smoking.NEGATIVE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setSmoking(1);
					#endif
					return this;
				} 
				case Smoking.NEUTRAL:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setSmoking(2);
					#endif
					return this;
				} 
				case Smoking.POSITIVE:
				{
					#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IPHONE && !UNITY_EDITOR
					getInstance().setSmoking(3);
					#endif
					return this;
				}
			}
			return null;
		}
		
	}
}
