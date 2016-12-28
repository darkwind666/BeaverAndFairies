using UnityEngine;
using System;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealBannerCallbacks
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		IBannerAdListener listener;	

		internal AppodealBannerCallbacks(IBannerAdListener listener) : base("com.appodeal.ads.BannerCallbacks") {
			this.listener = listener;
		}

		void onBannerLoaded(int height, Boolean isPrecache) {
			listener.onBannerLoaded();
		}
			
		void onBannerFailedToLoad() {
			listener.onBannerFailedToLoad();
		}
			
		void onBannerShown() {
			listener.onBannerShown();
		}
			
		void onBannerClicked() {
			listener.onBannerClicked();
		}
	}
#else
	{
		public AppodealBannerCallbacks(IBannerAdListener listener) { }
	}
#endif
}

