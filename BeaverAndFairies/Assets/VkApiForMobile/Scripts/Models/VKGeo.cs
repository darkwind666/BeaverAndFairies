using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKGeo
    {
		public static VKGeo Deserialize(object geo)
		{
			var data=(Dictionary<string,object>)geo;
			var _geo=new VKGeo();
			object type,coordinates,place;

			if(data.TryGetValue("type",out type))
				_geo.type=(string)type;

			if(data.TryGetValue("coordinates",out coordinates))
				_geo.coordinates=(string)coordinates;

			if(data.TryGetValue("place",out place))
				_geo.place=Place.Deserialize(place);

			return _geo;
		}
        public string type { get; set; }
        public string coordinates { get; set; }

        public Place place { get; set; }
    }
}
