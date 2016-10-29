using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKNote
    {
		public static VKNote Deserialize (object note)
		{
			var data = (Dictionary<string,object>)note;
			var _note = new VKNote ();
			object id, user_id, owner_id, title, text, comments, read_comments, view_url;
			if(data.TryGetValue("id",out id ))
				_note.id=(long)id;
			if(data.TryGetValue("user_id",out user_id ))
				_note.user_id=(long)user_id;
			if(data.TryGetValue("owner_id",out owner_id ))
				_note.owner_id=(long)owner_id;
			if(data.TryGetValue("text",out text ))
				_note.text=(string)text;
			if(data.TryGetValue("title",out title ))
				_note.title=(string)title;
			if(data.TryGetValue("comments",out comments ))
				_note.comments=(int)(long)comments;
			if(data.TryGetValue("read_comments",out read_comments ))
				_note.read_comments=(int)(long)read_comments;
			if(data.TryGetValue("view_url",out view_url ))
				_note.view_url=(string)view_url;
			return _note;
		}

        public long id { get; set; }

        public long user_id { get; set; }
        public long owner_id { get { return user_id; } set { user_id = value; } }

       
		public string title  { get; set; }
       
		public string text  { get; set; }
       

        public int comments { get; set; }

        public int read_comments { get; set; }

        public string view_url { get; set; }
    }
}
