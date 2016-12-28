using System;

namespace AppodealAds.Unity.Common
{
	public interface INonSkippableVideoAdListener
	{
		void onNonSkippableVideoLoaded();
		void onNonSkippableVideoFailedToLoad();
		void onNonSkippableVideoShown();
		void onNonSkippableVideoFinished();
		void onNonSkippableVideoClosed();
	}
}
