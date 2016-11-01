using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tacticsoft;
using com.playGenesis.VkUnityPlugin;


public class InviteFriendsController : MonoBehaviour, ITableViewDataSource {

	public VKFriendCell m_cellPrefab;
	public TableView m_tableView;

	public List<BeaverTimeVKFriend> friendsDataSource;
	public GameObject vkontakteControllerContainer;

	VKontakteInviteFriendsInterface _vkontakteGameController;

	void Start () {
		_vkontakteGameController = vkontakteControllerContainer.GetComponent<VKontakteInviteFriendsInterface>();
		m_tableView.dataSource = this;
		if(friendsDataSource == null)
		{
			friendsDataSource = new List<BeaverTimeVKFriend>();
		}
	}

	void Update () {
	
	}

	void OnEnable()
	{
		if(friendsDataSource != null && m_tableView.dataSource != null)
		{
			m_tableView.ReloadData();
		}
	}

	#region ITableViewDataSource

	public int GetNumberOfRowsForTableView(TableView tableView) {
		return friendsDataSource.Count;
	}

	//Will be called by the TableView to know what is the height of each row
	public float GetHeightForRowInTableView(TableView tableView, int row) {
		return (m_cellPrefab.transform as RectTransform).rect.height;
	}

	//Will be called by the TableView when a cell needs to be created for display
	public TableViewCell GetCellForRowInTableView(TableView tableView, int row) {
		VKFriendCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as VKFriendCell;
		if (cell == null) {
			cell = (VKFriendCell)GameObject.Instantiate(m_cellPrefab);
		}
		VKUser cellData = friendsDataSource[row].friend;
		cell.friendName.text = cellData.first_name + " " + cellData.last_name;

		if (friendsDataSource [row].invited == true) {
			cell.inviteButton.gameObject.SetActive (false);
		} else {
			cell.inviteButton.gameObject.SetActive (true);
		}

		Action<DownloadRequest> doOnFinish =(downloadRequest)=>
		{
			Texture2D tex=downloadRequest.DownloadResult.texture;

			if (cell.friendImage.sprite != null)
			{
				DestroyObject(cell.friendImage.sprite);
			}

			cell.friendImage.sprite = Sprite.Create(tex,new Rect(0,0,50,50),new Vector2(0.5f,0.5f));
		};

		_vkontakteGameController.loadImageWithUrlAndCallback (cellData.photo_50, doOnFinish);

		cell.inviteButton.onClick.RemoveAllListeners();
		cell.inviteButton.onClick.AddListener(() => { 
			_vkontakteGameController.inviteFriend(cellData.id.ToString(), cellData.first_name, () => {
				cell.inviteButton.gameObject.SetActive(false);
				friendsDataSource[row].invited = true;
			});
		});

		return cell;
	}

	#endregion

}

public class BeaverTimeVKFriend {

	public bool invited;
	public VKUser friend;
}

public interface VKontakteInviteFriendsInterface {

	void loadImageWithUrlAndCallback (string aUrl, Action<DownloadRequest> aCallback);
	void inviteFriend(string friendId, string friendName, Action aCallback);

}
