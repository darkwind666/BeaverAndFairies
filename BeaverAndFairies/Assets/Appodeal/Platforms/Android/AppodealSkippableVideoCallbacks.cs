using UnityEngine;
using System.Collections;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealSkippableVideoCallbacks 
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		ISkippableVideoAdListener listener;

		internal AppodealSkippableVideoCallbacks(ISkippableVideoAdListener listener) : base("com.appodeal.ads.SkippableVideoCallbacks") {
			this.listener = listener;
		}
		
		void onSkippableVideoLoaded() {
			listener.onSkippableVideoLoaded();
		}
		
		void onSkippableVideoFailedToLoad() {
			listener.onSkippableVideoFailedToLoad();
		}
		
		void onSkippableVideoShown() {
			listener.onSkippableVideoShown();
		}
		
		void onSkippableVideoFinished() {
			listener.onSkippableVideoFinished();
		}
		
		void onSkippableVideoClosed(bool finished) {
			listener.onSkippableVideoClosed();
		}
	}
#else
	{
		public AppodealSkippableVideoCallbacks(ISkippableVideoAdListener listener) { }
	}
#endif
}
