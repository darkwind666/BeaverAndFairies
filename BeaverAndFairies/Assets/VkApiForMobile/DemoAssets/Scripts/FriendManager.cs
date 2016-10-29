using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.playGenesis.VkUnityPlugin;
using System;
using System.Collections.Generic;

public class VKFriedImage
{
    public Texture2D Img;
    public long VKUserId;
}
public class FriendManager : MonoBehaviour {

	public Text t;
	public Image i;
    public Sprite noPhoto;
    public static List<VKFriedImage> fImages=new List<VKFriedImage>(); 
    private VKUser _friend;

    public VKUser friend {
        get { return _friend; }
        set
        {
            if (value == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _friend = value;
                gameObject.SetActive(true);
                t.text = _friend.first_name + " " + _friend.last_name;
                GetImageFromCacheOrDownload(value.id);
            }
            
        }
    }

    private void GetImageFromCacheOrDownload(long id)
    {
        var image = fImages.Find(i => i.VKUserId == id);
        if (image != null && image.Img != null)
        {
            setUpImage(image.Img);
        }
        else
        {
            if (!string.IsNullOrEmpty(friend.photo_200))
            {
                DownloadFriendImage(friend.photo_200,friend.id);
            }
            else
            {
                i.sprite = noPhoto;
                fImages.Add(new VKFriedImage() {VKUserId = id, Img = null});
            }
        }
    }

    private void DownloadFriendImage(string  url,long id)
    {
        Action<DownloadRequest> doOnFinish = (d) =>
        {
            var fid = (long)d.CustomData[0];
            if (d.DownloadResult.error == null && friend.id == fid)
            {
               
                setUpImage(d.DownloadResult.texture);
                Destroy(d.DownloadResult.texture);

                fImages.Add(new VKFriedImage() {VKUserId = fid, Img = i.sprite.texture});
            }

        };
        var r = new DownloadRequest
        {
            url= url,
            onFinished = doOnFinish,
            CustomData = new object[] { id }
        };
        VkApi.Downloader.download(r);
    }

    public void setUpImage(byte[] photo)
	{
		var tex=new Texture2D(200,200);
		tex.LoadImage(photo);
        
		i.sprite=Sprite.Create(tex,new Rect(0,0,200,200),new Vector2(0.5f,0.5f));
        Destroy(tex);

    }
	public void setUpImage(Texture2D photo)
	{
        //var tex=new Texture2D(200,200);
        //tex.LoadImage(photo);
        if (i.sprite != noPhoto)
            DestroyObject(i.sprite);
		i.sprite=Sprite.Create(photo,new Rect(0,0,200,200),new Vector2(0.5f,0.5f));
		
	}
	public virtual void Invite(){
		if (friend != null) {
			VKRequest r2 = new VKRequest (){
				url="apps.sendRequest?user_id="+friend.id+"&text=hello_from_vk_plugin2&type=invite&name=sayhello",
				CallBackFunction=OnAppSendRequest
			};
			VkApi.VkApiInstance.Call(r2);
		}
	}
	public virtual void OnAppSendRequest(VKRequest r){
		if (r.error!=null) {
			GlobalErrorHandler.Instance.Notification.Notify(r);
			return;
		}
		Debug.Log (r.response);
	}


}
