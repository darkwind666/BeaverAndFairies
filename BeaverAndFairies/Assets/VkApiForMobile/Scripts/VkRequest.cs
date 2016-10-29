using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace com.playGenesis.VkUnityPlugin 
{
	[System.Serializable]
	public class VKRequest {
		private string _url="";
		public  string url{
			get{
				return _url;
			}
			set{
				_url=value;
				fullurl="";
			}
		}
		public string fullurl="";
		public string response;
		public Error error;
		public int attempt;
		public Action<VKRequest> CallBackFunction;
		public object[] data;
		public bool needsToBeeConfirmed;
	}
}