using UnityEngine;
using System;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity.Android 
{
	public class AppodealPermissionCallbacks
#if UNITY_ANDROID
		: AndroidJavaProxy
	{
		IPermissionGrantedListener listener;	

		internal AppodealPermissionCallbacks(IPermissionGrantedListener listener) : base("com.appodeal.ads.utils.PermissionsHelper$AppodealPermissionCallbacks") {
			this.listener = listener;
		}
		
		void writeExternalStorageResponse(int result) {
			listener.writeExternalStorageResponse(result);
		}
		
		void accessCoarseLocationResponse(int result) {
			listener.accessCoarseLocationResponse(result);
		}
	}
#else
	{
		public AppodealPermissionCallbacks(IPermissionGrantedListener listener) { }
	}
#endif
}
