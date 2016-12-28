using System;

namespace AppodealAds.Unity.Common
{
	// Interface for the methods to be invoked by the native plugin.
	internal interface IAdListener
	{
		void FireRewardUser(int amount);
		void FireAdLoaded();
		void FireAdFailedToLoad(string message);
		void FireAdOpened();
		void FireAdClosing();
		void FireAdClosed();
		void FireAdLeftApplication();
	}
}
