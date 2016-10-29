using System;
using System.Collections;
using UnityEngine;

namespace com.playGenesis.VkUnityPlugin
{
    public class Downloader : MonoBehaviour
    {
        public void download(DownloadRequest d)
        {
            StartCoroutine(_download(d));
        }

        private IEnumerator _download(DownloadRequest d)
        {
            var request = d.url;
            var www = new WWW(Uri.EscapeUriString(request));
            yield return www;
            d.DownloadResult = www;
            if (d.onFinished != null)
                d.onFinished(d);
        }
    }

    public class DownloadRequest
    {
        public string url { get; set; }
        public Action<DownloadRequest> onFinished { get; set; }
        public WWW DownloadResult { get; set; }
        public object[] CustomData { get; set; }
    }
}