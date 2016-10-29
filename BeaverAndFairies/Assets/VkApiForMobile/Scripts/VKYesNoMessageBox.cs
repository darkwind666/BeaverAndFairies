using UnityEngine;
using UnityEngine.UI;

namespace com.playGenesis.VkUnityPlugin
{
    public class VKYesNoMessageBox : QueueWorker<YesNoTaskData>
    {
        public Text Message;

        private void Start()
        {
            Message = GetComponentInChildren<Text>();
        }

       


        protected override void StartProcessing()
        {
            gameObject.SetActive(true);
            Message.text = _current.Element.Message;
        }

        public void OkButtonClicked()
        {
            _current.Element.OnYesButton();
            if (_current.NextElement == null)
            {
                gameObject.SetActive(false);
            }
            ProccessNext();
        }

        public void CancelButtonClicked()
        {
            _current.Element.OnNoButton();
            if (_current.NextElement == null)
            {
                gameObject.SetActive(false);
            }
           ProccessNext();
            
        }
    }
}