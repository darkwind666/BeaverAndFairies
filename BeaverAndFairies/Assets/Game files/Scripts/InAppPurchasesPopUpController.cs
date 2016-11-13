using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InAppPurchasesPopUpController : MonoBehaviour {

	public InAppPurchasesController inAppPurchasesController;
	public FairiesDataList fairiesDataSource;

	public Button blockAdInAppButton;
	public Text blockAdInAppButtonPrice;
	public Button restoreInAppsButton;
	public Text buyScoresCount1InAppPrice;
	public Text buyScoresCount1;
	public Text buyScoresCount2InAppPrice;
	public Text buyScoresCount2;
	public Text buyScoresCount3InAppPrice;
	public Text buyScoresCount3;
	public Text buyScoresCount4InAppPrice;
	public Text buyScoresCount4;
	public Text buyScoresCount5InAppPrice;
	public Text buyScoresCount5;

	GamePlayerDataController _playerData;


	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		setUpPrices();
		setUpCounts();

		if (_playerData.blockAdsInAppBought == true) {
			blockAdInAppButton.gameObject.SetActive(false);
		}

		#if UNITY_IOS

		restoreInAppsButton.SetActive(true);

		#endif

	}

	void Update () {
	
	}

	void setUpPrices()
	{
		blockAdInAppButtonPrice.text = fairiesDataSource.blockAdsInAppPrice.ToString() + "$";
		buyScoresCount1InAppPrice.text = fairiesDataSource.inAppsDataArray[0].price.ToString() + "$";
		buyScoresCount2InAppPrice.text = fairiesDataSource.inAppsDataArray[1].price.ToString() + "$";
		buyScoresCount3InAppPrice.text = fairiesDataSource.inAppsDataArray[2].price.ToString() + "$";
		buyScoresCount4InAppPrice.text = fairiesDataSource.inAppsDataArray[3].price.ToString() + "$";
		buyScoresCount5InAppPrice.text = fairiesDataSource.inAppsDataArray[4].price.ToString() + "$";
	}

	void setUpCounts()
	{
		buyScoresCount1.text = fairiesDataSource.inAppsDataArray[0].scoresCount.ToString();
		buyScoresCount2.text = fairiesDataSource.inAppsDataArray[1].scoresCount.ToString();
		buyScoresCount3.text = fairiesDataSource.inAppsDataArray[2].scoresCount.ToString();
		buyScoresCount4.text = fairiesDataSource.inAppsDataArray[3].scoresCount.ToString();
		buyScoresCount5.text = fairiesDataSource.inAppsDataArray[4].scoresCount.ToString();
	}

}
