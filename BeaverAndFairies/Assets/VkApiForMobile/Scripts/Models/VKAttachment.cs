using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKAttachment
    {
		public static VKAttachment Deserialize (object attachment)
		{
			var data=(Dictionary<string,object>)attachment;
			var _atachment=new VKAttachment();
			object type, audio, photo, poll, doc, link, wall, note, Page;
			
			if(data.TryGetValue("type",out type))
			   _atachment.type=(string) type;
			if(data.TryGetValue("audio",out audio))
				_atachment.audio=VKAudio.Deserialize(audio);
			if(data.TryGetValue("photo",out photo))
				_atachment.photo=VKPhoto.Deserialize(photo);
			if(data.TryGetValue("poll",out poll))
				_atachment.poll=VKPoll.Deserialize(poll);
			if(data.TryGetValue("doc",out doc))
				_atachment.doc=VKDocument.Deserialize(doc);
			if(data.TryGetValue("link",out link))
				_atachment.link=VKLink.Deserialize(link);
			if(data.TryGetValue("wall",out wall))
				_atachment.wall=VKWallPost.Deserialize(wall);
			if(data.TryGetValue("note",out note))
				_atachment.note=VKNote.Deserialize(note);
			if(data.TryGetValue("Page",out Page))
				_atachment.Page=VKPage.Deserialize(Page);
			return _atachment;
		}

        public string type { get; set; }

        public VKAudio audio { get; set; }
        public VKVideo video { get; set; }
        public VKPhoto photo { get; set; }
        public VKPoll poll { get; set; }
        public VKDocument doc { get; set; }
        public VKLink link { get; set; }
        public VKWallPost wall { get; set; }
        public VKNote note { get; set; }
        public VKPage Page { get; set; }
      
    }
}
