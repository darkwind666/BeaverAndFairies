using System;
using UnityEngine;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin.MiniJSON;


namespace com.playGenesis.VkUnityPlugin
{
	public class Utilities
	{
		public static string GenerateFullHttpReqString(string request)
		{
			var vsetts = VkApi.VkSetts;
			var currentToken = VkApi.CurrentToken;
			string _request = request;
			//adding access token and api version info to request
			if (request.EndsWith ("?")) {
				_request = _request + "v=" + vsetts.apiVersion + "&access_token=" + currentToken.access_token + "&https=1";
			} else {
				_request = _request + "&v=" + vsetts.apiVersion + "&access_token=" + currentToken.access_token + "&https=1";
			}
			_request=System.Uri.EscapeUriString (_request);
			while (_request.Contains("#")) {
				_request=_request.Replace ("#", "%23");
			}
			return VkApi.VkApiInstance.VkRequestUrlBase + _request;
		}

		public static Dictionary<string,string> ParseUrlParams(string fullurl){
			var result = new Dictionary<string,string> ();
			var parametersUrlPart = fullurl.Split ('#')[1];
			var parametersArray = parametersUrlPart.Split ('&');
			foreach (var parameter in parametersArray) {
				var name =parameter.Split('=')[0];
				var value=parameter.Split('=')[1];
				result.Add(name,value);
			}
			return result;
		}

		public static string ParseConfirmationUrl (string response)
		{
			var dict=Json.Deserialize(response) as Dictionary<string,object>;
			var resp= (Dictionary<string,object>)dict["error"];
			var eroor_url=(string)resp["redirect_uri"];
			return eroor_url;
			
		}
        public static string ParseConfirmationText(string response)
        {
            var dict = Json.Deserialize(response) as Dictionary<string, object>;
            var resp = (Dictionary<string, object>)dict["error"];
            return (string)resp["confirmation_text"]; 
        }
   
    }
}

