using System;

namespace AppodealAds.Unity.Common
{
	public interface IBannerAdListener
	{
		void onBannerLoaded();
		void onBannerFailedToLoad();
		void onBannerShown();
		void onBannerClicked();
	}
}