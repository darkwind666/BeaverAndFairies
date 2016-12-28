using System;

namespace AppodealAds.Unity.Common
{
	public interface IInterstitialAdListener
	{
		void onInterstitialLoaded();
		void onInterstitialFailedToLoad();
		void onInterstitialShown();
		void onInterstitialClosed();
		void onInterstitialClicked();
	}
}
