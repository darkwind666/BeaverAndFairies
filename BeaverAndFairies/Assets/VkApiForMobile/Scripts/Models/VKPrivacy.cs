using System;

using System.Collections.Generic;

namespace com.playGenesis.VkUnityPlugin
{  
    /// <summary>
    /// https://vk.com/dev/privacy_setting
    /// </summary>
    public partial class VKPrivacy
    {
		public static VKPrivacy Deserialize(object Privacy) 
		{
			VKPrivacy _privacy=new VKPrivacy();
			var data=(Dictionary<string,object>)Privacy;
			object type,users,lists,except_lists,except_users;

			if(data.TryGetValue("type",out type))
				_privacy.type=(string)type;
			if (data.TryGetValue ("users", out users)) {
				_privacy.users = new List<long>();
				foreach (var i in (List<object>)users) {
					_privacy.users.Add((long)i);
				}
			}
			if (data.TryGetValue ("lists", out lists)) {
				_privacy.lists = new List<long>();
				foreach (var i in (List<object>)lists) {
					_privacy.lists.Add((long)i);
				}
			}
			if (data.TryGetValue ("except_lists", out except_lists)) {
				_privacy.except_lists = new List<long>();
				foreach (var i in (List<object>)except_lists) {
					_privacy.except_lists.Add((long)i);
				}
			}
			if (data.TryGetValue ("except_users", out except_users)) {
				_privacy.except_users = new List<long>();
				foreach (var i in (List<object>)except_users) {
					_privacy.except_users.Add((long)i);
				}
			}
			return _privacy;

		}
        public string type {get;set;}

        public List<long> users {get;set;}

        public List<long> lists {get;set;}

        public List<long> except_lists {get;set;}        

        public List<long> except_users {get;set;}
    }

}
