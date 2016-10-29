using UnityEngine;
using System.Collections;
using System;

namespace com.playGenesis.VkUnityPlugin
{
	public class PersistentToke {
		public string access_token;
		public int expires_in;
		public string _user_id;
		public string user_id{
			get{
				return _user_id;
			}
			set{
				_user_id=value;
			}
		}
		public DateTime tokenRecievedTime;
		public bool tokenFromEditor;

	   
	}
}

