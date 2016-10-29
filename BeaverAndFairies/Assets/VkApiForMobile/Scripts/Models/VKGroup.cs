using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace com.playGenesis.VkUnityPlugin
{
   /// <summary>
   /// https://vk.com/dev/fields_groups
   /// </summary>
    
	public partial class VKGroup
    {
		public static VKGroup Deserialise(object group){
			var data = (Dictionary<string,object>)group;
			var _group = new VKGroup ();
			object id, name, screen_name, is_closed, deactivated, is_admin, admin_level, is_member;
			if (data.TryGetValue ("id", out id))
				_group.id = (long)id;

			if (data.TryGetValue ("name", out name))
				_group.name = (string)name;

			if (data.TryGetValue ("screen_name", out screen_name))
				_group.screen_name = (string)screen_name;

			if (data.TryGetValue ("is_closed", out is_closed))
				_group.is_closed = (int)(long)is_closed;

			if (data.TryGetValue ("deactivated", out deactivated))
				_group.deactivated = (string)deactivated;

			if (data.TryGetValue ("is_admin", out is_admin))
				_group.is_admin = (int)(long)is_admin;

			if (data.TryGetValue ("admin_level", out admin_level))
				_group.admin_level = (int)(long)admin_level;

			if (data.TryGetValue ("is_member", out is_member))
				_group.is_member = (int)(long)is_member;

			object type,photo_50,photo_100,photo_200,city,country,place,description,wiki_page;

			if (data.TryGetValue ("type", out type))
				_group.type = (string)type;
			if (data.TryGetValue ("photo_50", out photo_50))
				_group.photo_50 = (string)photo_50;
			if (data.TryGetValue ("photo_100", out photo_100))
				_group.photo_100 = (string)photo_100;
			if (data.TryGetValue ("photo_200", out photo_200))
				_group.photo_200 = (string)photo_200;
			if (data.TryGetValue ("city", out city))
				_group.city = (long)city;
			if (data.TryGetValue ("country", out country))
				_group.country = (long)country;
			if (data.TryGetValue ("place", out place))
				_group.place = VKPlace.Deserialize(place);
			if (data.TryGetValue ("description", out description))
				_group.description = (string)description;
			if (data.TryGetValue ("wiki_page", out wiki_page))
				_group.wiki_page = (string)wiki_page;

			object members_count,counters,start_date,finish_date,can_post,can_see_all_posts,can_upload_doc,can_create_topic;

			if (data.TryGetValue ("members_count", out members_count))
				_group.members_count = (int)(long)members_count;
			if (data.TryGetValue ("counters", out counters))
				_group.counters = VKCounters.Deserialize(counters);
			if (data.TryGetValue ("start_date", out start_date))
				_group.start_date = (long)start_date;
			if (data.TryGetValue ("finish_date", out finish_date))
				_group.finish_date = (long)finish_date;
			if (data.TryGetValue ("can_post", out can_post))
				_group.can_post = (int)(long)can_post;
			if (data.TryGetValue ("can_see_all_posts", out can_see_all_posts))
				_group.can_see_all_posts =(int)(long)can_see_all_posts;
			if (data.TryGetValue ("can_upload_doc", out can_upload_doc))
				_group.can_upload_doc =(int) (long)can_upload_doc;
			if (data.TryGetValue ("can_create_topic", out can_create_topic))
				_group.can_create_topic = (int)(long)can_create_topic;

			object activity,status,contacts,links,fixed_post,verified,site;

			if (data.TryGetValue ("activity", out activity))
				_group.activity = (string)activity;
			if (data.TryGetValue ("status", out status))
				_group.status = (string)status;
			if (data.TryGetValue ("contacts", out contacts))
				_group.contacts = (string)contacts;
			if (data.TryGetValue ("links", out links))
				_group.links = (string)links;
			if (data.TryGetValue ("fixed_post", out fixed_post))
				_group.fixed_post = (long)fixed_post;
			if (data.TryGetValue ("verified", out verified))
				_group.verified = (int)(long)verified;
			if (data.TryGetValue ("site", out site))
				_group.site = (string)site;
			return _group;
		}
        public long id { get; set; }

        public string name { get; set; }

        public string screen_name { get; set; }

        public int is_closed { get; set; }

        public string deactivated { get; set; }

        public int is_admin { get; set; }

        public int admin_level { get; set; }

        public int is_member { get; set; }

        public string type { get; set; }

        public string photo_50 { get; set; }

        public string photo_100 { get; set; }

        public string photo_200 { get; set; }

        public long city { get; set; }

        public long country { get; set; }

        public VKPlace place { get; set; }

       
		public string description{ get; set;}
        

        public string wiki_page
		{
            get;
            set;
        }

        public int members_count
        {
            get;
            set;
        }

        public VKCounters counters
        {
            get;
            set;
        }

        public long start_date
        {
            get;
            set;
        }

        public long finish_date
        {
            get;
            set;
        }

        public int can_post
        {
            get;
            set;
        }

        public int can_see_all_posts
        {
            get;
            set;
        }

        public int can_upload_doc
        {
            get;
            set;
        }

        public int can_create_topic
        {
            get;
            set;
        }

        public string activity { get; set; }

        public string status { get; set; }

        public string contacts { get; set; }

        public string links { get; set; }

        public long fixed_post { get; set; }

        public int verified { get; set; }

        public string site { get; set; }

    }

    public class VKPlace
    {
		public static VKPlace Deserialize (object place)
		{
			var data=(Dictionary<string,object>)place;
			var _place=new VKPlace();
			object id,title,latitude,longitude,type,country,city,address;
			if(data.TryGetValue("id",out id))
				_place.id=(long)id;
			if(data.TryGetValue("title",out title))
				_place.title=(string)title;
			if(data.TryGetValue("latitude",out latitude))
				_place.latitude=(int)(long)latitude;
			if(data.TryGetValue("longitude",out longitude))
				_place.longitude=(int)(long)longitude;
			if(data.TryGetValue("type",out type))
				_place.type=(string)type;
			if(data.TryGetValue("country",out country))
				_place.country=(long)country;
			if(data.TryGetValue("city",out city))
				_place.city=(long)city;
			if(data.TryGetValue("address",out address))
				_place.address=(string)address;

			return _place;
		}

        public long id { get; set; }

        public string title { get; set; }

        public int latitude { get; set; }

        public int longitude { get; set; }

        public string type { get; set; }

        public long country { get; set; }

        public long city { get; set; }

        public string address { get; set; }
    }
}
