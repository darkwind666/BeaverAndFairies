using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
using System.Collections.Generic;
using System;
using com.playGenesis.VkUnityPlugin.MiniJSON;
namespace com.playGenesis.VkUnityPlugin 
{
	public class VKShare {
		VkApi vkapi;
		string _text;
		List<ShareImage> _images;
		string _link;
		int _imagesToUpload;
		List<long> _photoIds=new List<long>();
		long _group_id=0;
		public static int RepeatRequest=5;
#pragma warning disable 0414
		Action<VKRequest> _callbackFunction;
#pragma warning restore 0414 

		public VKShare (Action<VKRequest> CallbackFunction,
		                string text="", 
		                List<ShareImage> images=null,
		                string link="",
		                long group_id=0)
		{
			vkapi = VkApi.VkApiInstance;
			_text = text;
			_images = images;
			_link = link;
			_group_id = group_id;

			if (CallbackFunction == null)
				CallbackFunction = (r) => {Debug.LogError("The Callback Function is not optional in VkShare");};

			_callbackFunction = CallbackFunction;

		}

		public void Share()
		{
			if (_images == null) {
				PostToWall (RepeatRequest);
				return;
			}
			_imagesToUpload = _images.Count;
			var groupIdParam=_group_id==0?"":"group_id="+_group_id.ToString();
			VKRequest r = new VKRequest ()
			{
				url="photos.getWallUploadServer?"+groupIdParam,
				data=new object[]{RepeatRequest},
				CallBackFunction=GotWallUploadServer
			};
			vkapi.Call(r);

		}


		void GotWallUploadServer (VKRequest arg1)
		{
			if (arg1.error != null) {
				_callbackFunction(arg1);
				return;
			}

			var dict=Json.Deserialize(arg1.response) as Dictionary<string,object>;
			var resp= (Dictionary<string,object>)dict["response"];
			var url=(string)resp["upload_url"];
			
			foreach(var i in _images)
			{
				var file =new FileForUpload{data=i.data,filename=i.imageName,mimeType=(string)i.imagetype};
				VKRequest r= new VKRequest(){
					fullurl=url,
					CallBackFunction=PhotoHasBeenUploaded,
					data=new object[]{RepeatRequest,file}
				};

				vkapi.UploadToserverCall(r,file);
				
			}

		}


		
		void PhotoHasBeenUploaded (VKRequest arg1)
		{
			if (arg1.error != null) {
				_callbackFunction(arg1);
				return;
			}

			var dict=Json.Deserialize(arg1.response) as Dictionary<string,object>;
			var server= (long)dict["server"];
			var photo=(string)dict["photo"];
			var hash=(string)dict["hash"];
			var groupIdParam=_group_id==0?"":"&group_id="+_group_id.ToString();
			VKRequest r = new VKRequest (){
				url="photos.saveWallPhoto?photo="+photo+"&server="+server+"&hash="+hash+groupIdParam, 
			    CallBackFunction= OnPhotoSaved,
				data=new object[]{RepeatRequest}
			};
			vkapi.Call(r);


		}

		void OnPhotoSaved (VKRequest arg1)
		{
			if (arg1.error != null) {
				_callbackFunction(arg1);
				return;
			}

			var dict=Json.Deserialize(arg1.response) as Dictionary<string,object>;
			var resp= (List<object>)dict["response"];
			var photo = VKPhoto.Deserialize (resp [0]);
			_photoIds.Add (photo.id);
			_imagesToUpload--;
			if (_imagesToUpload == 0) {
				
				PostToWall(RepeatRequest);
			}

		}
		string GenerateAttachementsForWall()
		{
			string result = "";
			var idParam = _group_id == 0 ? VkApi.CurrentToken.user_id : "-"+_group_id;

			foreach (var id in _photoIds) {
				result=result+"photo"+idParam+"_"+id+",";
			}

			return result.Substring (0, result.Length - 1);
		}

		void PostToWall(int attemptsLeft)
		{
			var requestString ="wall.post?" ;

			if(!string.IsNullOrEmpty(_text))
				requestString+="message="+_text;
			if (_images != null)
				requestString += "&attachments=" + GenerateAttachementsForWall ();
			if (_link != null)
				requestString += "," + _link;
			VKRequest r = new VKRequest (){
				url=requestString,
				CallBackFunction=WhenPosted,
				data=new object[]{attemptsLeft}
			};
			vkapi.Call(r);
		}

		void WhenPosted (VKRequest arg1)
		{
			_callbackFunction(arg1);
			Debug.Log ("Finished Sharing");
		}
	}
}