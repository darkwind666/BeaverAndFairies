using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.playGenesis.VkUnityPlugin
{
    public class VKNotificationUI : QueueWorker<String>
    {
        public Text message;

        private void Start()
        {
            message = gameObject.GetComponentInChildren<Text>();
        }

        public void Notity(string message)
        {
            Add(message);
        }

        protected override void StartProcessing()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            message.text = _current.Element;
        }
        public void Notify(VKRequest r)
        {
            if (!string.IsNullOrEmpty(r.error.error_msg))
            {
                Add(r.error.error_msg);
            }
            
        }

        public void onOkButton()
        {
            message.text = "";
            if (_current.NextElement == null)
            {
                gameObject.SetActive(false);
            }
            ProccessNext();
        }
    }
}