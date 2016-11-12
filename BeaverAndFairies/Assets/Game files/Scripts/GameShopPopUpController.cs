using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Tacticsoft;

public class GameShopPopUpController : MonoBehaviour, ITableViewDataSource {

	public GameShopCell m_cellPrefab;
	public TableView m_tableView;
	public FairiesDataList fairiesDataSource;

	public Text slowBonusPrice;
	public Text damageBonusPrice;

	GamePlayerDataController _playerData;
	int _lastSelectedFairyIndex;

	void Start () {
		_playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
		m_tableView.dataSource = this;
		_lastSelectedFairyIndex = _playerData.selectedFairyIndex;
		slowBonusPrice.text = fairiesDataSource.slowBonusPrice.ToString();
		damageBonusPrice.text = fairiesDataSource.damageBonusPrice.ToString();
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

		GameFairyModel cellData = fairiesDataSource.dataArray[row];
		cell.fairyButton.image.sprite =  Sprite.Create(cellData.fairyTexture,new Rect(0,0,50,50),new Vector2(0.5f,0.5f));

		setUpPrice(cell, row);
		setUpBuyButton(cell, row);
		setUpToggle(cell, row);

		return cell;
	}

	void setUpPrice(GameShopCell cell, int row)
	{
		GameFairyModel cellData = fairiesDataSource.dataArray[row];

		if (_playerData.playerFairies.Contains (row) == true) {
			cell.price.gameObject.SetActive(false);
		} else {
			cell.price.text = cellData.fairyPrice.ToString();
		}
	}

	void setUpBuyButton(GameShopCell cell, int row)
	{
		GameFairyModel cellData = fairiesDataSource.dataArray[row];
		cell.fairyButton.onClick.RemoveAllListeners();
		cell.fairyButton.onClick.AddListener(() => { 

			if(_playerData.playerFairies.Contains(row) == false && _playerData.playerScore <= cellData.fairyPrice)
			{
				_playerData.playerFairies.Add(row);
				_playerData.playerScore -= cellData.fairyPrice;
				_playerData.selectedFairyIndex = row;
				_lastSelectedFairyIndex = row;
				_playerData.savePlayerData();
			}

		});
	}

	void setUpToggle(GameShopCell cell, int row)
	{
		cell.selectFairyToggle.onValueChanged.RemoveAllListeners();
		cell.selectFairyToggle.onValueChanged.AddListener((bool value) => {

			GameShopCell selectedFairyCell = m_tableView.GetCellAtRow(_lastSelectedFairyIndex) as GameShopCell;
			selectedFairyCell.selectFairyToggle.Select();

			_playerData.selectedFairyIndex = row;
			_lastSelectedFairyIndex = row;

		});
	}

	public void buySlowBonus()
	{
		if(_playerData.playerScore <= fairiesDataSource.slowBonusPrice)
		{
			_playerData.slowBonusCount++;
			_playerData.playerScore -= fairiesDataSource.slowBonusPrice;
			_playerData.savePlayerData();
		}
	}

	public void buyDamageBonus()
	{
		if(_playerData.playerScore <= fairiesDataSource.damageBonusPrice)
		{
			_playerData.damageBonusCount++;
			_playerData.playerScore -= fairiesDataSource.damageBonusPrice;
			_playerData.savePlayerData();
		}
	}

}
