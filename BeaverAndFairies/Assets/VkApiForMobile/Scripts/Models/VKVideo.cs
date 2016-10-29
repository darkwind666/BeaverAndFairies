using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    /// <summary>
    /// https://vk.com/dev/video_object
    /// </summary>
    public partial class VKVideo
    {
		public static VKVideo Deserialize (object video)
		{
			var data = (Dictionary<string,object>)video;
			var _video = new VKVideo();
			
			object id, owner_id, title, duration, date, views, photo_130, photo_320, photo_640, player;
			if (data.TryGetValue ("id", out id))
				_video.id = (long)id;
			if (data.TryGetValue ("owner_id", out owner_id))
				_video.owner_id = (long)owner_id;
			if (data.TryGetValue ("title", out title))
				_video.title = (string)title;
			if (data.TryGetValue ("duration", out duration))
				_video.duration = (int)(long)duration;
			if (data.TryGetValue ("date", out date))
				_video.date = (int)(long)date;
			if (data.TryGetValue ("views", out views))
				_video.views = (int)(long)views;
			if (data.TryGetValue ("photo_130", out photo_130))
				_video.photo_130 = (string)photo_130;
			if (data.TryGetValue ("photo_320", out photo_320))
				_video.photo_320 = (string)photo_320;
			if (data.TryGetValue ("photo_640", out photo_640))
				_video.photo_640 = (string)photo_640;
			if (data.TryGetValue ("player", out player))
				_video.player = (string)player;
			if (data.TryGetValue ("id", out id))
				_video.id = (long)id;
			return _video;
		}
        public long id
        {
            get;
            set;
        }

        public long owner_id { get; set; }

		public string title { get; set; }
        
        public int duration { get; set; }


		public string description { get; set; }
        
        public int date { get; set; }
        public int views { get; set; }

        public string photo_130
        {
            get;
            set;
        }

        public string photo_320
        {
            get;
            set;
        }

        public string photo_640
        {
            get;
            set;
        }

        public string player { get; set; }
    }
}
