using UnityEngine;
using System.Collections;
using System;

namespace com.playGenesis.VkUnityPlugin
{
    public class VKToken : EventArgs
    {
        public string access_token;
        public int expires_in;
        public string user_id;
        public DateTime tokenRecievedTime;

        public static VKToken ParseSerializeTokenFromNaviteSdk(string message)
        {
            //serialization format: token#expires_in#user_id

            string[] token = message.Split('#');
            VKToken ti = new VKToken();

            ti.access_token = token[0];
            ti.tokenRecievedTime = DateTime.Now;
            ti.expires_in = int.Parse(token[1]) == 0 ? 999999 : int.Parse(token[1]);
            ti.user_id = token[2];
            return ti;
        }

        public static bool IsValidToken(VKToken ti)
        {
            var currentToken = ti;
            var isvalid = currentToken.tokenRecievedTime.AddSeconds(currentToken.expires_in) > DateTime.Now
                ? true
                : false;
            return isvalid;
        }
        public static int TokenValidFor()
        {
            var currentToken = VkApi.CurrentToken;
            return
                (int) (currentToken.tokenRecievedTime.AddSeconds(currentToken.expires_in) - DateTime.Now).TotalSeconds;
        }

        public static void ResetToken()
        {
			VkApi.CurrentToken = new VKToken{
				access_token="",
				tokenRecievedTime=DateTime.Parse("1/1/1992"),
				expires_in=1,
				user_id="",
			};
            PlayerPrefs.SetString("vkaccess_token", "");
            PlayerPrefs.SetInt("vkexpires_in", 0);
            PlayerPrefs.SetString("vkuser_id", "");
            PlayerPrefs.SetString("vktokenRecievedTime", "1/1/1992");

        }
        
        public void Save()
        {
            PlayerPrefs.SetString("vkaccess_token", access_token);
            PlayerPrefs.SetInt("vkexpires_in", expires_in);
            PlayerPrefs.SetString("vkuser_id", user_id);
            PlayerPrefs.SetString("vktokenRecievedTime", tokenRecievedTime.ToString());
        }

        public static VKToken LoadPersistent()
        {

            DateTime recievedtokentime= DateTime.Parse("1/1/1990");
            DateTime.TryParse(PlayerPrefs.GetString("vktokenRecievedTime", ""), out recievedtokentime);
            return new VKToken
            {
                access_token = PlayerPrefs.GetString("vkaccess_token", ""),
                expires_in = PlayerPrefs.GetInt("vkexpires_in", 0),
                tokenRecievedTime = recievedtokentime,
                user_id = PlayerPrefs.GetString("vkuser_id", "")
            };
        }

        public static string ParseFromAuthUrl(string url)
        {
            var prms = url.Split('#')[1].Split('&');
            string access_token="", expires_in="", user_id = "";
            
            foreach (var p in prms)
            {
                var pName = p.Split('=')[0];
                var pValue= p.Split('=')[1];

                if (pName == "access_token")
                {
                    access_token = pValue;
                }else if (pName == "expires_in")
                {
                    expires_in = pValue;
                }else if (pName == "user_id")
                {
                    user_id = pValue;
                }

            }
            return access_token + "#" + expires_in + "#" + user_id;
        }
    }
}
