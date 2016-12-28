using UnityEngine;
using System.Collections;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealNonSkippableVideoCallbacks 
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		INonSkippableVideoAdListener listener;
		
		internal AppodealNonSkippableVideoCallbacks(INonSkippableVideoAdListener listener) : base("com.appodeal.ads.NonSkippableVideoCallbacks") {
			this.listener = listener;
		}
		
		void onNonSkippableVideoLoaded() {
			listener.onNonSkippableVideoLoaded();
		}
		
		void onNonSkippableVideoFailedToLoad() {
			listener.onNonSkippableVideoFailedToLoad();
		}
		
		void onNonSkippableVideoShown() {
			listener.onNonSkippableVideoShown();
		}
		
		void onNonSkippableVideoFinished() {
			listener.onNonSkippableVideoFinished();
		}
		
		void onNonSkippableVideoClosed(bool finished) {
			listener.onNonSkippableVideoClosed();
		}
	}
#else
	{
		public AppodealNonSkippableVideoCallbacks(INonSkippableVideoAdListener listener) { }
	}
#endif
}


