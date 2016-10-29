using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;

namespace com.playGenesis.VkUnityPlugin
{
    public class Conctenated<T>
    {
        public T Element { get; set; }
        public Conctenated<T> NextElement { get; set; }

        public Conctenated(T element)
        {
            Element = element;
        }

    }
}