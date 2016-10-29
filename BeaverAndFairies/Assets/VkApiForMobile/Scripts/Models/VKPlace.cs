using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class Place
    {
		public static Place Deserialize (object place)
		{
			var data=(Dictionary<string,object>)place;
			var _place=new Place();

			object title, address, latitude, longitude, country, icon, type,
			group_id, group_photo, checkins,updated;

			if (data.TryGetValue ("title", out title))
				_place.title = (string)title;
			if (data.TryGetValue ("address", out address))
				_place.address = (string)address;
			if (data.TryGetValue ("latitude", out latitude))
				_place.latitude = (double)latitude;
			if (data.TryGetValue ("longitude", out longitude))
				_place.longitude = (double)longitude;
			if (data.TryGetValue ("country", out country))
				_place.country = (string)country;
			if (data.TryGetValue ("icon", out icon))
				_place.icon = (string)icon;
			if (data.TryGetValue ("type", out type))
				_place.type = (string)title;
			if (data.TryGetValue ("group_id", out group_id))
				_place.group_id = (long)group_id;
			if (data.TryGetValue ("group_photo", out group_photo))
				_place.group_photo = (string)group_photo;
			if (data.TryGetValue ("checkins", out checkins))
				_place.checkins = (int)(long)checkins;
			if (data.TryGetValue ("updated", out updated))
				_place.updated = (long)updated;
			return _place;
		}

       
		public string title { get; set; }
       
		public string address { get; set; }
        

        public double latitude { get; set; }

        public double longitude { get; set; }
     
        public string country { get; set; }

        public string city { get; set; }

        public string icon { get; set; }

        public string type { get; set; }

        public long group_id { get; set; }

        public string group_photo { get; set; }

        public int checkins { get; set; }

        public long updated { get; set; }

    }
}
