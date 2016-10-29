using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKPhoto
    {
		public static VKPhoto Deserialize (object photo)
		{
			var data=(Dictionary<string,object>)photo;
			var _photo=new VKPhoto();
			object id, album_id, owner_id, user_id, photo_75, photo_130, photo_604, photo_807, photo_1280, photo_2560;

			if (data.TryGetValue ("id", out id))
				_photo.id = (long)id;
			if (data.TryGetValue ("album_id", out album_id))
				_photo.album_id = (long)album_id;
			if (data.TryGetValue ("owner_id", out owner_id))
				_photo.owner_id = (long)owner_id;
			if (data.TryGetValue ("user_id", out user_id))
				_photo.user_id = (long)user_id;
			if (data.TryGetValue ("photo_75", out photo_75))
				_photo.photo_75 = (string)photo_75;
			if (data.TryGetValue ("photo_130", out photo_130))
				_photo.photo_130 = (string)photo_130;
			if (data.TryGetValue ("photo_604", out photo_604))
				_photo.photo_604 = (string)photo_604;
			if (data.TryGetValue ("photo_807", out photo_807))
				_photo.photo_807 = (string)photo_807;
			if (data.TryGetValue ("photo_1280", out photo_1280))
				_photo.photo_1280 = (string)photo_1280;
			if (data.TryGetValue ("photo_2560", out photo_2560))
				_photo.photo_2560 = (string)photo_2560;

			object width, height, text, date;

			if (data.TryGetValue ("width", out width))
				_photo.width = (int)(long)width;
			if (data.TryGetValue ("height", out height))
				_photo.height = (int)(long)height;
			if (data.TryGetValue ("text", out text))
				_photo.text = (string)text;
			if (data.TryGetValue ("date", out date))
				_photo.date = (int)(long)date;

			return _photo;
		}

        public long id { get; set; }
        public long album_id
        {
            get;
            set;
        }
      
        public long owner_id { get; set; }
        public long user_id { get; set; }

        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public string photo_807 { get; set; }
        public string photo_1280 { get; set; }
        public string photo_2560 { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        
		public string text { get; set; }
       

        public int date { get; set; }
    }
}
