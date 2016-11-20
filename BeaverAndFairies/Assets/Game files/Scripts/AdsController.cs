using UnityEngine;
using System.Collections;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdsController : MonoBehaviour, INonSkippableVideoAdListener {

	public GameGlobalSettings settings;
	public FinalChanceController chanceController;
	public EndGameController levelResultsController;

	bool _finalChanceAd;
	bool _simplifyGameAd;
	bool _additionalScoreAd;
	bool _adToBlockAd;

	BlockAdsController _currentBlockAdsController;

	void Start () {

		if (settings.showAppodealAds) 
		{
			string appodealId;

			if (settings.paidGame) {

				appodealId = settings.androidHdAppodealId;

				#if UNITY_IOS
				appodealId = settings.iosHdAppodealId;
				#endif 

			} else {
				appodealId = settings.androidFreeAppodealId;

				#if UNITY_IOS
				appodealId = settings.iosFreeAppodealId;
				#endif 
			}

			Appodeal.initialize(appodealId, Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.INTERSTITIAL | Appodeal.BANNER_TOP);
			Appodeal.setNonSkippableVideoCallbacks(this);
		}

	}

	void Update () {
	
	}

	public void onNonSkippableVideoFinished() {}

	public void onNonSkippableVideoLoaded() { }
	public void onNonSkippableVideoFailedToLoad() { }
	public void onNonSkippableVideoShown() { }
	public void onNonSkippableVideoClosed() { }

	public bool adAvailable() {
		bool adAvailable = false;

		if (settings.showAppodealAds) 
		{
			adAvailable = Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO);
		}

		return adAvailable;
	}

	void getRewardForAd()
	{
		if(_finalChanceAd)
		{
			chanceController.getReward();
			_finalChanceAd = false;
		}

		if(_additionalScoreAd)
		{
			levelResultsController.getAdditionalScores();
			_additionalScoreAd = false;
		}

		if(_adToBlockAd)
		{
			_currentBlockAdsController.addFinishShow();
			_adToBlockAd = false;
		}
	}

	public void showFinalChanceAd() {

		_finalChanceAd = true;
		playGameAd();
	}

	public void showSimplifyGameAd() {

		_simplifyGameAd = true;
		playGameAd();
	}

	public void showAdditionalScoreAd() {

		_additionalScoreAd = true;
		playGameAd();
	}

	public void showAdToBlockAdFromController(BlockAdsController aBlockAdController)
	{
		_currentBlockAdsController = aBlockAdController;
		_adToBlockAd = true;
		playGameAd();
	}

	public void playGameAd()
	{
		showAds();
	}

	void showAds()
	{
		if (settings.showAppodealAds) 
		{
			Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
		}
	}

	public void tryShowInterstitial()
	{
		int randomNumber = UnityEngine.Random.Range(0, 3);
		if(randomNumber == 0)
		{
			if (settings.showAppodealAds && settings.paidGame == false) 
			{
				if (Appodeal.isLoaded (Appodeal.INTERSTITIAL)) 
				{
					Appodeal.show(Appodeal.INTERSTITIAL);
				}
			}
		}
	}

	public void showInterstitial()
	{
		if (settings.showAppodealAds && settings.paidGame == false && settings.blockAds == false) 
		{
			if (Appodeal.isLoaded (Appodeal.INTERSTITIAL)) 
			{
				Appodeal.show(Appodeal.INTERSTITIAL);
			}
		}
	}

	public void showBottomBanner()
	{
		if (settings.showAppodealAds && settings.paidGame == false && settings.blockAds == false)
		{
			if (Appodeal.isLoaded (Appodeal.BANNER_TOP))
			{
				Appodeal.show(Appodeal.BANNER_TOP);
			}
		}
	}

	public void hideBottomBanner()
	{
		if (settings.showAppodealAds) 
		{
			Appodeal.hide(Appodeal.BANNER_TOP);
		}
	}
}
