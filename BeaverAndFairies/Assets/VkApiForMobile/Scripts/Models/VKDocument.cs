using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKDocument
    {
		public static VKDocument Deserialize (object doc)
		{
			var data = (Dictionary<string,object>)doc;
			var _document = new VKDocument ();
			object id,owner_id,title,size,ext,url,photo_100,photo_130;
			if(data.TryGetValue("id",out id ))
			   _document.id=(long)id;
			if(data.TryGetValue("owner_id",out owner_id ))
				_document.owner_id=(long)owner_id;
			if(data.TryGetValue("title",out title ))
				_document.title=(string)title;
			if(data.TryGetValue("size",out size ))
				_document.size=(long)id;
			if(data.TryGetValue("ext",out ext ))
				_document.ext=(string)ext;
			if(data.TryGetValue("url",out url ))
				_document.url=(string)url;
			if(data.TryGetValue("photo_100",out photo_100 ))
				_document.photo_100=(string)photo_100;
			if(data.TryGetValue("photo_130",out photo_130 ))
				_document.photo_130=(string)photo_130;

			return _document;
		}

        public long id { get; set; }

        public long owner_id { get; set; }

		private string title{ get; set; }
      
        public long size { get; set; }
        public string ext { get; set; }
        public string url { get; set; }

        public string photo_100 { get; set; }
        public string photo_130 { get; set; }
    }
}
