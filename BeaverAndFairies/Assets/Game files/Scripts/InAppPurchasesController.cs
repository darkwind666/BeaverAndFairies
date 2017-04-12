using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;
using System.Collections;


public class InAppPurchasesController : MonoBehaviour, IStoreListener {

	public GameGlobalSettings gameSettings;
	public FairiesDataList fairiesDataSource;
	public GameObject blockAdButton;

	static IStoreController m_StoreController;
	static IExtensionProvider m_StoreExtensionProvider;

	GamePlayerDataController _playerData;
	GameAnaliticsController _analiticsController;

	void Start () {

		_analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

		#if UNITY_ANDROID

		gameSettings.inAppProductRemoveAdID = gameSettings.inAppProductRemoveAdID.ToLower();
		gameSettings.inAppProductScoreCount1ID = gameSettings.inAppProductScoreCount1ID.ToLower();
		gameSettings.inAppProductScoreCount2ID = gameSettings.inAppProductScoreCount2ID.ToLower();
		gameSettings.inAppProductScoreCount3ID = gameSettings.inAppProductScoreCount3ID.ToLower();
		gameSettings.inAppProductScoreCount4ID = gameSettings.inAppProductScoreCount4ID.ToLower();
		gameSettings.inAppProductScoreCount5ID = gameSettings.inAppProductScoreCount5ID.ToLower();

		#endif

		if (m_StoreController == null)
		{
			initializePurchasing();
		}
	}

	void Update () {
	
	}

	public void initializePurchasing() 
	{
		if (isInitialized())
		{
			return;
		}
			
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(gameSettings.inAppProductRemoveAdID, ProductType.NonConsumable);
		builder.AddProduct(gameSettings.inAppProductScoreCount1ID, ProductType.Consumable);
		builder.AddProduct(gameSettings.inAppProductScoreCount2ID, ProductType.Consumable);
		builder.AddProduct(gameSettings.inAppProductScoreCount3ID, ProductType.Consumable);
		builder.AddProduct(gameSettings.inAppProductScoreCount4ID, ProductType.Consumable);
		builder.AddProduct(gameSettings.inAppProductScoreCount5ID, ProductType.Consumable);

		UnityPurchasing.Initialize(this, builder);
	}

	private bool isInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void buyScoreCount1()
	{
		buyProductID(gameSettings.inAppProductScoreCount1ID);
	}

	public void buyScoreCount2()
	{
		buyProductID(gameSettings.inAppProductScoreCount2ID);
	}

	public void buyScoreCount3()
	{
		buyProductID(gameSettings.inAppProductScoreCount3ID);
	}

	public void buyScoreCount4()
	{
		buyProductID(gameSettings.inAppProductScoreCount4ID);
	}

	public void buyScoreCount5()
	{
		buyProductID(gameSettings.inAppProductScoreCount5ID);
	}

	public void buyRemoveAds()
	{
		if(_playerData.blockAdsInAppBought == false)
		{
			buyProductID(gameSettings.inAppProductRemoveAdID);
		}
	}

	void buyProductID(string productId)
	{
		if (isInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);

			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{ 
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}

		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!isInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
			
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			apple.RestoreTransactions((result) => {
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("OnInitialized: PASS");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}
		
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}
		
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductRemoveAdID, StringComparison.Ordinal))
		{
			buyBonusForIndex(0);
			_analiticsController.userBuyBlockAd();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductScoreCount1ID, StringComparison.Ordinal))
		{
			buyBonusForIndex(0);
			_analiticsController.userBuyScoresCount1();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductScoreCount2ID, StringComparison.Ordinal))
		{
			buyBonusForIndex(1);
			_analiticsController.userBuyScoresCount2();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductScoreCount3ID, StringComparison.Ordinal))
		{
			buyBonusForIndex(2);
			_analiticsController.userBuyScoresCount3();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductScoreCount4ID, StringComparison.Ordinal))
		{
			buyBonusForIndex(3);
			_analiticsController.userBuyScoresCount4();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, gameSettings.inAppProductScoreCount5ID, StringComparison.Ordinal))
		{
			buyBonusForIndex(4);
			_analiticsController.userBuyScoresCount5();
		}
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
			
		return PurchaseProcessingResult.Complete;
	}

	void buyBonusForIndex(int aIndex)
	{
		blockAdButton.SetActive(false);
		_playerData.blockAdsInAppBought = true;
		_playerData.playerScore += fairiesDataSource.inAppsDataArray[aIndex].scoresCount;
		_playerData.savePlayerData();
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
