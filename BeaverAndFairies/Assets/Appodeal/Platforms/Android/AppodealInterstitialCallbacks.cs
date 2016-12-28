using UnityEngine;
using System;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealInterstitialCallbacks
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		IInterstitialAdListener listener;	

		internal AppodealInterstitialCallbacks(IInterstitialAdListener listener) : base("com.appodeal.ads.InterstitialCallbacks") {
			this.listener = listener;
		}
		
		void onInterstitialLoaded(Boolean isPrecache) {
			listener.onInterstitialLoaded();
		}
		
		void onInterstitialFailedToLoad() {
			listener.onInterstitialFailedToLoad();
		}
		
		void onInterstitialShown() {
			listener.onInterstitialShown();
		}
		
		void onInterstitialClicked() {
			listener.onInterstitialClicked();
		}
		
		void onInterstitialClosed() {
			listener.onInterstitialClosed();
		}
	}
#else
	{
		public AppodealInterstitialCallbacks(IInterstitialAdListener listener) { }
	}
#endif
}
