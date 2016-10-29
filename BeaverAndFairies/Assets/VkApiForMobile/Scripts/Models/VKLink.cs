using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKLink
    {
		public static VKLink Deserialize(object link)
		{
			var _link=new VKLink();
			var data=(Dictionary<string,object>)link;
			object url,title,description,image_src;

			if(data.TryGetValue("url",out url))
				_link.url=(string)url;
			if(data.TryGetValue("title",out title))
				_link.title=(string)title;
			if(data.TryGetValue("description",out description))
				_link.description=(string)description;
			if(data.TryGetValue("image_src",out image_src))
				_link.image_src=(string)image_src;
			return _link;

		}
        public string url { get; set; }
       
		public string title { get; set; }
        
		public string description { get; set; }
        
        public string image_src { get; set; }
    }
}
