using UnityEngine;

namespace com.playGenesis.VkUnityPlugin
{
    public class GlobalErrorHandler : MonoBehaviour
    {
        public static GlobalErrorHandler Instance;
        public CaptchaDialog CaptchaDialog;
        public Eroor_17_Behaviour Error_17_worker;
        public VKNotificationUI Notification;
        public VKYesNoMessageBox YesNoMessageBox;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Update()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.SetAsLastSibling();
                    return;
                }
                transform.SetAsFirstSibling();
            }
        }

        private void Start()
        {
            VkApi.VkApiInstance.SubscribeToGlobalErrorEvent(OnGlobalError);
        }


        public void OnGlobalError(VKRequest request)
        {
            if (request.error.error_code == "17")
                handle_17_Error(request);
            else if (request.error.error_code == "14")
                handleCaptchaError(request);
            else if (request.error.error_code == "24")
                handleNeedToShowMessageToUser(request);
            else if (request.error.error_code == "404")
                handleNetworkError(request);
            else
                request.CallBackFunction(request);
        }


        private void handleNetworkError(VKRequest request)
        {
            if (request.attempt < 5)
            {
                request.attempt++;
                VkApi.VkApiInstance.Call(request);
            }
            else
            {
                request.CallBackFunction(request);
            }
        }

        private void handleCaptchaError(VKRequest request)
        {
            CaptchaDialog.Add(request);
        }

        private void handleNeedToShowMessageToUser(VKRequest r)
        {
            YesNoMessageBox.Add(new YesNoTaskData
            {
               
                OnYesButton = () =>
                {
                    r.fullurl += "&confirm=1";
                    VkApi.VkApiInstance.Call(r);
                },
                OnNoButton = () => { r.CallBackFunction(r); },
                Message = Utilities.ParseConfirmationText(r.response)
            });
        }

        private void handle_17_Error(VKRequest request)
        {
            Error_17_worker.Add(request);
        }

    }
}