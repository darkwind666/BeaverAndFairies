using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public class VKList<T>
    {
        public int count { get; set; }

        public List<T> items { get; set; }
    }
}
