using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace com.playGenesis.VkUnityPlugin
{
    public class WebView : QueueWorker<WebViewRequest>
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject jo;
		protected void  OpenWebView(string openurl,string closeurl)
		{
			jo=new AndroidJavaObject("com.playgenesis.vkunityplugin.Initializer"); 
			jo.Call ("OpenWebView",openurl,closeurl);
		}
#endif
#if UNITY_WSA && !UNITY_EDITOR 
		public delegate void OpenWebViewDelegate(string openurl,string closeurl);
		public OpenWebViewDelegate OpenWebViewAction{get;set;}
		private void  OpenWebView(string openurl,string closeurl)
		{
			if (OpenWebViewAction!=null){
				OpenWebViewAction(openurl,closeurl);
			}
		}
#endif
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void _OpenWebView(string openUrl,string closeurl);

		protected void  OpenWebView(string openurl,string closeurl)
		{

			_OpenWebView( openurl, closeurl);
		}

#endif
#if UNITY_EDITOR
        private void OpenWebView(string openurl, string closeurl)
        {
            Application.OpenURL(openurl);
        }
#endif
        
        public event Action<string> WebViewDoneEvent;
        public static WebView Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        protected override void StartProcessing()
        {
            OpenWebView(_current.Element.NavigateToUrl, _current.Element.CloseWhenNavigatedToUrl);
        }

        public string parseErrorFormUrl(string url)
        {
			if (url.Contains("cancel=1") || url.Contains("fail=1")||url.Contains("error=access_denied") )
            {
                return "Canceled by user";
            }
            if (url.Contains("network_error=1"))
            {
                return "Network error";
            }
            return null;
        }

        private void OnWebViewDoneIntrnal(string url)
        {
			Debug.Log("InternalWebView");
            _current.Element.LastUrlWithParams = url;
            var error = parseErrorFormUrl(url);

            if (!string.IsNullOrEmpty(error))
            {
                _current.Element.Error = new WebViewError(url, error);
            }
            _current.Element.CallbackAction(_current.Element);

            ProccessNext();
        }

        public void WebViewDone(string url)
        {
			Debug.Log("webview done with url "+url);
            //example  http://web.com?param1=a&param2=b&errormsg=no_network
            if (WebViewDoneEvent != null)
                WebViewDoneEvent(url);

            OnWebViewDoneIntrnal(url);
        }
    }

}