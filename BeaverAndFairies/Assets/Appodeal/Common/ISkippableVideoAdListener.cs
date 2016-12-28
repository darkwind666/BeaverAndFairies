using System;

namespace AppodealAds.Unity.Common
{
	public interface ISkippableVideoAdListener
	{
		void onSkippableVideoLoaded();
		void onSkippableVideoFailedToLoad();
		void onSkippableVideoShown();
		void onSkippableVideoFinished();
		void onSkippableVideoClosed();
	}
}
