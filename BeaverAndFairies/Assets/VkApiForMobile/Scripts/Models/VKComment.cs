using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKComment
    {
		public static VKComment Deserialize(object Comment)
		{
			var data=(Dictionary<string,object>)Comment;
			var _comment=new VKComment();
			object id,from_id,date,text,reply_to_user,reply_to_comment,attachments;
			if(data.TryGetValue("id",out id))
				_comment.id=(long)id;
			if(data.TryGetValue("from_id",out from_id))
				_comment.from_id=(long)from_id;
			if(data.TryGetValue("date",out date))
				_comment.date=(long)date;
			if(data.TryGetValue("text",out text))
				_comment.text=(string)text;
			if(data.TryGetValue("reply_to_user",out reply_to_user))
				_comment.reply_to_user=(long)reply_to_user;
			if(data.TryGetValue("reply_to_comment",out reply_to_comment))
				_comment.reply_to_comment=(long)reply_to_comment;
			if(data.TryGetValue("attachments",out attachments))
			{
				var att=(List<object>)attachments;
				var _attachments=new List<VKAttachment>();
				foreach (var a in att) {
					_attachments.Add(VKAttachment.Deserialize(a));
				}
				_comment.attachments=_attachments;
			}
			return _comment;

		}
        public long id { get; set; }

        public long from_id { get; set; }

        public long date { get; set; }

        private string text { get; set; }
        
        public long reply_to_user { get; set; }

        public long reply_to_comment { get; set; }

        public List<VKAttachment> attachments { get; set; }
    }
}
