using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockAdsController : MonoBehaviour {

	public GameGlobalSettings settings;
	public int stopAdsCount;
	public AdsController adsController;
	public GameObject adsAndHDVersionPad;
	public FairiesDataList gameBalanceData;

	public GameObject button;
	public Text countText;

	void Start () {

		if(settings.paidGame == true)
		{
			adsAndHDVersionPad.SetActive(false);
		}

		if(settings.blockAds)
		{
			button.SetActive(false);
		}

		stopAdsCount = gameBalanceData.maxStopAdsCount;
		countText.text = stopAdsCount.ToString();
	}

	void Update () {
	
	}

	public void showBlockAd()
	{
		adsController.showAdToBlockAdFromController(this);
	}

	public void addFinishShow()
	{
		stopAdsCount--;
		countText.text = stopAdsCount.ToString();

		if(stopAdsCount <= 0)
		{
			settings.blockAds = true;
			button.SetActive(false);
		}
	}
}
