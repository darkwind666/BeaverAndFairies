using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Tacticsoft;

public class GameShopPopUpController : MonoBehaviour, ITableViewDataSource {

	public GameShopCell m_cellPrefab;
	public TableView m_tableView;
	public FairiesDataList fairiesDataSource;
	public GameObject inAppPurchasesPopUp;

	public Text slowBonusPrice;
	public Text damageBonusPrice;
	public Text bonusesForAdsCountLabel;

	GamePlayerDataController _playerData;
	int _lastSelectedFairyIndex;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		m_tableView.dataSource = this;
		_lastSelectedFairyIndex = _playerData.selectedFairyIndex;
		slowBonusPrice.text = fairiesDataSource.slowBonusPrice.ToString();
		damageBonusPrice.text = fairiesDataSource.damageBonusPrice.ToString();
		bonusesForAdsCountLabel.text = fairiesDataSource.scoresForAdsCount.ToString();
	}

	void Update () {
	
	}

	void OnEnable()
	{
		if(fairiesDataSource != null && m_tableView.dataSource != null)
		{
			m_tableView.ReloadData();
		}
	}

	public int GetNumberOfRowsForTableView(TableView tableView) {
		return fairiesDataSource.dataArray.Length;
	}
		
	public float GetHeightForRowInTableView(TableView tableView, int row) {
		return (m_cellPrefab.transform as RectTransform).rect.height;
	}
		
	public TableViewCell GetCellForRowInTableView(TableView tableView, int row) {
		GameShopCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as GameShopCell;
		if (cell == null) {
			cell = (GameShopCell)GameObject.Instantiate(m_cellPrefab);
		}

		Debug.Log (row);

		setUpPrice(cell, row);
		setUpDescription(cell, row);
		setUpBuyButton(cell, row);
		setUpToggle(cell, row);

		return cell;
	}

	void setUpDescription(GameShopCell cell, int row)
	{
		GameFairyModel cellData = fairiesDataSource.dataArray[row];
		cell.descriptionDamageTime.text = cellData.fairyCreateSlowBonusTime.ToString();
		cell.descriptionSlowdownTime.text = cellData.fairyCreateDamageBonusTime.ToString();
	}

	void setUpPrice(GameShopCell cell, int row)
	{
		GameFairyModel cellData = fairiesDataSource.dataArray[row];

		if (_playerData.playerFairies.Contains (row) == true) {
			cell.price.gameObject.SetActive(false);
		} else {
			cell.price.gameObject.SetActive(true);
			cell.price.text = cellData.fairyPrice.ToString();
		}
	}

	void setUpBuyButton(GameShopCell cell, int row)
	{
		GameFairyModel cellData = fairiesDataSource.dataArray[row];
		cell.fairyButton.image.sprite =  Sprite.Create(cellData.fairyTexture,new Rect(0,0,cellData.fairyTexture.width,cellData.fairyTexture.height),new Vector2(0.5f,0.5f));
		cell.fairyButton.onClick.RemoveAllListeners();
		cell.fairyButton.onClick.AddListener(() => { 

			if(_playerData.playerFairies.Contains(row) == false)
			{
				if(_playerData.playerScore >= cellData.fairyPrice)
				{
					_playerData.playerFairies.Add(row);
					_playerData.playerScore -= cellData.fairyPrice;
					uncheckPreviousFairy();
					selectFairyOnIndexWithCell(row, cell);
					_playerData.savePlayerData();
					cell.price.gameObject.SetActive(false);

					GameAnaliticsController analiticsController = GameObject.FindObjectOfType<GameAnaliticsController>();
					analiticsController.buyFairyPressed();
				} else {
					inAppPurchasesPopUp.SetActive(true);
				}
			} else {
				uncheckPreviousFairy();
				selectFairyOnIndexWithCell(row, cell);
			}

		});
	}

	void selectFairyOnIndexWithCell(int aIndex, GameShopCell aCell)
	{
		_playerData.selectedFairyIndex = aIndex;
		_lastSelectedFairyIndex = aIndex;
		aCell.selectFairyToggle.gameObject.SetActive(true);
	}

	void uncheckPreviousFairy()
	{
		if(_lastSelectedFairyIndex >= 0)
		{
			GameShopCell selectedFairyCell = m_tableView.GetCellAtRow(_lastSelectedFairyIndex) as GameShopCell;
			selectedFairyCell.selectFairyToggle.gameObject.SetActive(false);
		}
	}

	void setUpToggle(GameShopCell cell, int row)
	{
		if(_playerData.playerFairies.Contains(row) == false)
		{
			cell.selectFairyToggle.gameObject.SetActive(false);
			return;
		}

		if(_lastSelectedFairyIndex == row)
		{
			cell.selectFairyToggle.gameObject.SetActive(true);
		} else {
			cell.selectFairyToggle.gameObject.SetActive(false);
		}
	}

	public void buySlowBonus()
	{
		if (_playerData.playerScore >= fairiesDataSource.slowBonusPrice) {
			_playerData.slowBonusCount++;
			_playerData.playerScore -= fairiesDataSource.slowBonusPrice;
			_playerData.savePlayerData ();
		} else {
			inAppPurchasesPopUp.SetActive(true);
		}
	}

	public void buyDamageBonus()
	{
		if(_playerData.playerScore >= fairiesDataSource.damageBonusPrice)
		{
			_playerData.damageBonusCount++;
			_playerData.playerScore -= fairiesDataSource.damageBonusPrice;
			_playerData.savePlayerData();
		} else {
			inAppPurchasesPopUp.SetActive(true);
		}
	}

	public void getAdditionalScores()
	{
		_playerData.damageBonusCount += fairiesDataSource.scoresForAdsCount;
		_playerData.savePlayerData();
	}

}
