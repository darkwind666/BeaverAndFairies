using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKPage
    {
		public static VKPage Deserialize (object page)
		{
			var data = (Dictionary<string,object>)page;
			var _page = new VKPage ();
			object id, group_id, creator_id, title, current_user_can_edit, current_user_can_edit_access;
			object who_can_view, who_can_edit, edited, created;

			if(data.TryGetValue("id",out id ))
				_page.id=(long)id;
			if(data.TryGetValue("group_id",out group_id ))
				_page.group_id=(long)group_id;
			if(data.TryGetValue("creator_id",out creator_id ))
				_page.creator_id=(long)creator_id;
			if(data.TryGetValue("title",out title ))
				_page.title=(string)title;
			if(data.TryGetValue("current_user_can_edit",out current_user_can_edit ))
				_page.current_user_can_edit=(int)(long)current_user_can_edit;
			if(data.TryGetValue("current_user_can_edit_access",out current_user_can_edit_access ))
				_page.current_user_can_edit_access=(int)(long)current_user_can_edit_access;
			if(data.TryGetValue("who_can_view",out who_can_view ))
				_page.who_can_view=(int)(long)who_can_view;
			if(data.TryGetValue("who_can_edit",out who_can_edit ))
				_page.who_can_edit=(int)(long)who_can_edit;
			if(data.TryGetValue("edited",out edited ))
				_page.edited=(int)(long)edited;
			if(data.TryGetValue("created",out created ))
				_page.created=(int)(long)created;

			object editor_id,views,parent,parent2,source,html,view_url;
			if(data.TryGetValue("editor_id",out editor_id ))
				_page.editor_id=(long)editor_id;
			if(data.TryGetValue("views",out views ))
				_page.views=(int)(long)views;
			if(data.TryGetValue("parent",out parent ))
				_page.parent=(string)parent;
			if(data.TryGetValue("parent2",out parent2 ))
				_page.parent2=(string)parent2;
			if(data.TryGetValue("source",out source ))
				_page.source=(string)source;
			if(data.TryGetValue("html",out html ))
				_page.html=(string)html;
			if(data.TryGetValue("view_url",out view_url ))
				_page.view_url=(string)view_url;

			return _page;
		}

        public long id { get; set; }

        public long group_id { get; set; }

        public long creator_id { get; set; }

        public string title { get; set; }

        public int current_user_can_edit { get; set; }

        public int current_user_can_edit_access { get; set; }

        public int who_can_view { get; set;}

        public int who_can_edit { get; set; }

        public int edited { get; set; }

        public int created { get; set; }

        public long editor_id { get; set; }

        public int views { get; set; }

        public string parent { get; set; }

        public string parent2 { get; set; }

        public string source { get; set; }

        public string html { get; set; }

        public string view_url { get; set; }
    }
}
