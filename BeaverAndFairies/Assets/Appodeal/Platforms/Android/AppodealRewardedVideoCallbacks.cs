using UnityEngine;
using System.Collections;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealRewardedVideoCallbacks 
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		IRewardedVideoAdListener listener;
		
		internal AppodealRewardedVideoCallbacks(IRewardedVideoAdListener listener) : base("com.appodeal.ads.RewardedVideoCallbacks") {
			this.listener = listener;
		}
		
		void onRewardedVideoLoaded() {
			listener.onRewardedVideoLoaded();
		}
		
		void onRewardedVideoFailedToLoad() {
			listener.onRewardedVideoFailedToLoad();
		}
		
		void onRewardedVideoShown() {
			listener.onRewardedVideoShown();
		}

		void onRewardedVideoFinished(int amount, AndroidJavaObject name) {
			listener.onRewardedVideoFinished(amount, null);
		}
		
		void onRewardedVideoFinished(int amount, string name) {
			listener.onRewardedVideoFinished(amount, name);
		}
		
		void onRewardedVideoClosed(bool finished) {
			listener.onRewardedVideoClosed();
		}
	}
#else
	{
		public AppodealRewardedVideoCallbacks(IRewardedVideoAdListener listener) { }
	}
#endif
}