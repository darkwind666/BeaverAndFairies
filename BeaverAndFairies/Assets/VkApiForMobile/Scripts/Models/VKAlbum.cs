using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKAlbum
    {
		public static VKAlbum Deserialize(object Album)
		{
			var data=(Dictionary<string,object>)Album;
			var _album=new VKAlbum();
			object id,thumb_id,owner_id,title,description,created,updated,size,thumb_src,privacy_view,privacy_comment;

			if(data.TryGetValue("id",out id))
				_album.id=(string)id;
			if(data.TryGetValue("thumb_id",out thumb_id))
				_album.thumb_id=(string)thumb_id;
			if(data.TryGetValue("owner_id",out owner_id))
				_album.owner_id=(string)owner_id;
			if(data.TryGetValue("title",out title))
				_album.title=(string)title;
			if(data.TryGetValue("description",out description))
				_album.description=(string)description;
			if(data.TryGetValue("created",out created))
				_album.created=(string)created;

			if(data.TryGetValue("updated",out updated))
				_album.updated=(string)updated;

			if(data.TryGetValue("size",out size))
				_album.size=(int)(long)size;

			if(data.TryGetValue("thumb_src",out thumb_src))
				_album.thumb_src=(string)thumb_src;

			if(data.TryGetValue("privacy_view",out privacy_view))
				_album.privacy_view=VKPrivacy.Deserialize(privacy_view);

			if(data.TryGetValue("privacy_comment",out privacy_comment))
				_album.privacy_comment=VKPrivacy.Deserialize(privacy_comment);
			return _album;
		}	

        public string id { get; set; }

        public string thumb_id { get; set; }

        public string owner_id { get; set; }

		public string title { get; set; }
        
		public string description { get; set; }
        

        public string created { get; set; }

        public string updated { get; set; }

        public int size { get; set; }

        public string thumb_src { get; set; }

        public VKPrivacy privacy_view { get; set; }

        public VKPrivacy privacy_comment { get; set; }
        
    }
}
