using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKChat
    {
		public static VKChat Deserialize(object Chat)
		{
			var _chat=new VKChat();
			var data=(Dictionary<string,object>)Chat;
			object type,id,title,admin_id,users,photo_100,photo_200;
		
			if(data.TryGetValue("type",out type))
				_chat.type=(string)type;

			if(data.TryGetValue("id",out id))
				_chat.id=(long)id;

			if(data.TryGetValue("title",out title))
				_chat.title=(string)title;

			if(data.TryGetValue("admin_id",out admin_id))
				_chat.admin_id=(long)admin_id;

			if (data.TryGetValue ("users", out users)) {
				_chat.users=new List<long>();
				foreach (var i in (List<object>)users) {
					_chat.users.Add((long)i);
				}
			}
				

			
			if(data.TryGetValue("photo_100",out photo_100))
				_chat.photo_100=(string)photo_100;

			if(data.TryGetValue("photo_200",out photo_200))
				_chat.photo_200=(string)photo_200;

			return _chat;
		}



		public string type { get; set; }
    

        public long id { get; set; }

		public string title { get; set; }
        
        public long admin_id { get; set; }
        public List<long> users { get; set; }

        public string photo_100 { get; set; }

        public string photo_200 { get; set; }
    }
}
