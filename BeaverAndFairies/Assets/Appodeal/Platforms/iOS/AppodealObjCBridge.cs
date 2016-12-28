using System;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
namespace AppodealAds.Unity.iOS
{
	
	internal delegate void AppodealInterstitialCallbacks ();
	internal delegate void AppodealSkippableVideoCallbacks ();
	internal delegate void AppodealNonSkippableVideoCallbacks ();
	internal delegate void AppodealBannerCallbacks ();
	internal delegate void AppodealRewardedVideoCallbacks ();
	internal delegate void AppodealRewardedVideoDidFinishCallback (int amount, String name);
	
	internal class AppodealObjCBridge
	{
		[DllImport("__Internal")]
		internal static extern void AppodealInitializeWithTypes (string apiKey, int types);
		
		[DllImport("__Internal")]
		internal static extern bool AppodealShowAd (int style);

		[DllImport("__Internal")]
		internal static extern bool AppodealShowAdforPlacement (int style, string placement);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetAutocache (bool autocache, int types);
		
		[DllImport("__Internal")]
		internal static extern void AppodealCacheAd (int types);
		
		[DllImport("__Internal")]
		internal static extern bool AppodealIsReadyWithStyle (int style);
		
		[DllImport("__Internal")]
		internal static extern void AppodealHideBanner ();

		[DllImport("__Internal")]
		internal static extern void AppodealConfirmUsage (int types);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetInterstitialDelegate (
			AppodealInterstitialCallbacks interstitialDidLoadAd,
			AppodealInterstitialCallbacks interstitialDidFailToLoadAd,
			AppodealInterstitialCallbacks interstitialDidClick,
			AppodealInterstitialCallbacks interstitialDidDismiss,
			AppodealInterstitialCallbacks interstitialWillPresent
		);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetSkippableVideoDelegate (
			AppodealSkippableVideoCallbacks skippableVideoDidLoadAd,
			AppodealSkippableVideoCallbacks skippableVideoDidFailToLoadAd,
			AppodealSkippableVideoCallbacks skippableVideoWillDismiss,
			AppodealSkippableVideoCallbacks skippableVideoDidFinish,
			AppodealSkippableVideoCallbacks skippableVideoDidPresent
		);

		[DllImport("__Internal")]
		internal static extern void AppodealSetNonSkippableVideoDelegate (
			AppodealNonSkippableVideoCallbacks nonSkippableVideoDidLoadAd,
			AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFailToLoadAd,
			AppodealNonSkippableVideoCallbacks nonSkippableVideoWillDismiss,
			AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFinish,
			AppodealNonSkippableVideoCallbacks nonSkippableVideoDidPresent
		);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetRewardedVideoDelegate(
			AppodealRewardedVideoCallbacks rewardedVideoDidLoadAd,
			AppodealRewardedVideoCallbacks rewardedVideoDidFailToLoadAd,
			AppodealRewardedVideoCallbacks rewardedVideoWillDismiss,
			AppodealRewardedVideoDidFinishCallback rewardedVideoDidFinish,
			AppodealRewardedVideoCallbacks rewardedVideoDidPresent
		);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetBannerDelegate (
			AppodealBannerCallbacks bannerDidLoadAd,
			AppodealBannerCallbacks bannerDidFailToLoadAd,
			AppodealBannerCallbacks bannerDidClick,
			AppodealBannerCallbacks bannerDidShow
		);
		
		[DllImport("__Internal")]
		internal static extern string AppodealGetVersion ();
		
		[DllImport("__Internal")]
		internal static extern void AppodealDisableLocationPermissionCheck();
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetDebugEnabled(bool debugEnabled);

		[DllImport("__Internal")]
		internal static extern void AppodealSetTestingEnabled(bool testingEnabled);

		[DllImport("__Internal")]
		internal static extern void AppodealDisableNetwork(string name);

		[DllImport("__Internal")]
		internal static extern void AppodealDisableNetworkForAdTypes(string name, int type);	

		[DllImport("__Internal")]
		internal static extern void setCustomSegmentString(string name, string value);

		[DllImport("__Internal")]
		internal static extern void setCustomSegmentDouble(string name, double value);

		[DllImport("__Internal")]
		internal static extern void setCustomSegmentInt(string name, int value);

		[DllImport("__Internal")]
		internal static extern void setCustomSegmentBool(string name, bool value);

		[DllImport("__Internal")]
		internal static extern void setSmartBanners(bool value);

		[DllImport("__Internal")]
		internal static extern void setBannerBackground(bool value);

		[DllImport("__Internal")]
		internal static extern void setBannerAnimation(bool value);

		[DllImport("__Internal")]
		internal static extern void AppodealSetUserId(string id);

		[DllImport("__Internal")]
		internal static extern void AppodealSetUserAge(int age);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserBirthday(string birthday);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserEmail(string email);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserGender(int gender);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserInterests(string interests);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserOccupation(int occupation);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserRelationship(int relationship);
		
		[DllImport("__Internal")]
		internal static extern void	AppodealSetUserAlcoholAttitude(int attitude);
		
		[DllImport("__Internal")]
		internal static extern void AppodealSetUserSmokingAttitude(int attitude);
	}
}
#endif