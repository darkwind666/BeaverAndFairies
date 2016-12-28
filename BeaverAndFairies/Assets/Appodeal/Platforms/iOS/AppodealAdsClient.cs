using UnityEngine;
using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using AOT;

#if UNITY_IPHONE
namespace AppodealAds.Unity.iOS
{
	public class AppodealAdsClient : IAppodealAdsClient {
		
		private const int AppodealAdTypeInterstitial 	  = 1 << 0;
		private const int AppodealAdTypeSkippableVideo 	  = 1 << 1;
		private const int AppodealAdTypeBanner       	  = 1 << 2;
		private const int AppodealAdTypeRewardedVideo	  = 1 << 4;
		private const int AppodealAdTypeNonSkippableVideo = 1 << 6;
		private const int AppodealAdTypeAll           = AppodealAdTypeInterstitial | AppodealAdTypeSkippableVideo | AppodealAdTypeBanner | AppodealAdTypeNonSkippableVideo | AppodealAdTypeRewardedVideo;
		
		private const int AppodealShowStyleInterstitial        = 1;
		private const int AppodealShowStyleVideo               = 2;
		private const int AppodealShowStyleVideoOrInterstitial = 3;
		private const int AppodealShowStyleBannerTop           = 4;
		private const int AppodealShowStyleBannerBottom        = 5;
		private const int AppodealShowStyleRewardedVideo       = 6;
		private const int AppodealShowStyleNonSkippableVideo   = 7;
		
		
		#region Singleton
		
		private AppodealAdsClient(){}
		
		private static readonly AppodealAdsClient instance = new AppodealAdsClient();
		
		public static AppodealAdsClient Instance {
			get {
				return instance; 
			}
		}
		
		#endregion

		public void requestAndroidMPermissions(IPermissionGrantedListener listener) {
			// not supported on ios
		}
		
		private static IInterstitialAdListener interstitialListener;
		private static ISkippableVideoAdListener skippableVideoListener;
		private static INonSkippableVideoAdListener nonSkippableVideoListener;
		private static IRewardedVideoAdListener rewardedVideoListener;
		private static IBannerAdListener bannerListener;
		
		#region Interstitial Delegate
		
