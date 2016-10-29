using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;
using System.Collections.Generic;
using System.Text;

public class CaptchaDialog : QueueWorker<VKRequest>
{
	public Image captchaImage;
	public InputField captchaText;

    Downloader dnl;
    VKCaptcha captchaData;
    private string lastaddedCaptchaParams;

	void Start()
	{

		dnl = FindObjectOfType<Downloader> ();
		captchaImage =transform.GetChild(0).GetComponent<Image> ();
		captchaText=GetComponentInChildren<InputField> ();
		captchaText.DeactivateInputField();
        
	}

	void ParseCaptchaIdAndUrl (string response)
	{
		var dict=Json.Deserialize(response) as Dictionary<string,object>;
		var resp= (Dictionary<string,object>)dict["error"];
		var captcha_sid=(string)resp["captcha_sid"];
		var captcha_img=(string)resp["captcha_img"];
		captchaData.url = captcha_img;
		captchaData.id = captcha_sid;
	}

    protected override void StartProcessing()
    {
        ParseCaptchaIdAndUrl(_current.Element.response);
        if (dnl == null)
            dnl = FindObjectOfType<Downloader>();
        var req = new DownloadRequest
        {
            url = captchaData.url,
            onFinished = OnGotCaptchaImage
        };
        dnl.download(req);
    }

   

	void OnGotCaptchaImage (DownloadRequest d)
	{
		if (d.DownloadResult.error != null) {
			return;
		}
		var spriteRect = new Rect (0, 0, 130, 50);
		var texture = d.DownloadResult.texture;
		captchaImage.sprite=Sprite.Create (texture, spriteRect, new Vector2 (0.5f, 0.5f));
		captchaText.ActivateInputField ();
		captchaText.text = "";
		gameObject.SetActive (true);
	}

	public void OnCaptchaEntered(string s)
	{

		captchaData.key = captchaText.text;
		if (_current.Element.url.Contains ("captcha_sid")) {
            _current.Element.url = _current.Element.url.Replace (lastaddedCaptchaParams, "&captcha_sid=" + captchaData.id + "&captcha_key=" + captchaData.key);
			lastaddedCaptchaParams="&captcha_sid="+captchaData.id+"&captcha_key="+captchaData.key;
		} else {
			lastaddedCaptchaParams="&captcha_sid="+captchaData.id+"&captcha_key="+captchaData.key;
            _current.Element.url+="&captcha_sid="+captchaData.id+"&captcha_key="+captchaData.key;

		}
        _current.Element.fullurl = null;
		VkApi.VkApiInstance.Call(_current.Element);
		Debug.Log (_current.Element.url);
		gameObject.SetActive (false);
		ProccessNext();
	}
}
