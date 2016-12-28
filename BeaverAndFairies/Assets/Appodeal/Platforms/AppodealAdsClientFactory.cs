using System;
using UnityEngine;
using AppodealAds.Unity.Common;

namespace AppodealAds.Unity
{
	internal class AppodealAdsClientFactory
	{
		internal static IAppodealAdsClient GetAppodealAdsClient()
		{
			#if UNITY_EDITOR
			return null;
			#elif UNITY_ANDROID
			return new AppodealAds.Unity.Android.AndroidAppodealClient();
			#elif UNITY_IPHONE
			return AppodealAds.Unity.iOS.AppodealAdsClient.Instance;
			#else
			return null;
			#endif
		}
	}
}