		[MonoPInvokeCallback (typeof (AppodealInterstitialCallbacks))]
		private static void interstitialDidLoad () {
			if (AppodealAdsClient.interstitialListener != null) {
				AppodealAdsClient.interstitialListener.onInterstitialLoaded();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealInterstitialCallbacks))]
		private static void interstitialDidFailToLoad () {
			if (AppodealAdsClient.interstitialListener != null) {
				AppodealAdsClient.interstitialListener.onInterstitialFailedToLoad();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealInterstitialCallbacks))]
		private static void interstitialDidClick () {
			if (AppodealAdsClient.interstitialListener != null) {
				AppodealAdsClient.interstitialListener.onInterstitialClicked();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealInterstitialCallbacks))]
		private static void interstitialDidDismiss () {
			if (AppodealAdsClient.interstitialListener != null) {
				AppodealAdsClient.interstitialListener.onInterstitialClosed();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealInterstitialCallbacks))]
		private static void interstitialWillPresent () {
			if (AppodealAdsClient.interstitialListener != null) {
				AppodealAdsClient.interstitialListener.onInterstitialShown();
			}
		}

		public void setInterstitialCallbacks(IInterstitialAdListener listener) {
			AppodealAdsClient.interstitialListener = listener;
			
			AppodealObjCBridge.AppodealSetInterstitialDelegate(
				AppodealAdsClient.interstitialDidLoad,
				AppodealAdsClient.interstitialDidFailToLoad,
				AppodealAdsClient.interstitialDidClick,
				AppodealAdsClient.interstitialDidDismiss,
				AppodealAdsClient.interstitialWillPresent
			);
		}
		
		#endregion
		
		#region Skippable Video Delegate
		
		[MonoPInvokeCallback (typeof (AppodealSkippableVideoCallbacks))]
		private static void skippableVideoDidLoadAd() {
			if (AppodealAdsClient.skippableVideoListener != null) {
				AppodealAdsClient.skippableVideoListener.onSkippableVideoLoaded();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealSkippableVideoCallbacks))]
		private static void skippableVideoDidFailToLoadAd() {
			if (AppodealAdsClient.skippableVideoListener != null) {
				AppodealAdsClient.skippableVideoListener.onSkippableVideoFailedToLoad();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealSkippableVideoCallbacks))]
		private static void skippableVideoWillDismiss() {
			if (AppodealAdsClient.skippableVideoListener != null) {
				AppodealAdsClient.skippableVideoListener.onSkippableVideoClosed();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealSkippableVideoCallbacks))]
		private static void skippableVideoDidFinish() {
			if (AppodealAdsClient.skippableVideoListener != null) {
				AppodealAdsClient.skippableVideoListener.onSkippableVideoFinished();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealSkippableVideoCallbacks))]
		private static void skippableVideoDidPresent() {
			if (AppodealAdsClient.skippableVideoListener != null) {
				AppodealAdsClient.skippableVideoListener.onSkippableVideoShown();
			}
		}
		
		public void setSkippableVideoCallbacks(ISkippableVideoAdListener listener) {
			AppodealAdsClient.skippableVideoListener = listener;
			
			AppodealObjCBridge.AppodealSetSkippableVideoDelegate(
				AppodealAdsClient.skippableVideoDidLoadAd,
				AppodealAdsClient.skippableVideoDidFailToLoadAd,
				AppodealAdsClient.skippableVideoWillDismiss,
				AppodealAdsClient.skippableVideoDidFinish,
				AppodealAdsClient.skippableVideoDidPresent
			);
		}
		
		#endregion

		#region Non Skippable Video Delegate

		[MonoPInvokeCallback (typeof (AppodealNonSkippableVideoCallbacks))]
		private static void nonSkippableVideoDidLoadAd() {
			if (AppodealAdsClient.nonSkippableVideoListener != null) {
				AppodealAdsClient.nonSkippableVideoListener.onNonSkippableVideoLoaded();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealNonSkippableVideoCallbacks))]
		private static void nonSkippableVideoDidFailToLoadAd() {
			if (AppodealAdsClient.nonSkippableVideoListener != null) {
				AppodealAdsClient.nonSkippableVideoListener.onNonSkippableVideoFailedToLoad();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealNonSkippableVideoCallbacks))]
		private static void nonSkippableVideoWillDismiss() {
			if (AppodealAdsClient.nonSkippableVideoListener != null) {
				AppodealAdsClient.nonSkippableVideoListener.onNonSkippableVideoClosed();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealNonSkippableVideoCallbacks))]
		private static void nonSkippableVideoDidFinish() {
			if (AppodealAdsClient.nonSkippableVideoListener != null) {
				AppodealAdsClient.nonSkippableVideoListener.onNonSkippableVideoFinished();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealNonSkippableVideoCallbacks))]
		private static void nonSkippableVideoDidPresent() {
			if (AppodealAdsClient.nonSkippableVideoListener != null) {
				AppodealAdsClient.nonSkippableVideoListener.onNonSkippableVideoShown();
			}
		}

		public void setNonSkippableVideoCallbacks(INonSkippableVideoAdListener listener) {
			AppodealAdsClient.nonSkippableVideoListener = listener;

			AppodealObjCBridge.AppodealSetNonSkippableVideoDelegate(
				AppodealAdsClient.nonSkippableVideoDidLoadAd,
				AppodealAdsClient.nonSkippableVideoDidFailToLoadAd,
				AppodealAdsClient.nonSkippableVideoWillDismiss,
				AppodealAdsClient.nonSkippableVideoDidFinish,
				AppodealAdsClient.nonSkippableVideoDidPresent
			);
		}

		#endregion
		
		#region Rewarded Video Delegate
		
		
		[MonoPInvokeCallback (typeof (AppodealRewardedVideoCallbacks))]
		private static void rewardedVideoDidLoadAd() {
			if (AppodealAdsClient.rewardedVideoListener != null) {
				AppodealAdsClient.rewardedVideoListener.onRewardedVideoLoaded();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealRewardedVideoCallbacks))]
		private static void rewardedVideoDidFailToLoadAd() {
			if (AppodealAdsClient.rewardedVideoListener != null) {
				AppodealAdsClient.rewardedVideoListener.onRewardedVideoFailedToLoad();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealRewardedVideoCallbacks))]
		private static void rewardedVideoWillDismiss() {
			if (AppodealAdsClient.rewardedVideoListener != null) {
				AppodealAdsClient.rewardedVideoListener.onRewardedVideoClosed();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealRewardedVideoDidFinishCallback))]
		private static void rewardedVideoDidFinish(int amount, string name) {
			if (AppodealAdsClient.rewardedVideoListener != null) {
				AppodealAdsClient.rewardedVideoListener.onRewardedVideoFinished(amount, name);
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealRewardedVideoCallbacks))]
		private static void rewardedVideoDidPresent() {
			if (AppodealAdsClient.rewardedVideoListener != null) {
				AppodealAdsClient.rewardedVideoListener.onRewardedVideoShown();
			}
		}
		
		public void setRewardedVideoCallbacks(IRewardedVideoAdListener listener) {
			AppodealAdsClient.rewardedVideoListener = listener;
			
			AppodealObjCBridge.AppodealSetRewardedVideoDelegate(
				AppodealAdsClient.rewardedVideoDidLoadAd,
				AppodealAdsClient.rewardedVideoDidFailToLoadAd,
				AppodealAdsClient.rewardedVideoWillDismiss,
				AppodealAdsClient.rewardedVideoDidFinish,
				AppodealAdsClient.rewardedVideoDidPresent
			);
		}
		
		#endregion

		#region Banner Delegate
		
		[MonoPInvokeCallback (typeof (AppodealBannerCallbacks))]
		private static void bannerDidLoadAd() {
			if (AppodealAdsClient.bannerListener != null) {
				AppodealAdsClient.bannerListener.onBannerLoaded();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealBannerCallbacks))]
		private static void bannerDidFailToLoadAd() {
			if (AppodealAdsClient.bannerListener != null) {
				AppodealAdsClient.bannerListener.onBannerFailedToLoad();
			}
		}
		
		[MonoPInvokeCallback (typeof (AppodealBannerCallbacks))]
		private static void bannerDidClick () {
			if (AppodealAdsClient.bannerListener != null) {
				AppodealAdsClient.bannerListener.onBannerClicked();
			}
		}

		[MonoPInvokeCallback (typeof (AppodealBannerCallbacks))]
		private static void bannerDidShow () {
			if (AppodealAdsClient.bannerListener != null) {
				AppodealAdsClient.bannerListener.onBannerShown();
			}
		}
		
		public void setBannerCallbacks(IBannerAdListener listener) {
			AppodealAdsClient.bannerListener = listener;
			
			AppodealObjCBridge.AppodealSetBannerDelegate(
				AppodealAdsClient.bannerDidLoadAd,
				AppodealAdsClient.bannerDidFailToLoadAd,
				AppodealAdsClient.bannerDidClick,
				AppodealAdsClient.bannerDidShow);
		}
		
		#endregion
		
		private int nativeAdTypesForType(int adTypes) {
			int nativeAdTypes = 0;
			
			if ((adTypes & Appodeal.INTERSTITIAL) > 0) {
				nativeAdTypes |= AppodealAdTypeInterstitial;
			}
			
			if ((adTypes & Appodeal.SKIPPABLE_VIDEO) > 0) {
				nativeAdTypes |= AppodealAdTypeSkippableVideo;
			}
			
			if ((adTypes & Appodeal.BANNER) > 0 || 
			    (adTypes & Appodeal.BANNER_TOP) > 0 || 
			    (adTypes & Appodeal.BANNER_BOTTOM) > 0) {
				
				nativeAdTypes |= AppodealAdTypeBanner;
			}
			
			if ((adTypes & Appodeal.REWARDED_VIDEO) > 0) {
				nativeAdTypes |= AppodealAdTypeRewardedVideo;
			} 

			if ((adTypes & Appodeal.NON_SKIPPABLE_VIDEO) > 0) {
				nativeAdTypes |= AppodealAdTypeNonSkippableVideo;
			}
			
			return nativeAdTypes;
		}
		
		private int nativeShowStyleForType(int adTypes) {
			bool isInterstitial = (adTypes & Appodeal.INTERSTITIAL) > 0;
			bool isVideo = (adTypes & Appodeal.SKIPPABLE_VIDEO) > 0;
			
			if (isInterstitial && isVideo) {
				return AppodealShowStyleVideoOrInterstitial;
			} else if (isVideo) {
				return AppodealShowStyleVideo;
			} else if (isInterstitial) {
				return AppodealShowStyleInterstitial;
			}
			
			if ((adTypes & Appodeal.BANNER_TOP) > 0) {
				return AppodealShowStyleBannerTop;
			}
			
			if ((adTypes & Appodeal.BANNER_BOTTOM) > 0) {
				return AppodealShowStyleBannerBottom;
			}
			
			if ((adTypes & Appodeal.REWARDED_VIDEO) > 0) {
				return AppodealShowStyleRewardedVideo;
			} 

			if ((adTypes & Appodeal.NON_SKIPPABLE_VIDEO) > 0) {
				return AppodealShowStyleNonSkippableVideo;
			}
			
			return 0;
		}
		
		public void initialize(string appKey, int adTypes) {
			AppodealObjCBridge.AppodealInitializeWithTypes(appKey, nativeAdTypesForType(adTypes));
		}
		
		public void cache(int adTypes) {
			AppodealObjCBridge.AppodealCacheAd(nativeAdTypesForType(adTypes));
		}
		
		public Boolean isLoaded(int adTypes) {
			int style = nativeShowStyleForType(adTypes);
			bool isBanner = style == AppodealShowStyleBannerTop || style == AppodealShowStyleBannerBottom;
			
			return isBanner ? true : AppodealObjCBridge.AppodealIsReadyWithStyle(style);
		}
		
		public Boolean isPrecache(int adTypes) {
			return false;
		}
		
		public Boolean show(int adTypes) {
			return AppodealObjCBridge.AppodealShowAd(nativeShowStyleForType(adTypes));
		}

		public Boolean show(int adTypes, string placement)
		{
			return AppodealObjCBridge.AppodealShowAdforPlacement(nativeShowStyleForType(adTypes), placement);
		}
		
		public void hide(int adTypes) {
			if ((nativeAdTypesForType(adTypes) & AppodealAdTypeBanner) > 0) {
				AppodealObjCBridge.AppodealHideBanner();
			}
		}
		
		public void setAutoCache(int adTypes, Boolean autoCache) {
			AppodealObjCBridge.AppodealSetAutocache(autoCache, nativeAdTypesForType(adTypes));
		}
		
		public void setTesting(Boolean test) {
			AppodealObjCBridge.AppodealSetTestingEnabled(test);
		}
		
		public void setLogging(Boolean logging) {
			AppodealObjCBridge.AppodealSetDebugEnabled(logging);
		}
		
		public void setOnLoadedTriggerBoth(int adTypes, Boolean onLoadedTriggerBoth) {
			// Not supported for iOS SDK
		}

		public void confirm(int adTypes) {
			AppodealObjCBridge.AppodealConfirmUsage(adTypes);
		}

		public void disableWriteExternalStoragePermissionCheck() 
		{
			// Not supported for iOS SDK
		}
		
		public void disableNetwork(String network) {
			AppodealObjCBridge.AppodealDisableNetwork(network);
		}
		
		public void disableNetwork(String network, int adTypes) {
			AppodealObjCBridge.AppodealDisableNetworkForAdTypes(network, adTypes);
		}
		
		public void disableLocationPermissionCheck() 
		{
			AppodealObjCBridge.AppodealDisableLocationPermissionCheck();
		}
		
		public void orientationChange() { } // handled by SDK
		
		
		public string getVersion() {
			return AppodealObjCBridge.AppodealGetVersion();
		}
		
		//User Settings
		
		public void getUserSettings() {
			// No additional state change required on iOS
		}

		public void setUserId(string id) {
			AppodealObjCBridge.AppodealSetUserId(id);
		}

		public void setAge(int age) 
		{
			AppodealObjCBridge.AppodealSetUserAge(age);
		}
		
		public void setBirthday(string bDay)
		{
			AppodealObjCBridge.AppodealSetUserBirthday(bDay);
		}
		
		public void setEmail(String email)
		{
			AppodealObjCBridge.AppodealSetUserEmail(email);
		}
		
		public void setGender(int gender)
		{
			AppodealObjCBridge.AppodealSetUserGender(gender - 1); // iOS Enum starts from 0
		}
		
		public void setInterests(String interests)
		{
			AppodealObjCBridge.AppodealSetUserInterests(interests);
		}
		
		public void setOccupation(int occupation)
		{
			AppodealObjCBridge.AppodealSetUserOccupation(occupation - 1);
		}
		
		public void setRelation(int relation)
		{
			AppodealObjCBridge.AppodealSetUserRelationship(relation - 1);
		}
		
		public void setAlcohol(int alcohol)
		{
			AppodealObjCBridge.AppodealSetUserAlcoholAttitude(alcohol);
		}
		
		public void setSmoking(int smoking)
		{
			AppodealObjCBridge.AppodealSetUserSmokingAttitude(smoking);
		}

		public void trackInAppPurchase(double amount, string currency)
		{
			//TODO;
		}

		public void setCustomRule(string name, bool value) 
		{
			AppodealObjCBridge.setCustomSegmentBool(name, value);
		}
		
		public void setCustomRule(string name, int value) 
		{
			AppodealObjCBridge.setCustomSegmentInt(name, value);
		}
		
		public void setCustomRule(string name, double value) 
		{
			AppodealObjCBridge.setCustomSegmentDouble(name, value);
		}
		
		public void setCustomRule(string name, string value)
		{
			AppodealObjCBridge.setCustomSegmentString(name, value);
		}

		public void setSmartBanners(Boolean value)
		{
			AppodealObjCBridge.setSmartBanners(value);
		}

		public void setBannerAnimation(bool value) {
			AppodealObjCBridge.setBannerAnimation(value);
		}

		public void setBannerBackground(bool value) {
			AppodealObjCBridge.setBannerBackground(value);
		}
				
	}
}
#endif