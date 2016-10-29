using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKAudio
    {
		public static VKAudio Deserialize(object Audio)
		{
			object id,owner_id,artist,title,duration,url,lyrics_id,album_id,genre_id;
			var data=(Dictionary<string,object>)Audio;
			var audio_des=new VKAudio();

			if(data.TryGetValue("id",out id))
				audio_des.id=(long)id;
			if(data.TryGetValue("owner_id",out owner_id))
				audio_des.owner_id=(long)owner_id;
			if(data.TryGetValue("artist",out artist))
				audio_des.artist=(string)artist;
			if(data.TryGetValue("title",out title ))
				audio_des.title=(string)title;
			if(data.TryGetValue("duration",out duration ))
				audio_des.duration=(int)(long)duration;
			if(data.TryGetValue("url",out url ))
				audio_des.url=(string)url;
			if(data.TryGetValue("lyrics_id",out lyrics_id))
				audio_des.lyrics_id=(long)lyrics_id;
			if(data.TryGetValue("album_id",out album_id))
				audio_des.album_id=(long)album_id;
			if(data.TryGetValue("genre_id",out genre_id))
				audio_des.genre_id=(long)genre_id;

			return audio_des;
		}

        public long id { get; set; }

        public long owner_id { get; set; }

		public string artist  { get; set; }

		public string title  { get; set; }

        public int duration { get; set; }

        public string url { get; set; }

        public long lyrics_id { get; set; }

        public long album_id { get; set;}

        public long genre_id { get; set; }
    }
}
