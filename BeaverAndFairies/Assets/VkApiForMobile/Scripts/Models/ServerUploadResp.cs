using UnityEngine;
using System.Collections;

namespace com.playGenesis.VkUnityPlugin
{
	public class ServerRespOnPhotoUpload
	{
		public int server { get; set; }
		public string photos_list { get; set; }
		public int aid { get; set; }
		public string hash { get; set; }
		public int gid { get; set; }
	}
	public class GetUploadServerResp{
		public string upload_url { get; set; }
		public int album_id { get; set; }
		public int user_id { get; set; }
	}
}