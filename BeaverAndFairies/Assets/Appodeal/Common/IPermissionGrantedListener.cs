using System;

namespace AppodealAds.Unity.Common
{
	public interface IPermissionGrantedListener
	{
		void writeExternalStorageResponse(int result);
		void accessCoarseLocationResponse(int result);
	}
}
