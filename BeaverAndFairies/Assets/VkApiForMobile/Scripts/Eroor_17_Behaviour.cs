namespace com.playGenesis.VkUnityPlugin
{
    public class Eroor_17_Behaviour : QueueWorker<VKRequest>
    {
        private VkApi _vkapi;

        private void Start()
        {
            _vkapi = VkApi.VkApiInstance;
        }

        protected override void StartProcessing()
        {
#if UNITY_EDITOR
            GlobalErrorHandler.Instance.YesNoMessageBox.Add(
                new YesNoTaskData
                {
                    CustomData = new object[] { _current.Element},
                    OnYesButton =
                        () => { WebView.Instance.WebViewDone("https://oauth.vk.com/blank.html#success=1"); },
                    OnNoButton =
                        () => { WebView.Instance.WebViewDone("https://oauth.vk.com/blank.html#cancel=1"); },
                    Message = "Select the same you did in browser"
                }
           );
#endif
            WebView.Instance.Add(new WebViewRequest
            {
                CallbackAction = OnRequestFinished,
                NavigateToUrl = Utilities.ParseConfirmationUrl(_current.Element.response),
                CloseWhenNavigatedToUrl = "https://oauth.vk.com/blank.html"
            });
        }

        private void OnRequestFinished(WebViewRequest e)
        {
            if (e.Error == null)
            {
                _vkapi.Call(_current.Element);
                ProccessNext();
            }
            else
            {
                _current.Element.error.error_code = "15";
                _current.Element.error.error_msg = "Access Denied";
                _current.Element.CallBackFunction(_current.Element);
                ProccessNext();
            }
        }
    }
}