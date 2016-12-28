using System;
using AppodealAds.Unity;

namespace AppodealAds.Unity.Common {
	public interface IAppodealAdsClient {

		void initialize(String appKey, int type);

		void orientationChange ();
		void disableNetwork (string network);
		void disableNetwork (string network, int type);
		void disableLocationPermissionCheck();
		void disableWriteExternalStoragePermissionCheck();

		void setInterstitialCallbacks (IInterstitialAdListener listener);
		void setSkippableVideoCallbacks (ISkippableVideoAdListener listener);
		void setNonSkippableVideoCallbacks (INonSkippableVideoAdListener listener);
		void setRewardedVideoCallbacks (IRewardedVideoAdListener listener);
		void setBannerCallbacks (IBannerAdListener listener);
		void requestAndroidMPermissions(IPermissionGrantedListener listener);
		void cache (int adTypes);
		void confirm(int adTypes);
		
		bool isLoaded (int adTypes);
		bool isPrecache (int adTypes);
		bool show(int adTypes);
		bool show(int adTypes, string placement);

		void hide (int adTypes);
		void setAutoCache (int adTypes, bool autoCache);
		void setOnLoadedTriggerBoth (int adTypes, bool onLoadedTriggerBoth);
		void setTesting(bool test);
		void setLogging(bool logging);
		void setSmartBanners(bool value);
		void setBannerAnimation(bool value);
		void setBannerBackground(bool value);

		void trackInAppPurchase(double amount, string currency);
		void setCustomRule(string name, bool value);
		void setCustomRule(string name, int value);
		void setCustomRule(string name, double value);
		void setCustomRule(string name, string value);
		
		string getVersion();

		void setUserId(string id);
		void setAge(int age);
		void setBirthday(string bDay);
		void setEmail(String email);
		void setGender(int gender);
		void setInterests(String interests);
		void setOccupation(int occupation);
		void setRelation(int relation);
		void setAlcohol(int alcohol);
		void setSmoking(int smoking);
		void getUserSettings();

	}
}
