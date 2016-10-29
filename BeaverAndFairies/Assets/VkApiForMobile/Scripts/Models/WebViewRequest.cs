using UnityEngine;
using System.Collections;
using System;

namespace com.playGenesis.VkUnityPlugin
{
    public class WebViewRequest
    {
        public string NavigateToUrl { get; set; }
        public string CloseWhenNavigatedToUrl { get; set; }
        public Action<WebViewRequest> CallbackAction { get; set; }
        public WebViewError Error { get; set; }
        public string LastUrlWithParams { get; set; }

    }

}



