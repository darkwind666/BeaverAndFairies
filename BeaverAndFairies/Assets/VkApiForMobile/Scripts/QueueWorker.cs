using UnityEngine;
using System.Collections;

namespace com.playGenesis.VkUnityPlugin
{
    public class QueueWorker<T>:MonoBehaviour
    {
        protected Conctenated<T> _current;
        protected void AddNextElement(Conctenated<T> target, Conctenated<T> member)
        {
            if (target.NextElement == null)
            {
                target.NextElement = member;
            }
            else
            {
                AddNextElement(target.NextElement, member);
            }
        }
        public void Add(T element)
        {
            if (_current == null)
            {
                _current = new Conctenated<T>(element);
                StartProcessing();
            }
            else
            {
                AddNextElement(_current, new Conctenated<T>(element));
            }
        }

        protected virtual void StartProcessing()
        {

        }
        protected void ProccessNext()
        {
            _current = _current.NextElement;
            if (_current == null)
                return;
            StartProcessing();
        }
    }

}
