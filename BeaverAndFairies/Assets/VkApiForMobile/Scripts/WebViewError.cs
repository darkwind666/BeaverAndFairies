using UnityEngine;
using System.Collections;

namespace com.playGenesis.VkUnityPlugin
{
    public class WebViewError
    {
        public WebViewErrorType ErrorType = WebViewErrorType.UknownError;

        public WebViewError(string url, string error)
        {
            FailedUrl = url;

            if (error.Contains("Canceled by user"))
            {
                ErrorType = WebViewErrorType.CanceledByUser;
            }
            else if (error.Contains("Network error"))
            {
                ErrorType = WebViewErrorType.NetworkError;
            }

        }

        public string FailedUrl { get; set; }
    }

    public sealed class WebViewErrorType
    {
        public static readonly WebViewErrorType CanceledByUser = new WebViewErrorType(1, "Canceled by user");
        public static readonly WebViewErrorType NetworkError = new WebViewErrorType(3, "Network error");
        public static readonly WebViewErrorType UknownError = new WebViewErrorType(4, "Undefined error");

        private readonly string name;
        private readonly int value;

        private WebViewErrorType(int value, string name)
        {
            this.name = name;
            this.value = value;
        }

        public static explicit operator string(WebViewErrorType h)
        {
            return h.name;
        }
    }
}