using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    /// <summary>
    /// https://vk.com/dev/post
    /// </summary>
    public partial class VKWallPost
    {
		public static VKWallPost Deserialize(object WallPost)
		{
			var _post=new VKWallPost();
			var data=(Dictionary<string,object>)WallPost;

			object id,owner_id,from_id,date,text,reply_owner_id,reply_post_id,friends_only;


			if(data.TryGetValue("id",out id))
				_post.id=(long)id;
			if(data.TryGetValue("owner_id",out owner_id))
				_post.owner_id=(long)owner_id;
			if(data.TryGetValue("from_id",out from_id))
				_post.from_id=(long)from_id;
			if(data.TryGetValue("date",out date))
				_post.date=(long)date;
			if(data.TryGetValue("text",out text))
				_post.text=(string)text;
			if(data.TryGetValue("reply_owner_id",out reply_owner_id))
				_post.reply_owner_id=(long)reply_owner_id;
			if(data.TryGetValue("reply_post_id",out reply_post_id))
				_post.reply_post_id=(long)reply_post_id;
			if(data.TryGetValue("friends_only",out friends_only))
				_post.friends_only=(int)(long)friends_only;

			object comments,likes,reposts,post_type,post_source,attachments,geo;

			if(data.TryGetValue("comments",out comments))
				_post.comments=VKComments.Deserialize(comments);
			if(data.TryGetValue("likes",out likes))
				_post.likes=VKLikes.Deserialize(likes);
			if(data.TryGetValue("reposts",out reposts))
				_post.reposts=VKReposts.Deserialize(reposts);
			if(data.TryGetValue("post_type",out post_type))
				_post.post_type=(string)post_type;
			if(data.TryGetValue("post_source",out post_source))
				_post.post_source=VKPostSource.Deserialize(post_source);
			if(data.TryGetValue("attachments",out attachments))
			{
				var _att=new List<VKAttachment>();
				foreach(var a in (List<object>)attachments)
				{
					_att.Add(VKAttachment.Deserialize(a));
				}
				_post.attachments=_att;
			}
			if(data.TryGetValue("geo",out geo))
				_post.geo=VKGeo.Deserialize(geo);

			object signer_id,copy_history,can_pin,is_pinned;
			if(data.TryGetValue("signer_id",out signer_id))
				_post.signer_id=(long)signer_id;
			if(data.TryGetValue("copy_history",out copy_history))
			{ 
				var h=new List<VKWallPost>();
				foreach(var h1 in (List<object>)copy_history)
				{
					h.Add(VKWallPost.Deserialize(h1));
				}
				_post.copy_history=h;
			}

			if(data.TryGetValue("can_pin",out can_pin))
				_post.can_pin=(int)(long)can_pin;
			if(data.TryGetValue("is_pinned",out is_pinned))
				_post.is_pinned=(int)(long)is_pinned;

			return _post;
		}
        public long id { get; set; }

        public long owner_id { get; set; }

        public long from_id { get; set; }

        public long date { get; set; }

        public string text { get; set; }

        public long reply_owner_id { get; set; }

        public long reply_post_id { get; set; }

        public int friends_only { get; set; }

        public VKComments comments { get; set; }

        public VKLikes likes { get; set; }

        public VKReposts reposts { get; set; }

        public string post_type { get; set; }

        public VKPostSource post_source { get; set; }

        public List<VKAttachment> attachments { get; set; }

        public VKGeo geo { get; set; }

        public long signer_id { get; set; }

        public List<VKWallPost> copy_history { get; set; }

        public int can_pin { get; set; }

        public int is_pinned { get; set; }
    }


    public partial class VKComments
    {
		public static VKComments Deserialize(object comments)
		{
			var _comments =new VKComments();
			var data=(Dictionary<string,object>)comments;
			object count,can_post;
			if(data.TryGetValue("count",out count))
				_comments.count=(int)(long)count;
			if(data.TryGetValue("can_post",out can_post))
				_comments.can_post=(int)(long)can_post;

			return _comments;
		}
        public int count { get; set; }
        public int can_post { get; set; }

    }

    public partial class VKLikes
    {
		public static VKLikes Deserialize(object likes)
		{
			var _likes =new VKLikes();
			var data=(Dictionary<string,object>)likes;
			object count,user_likes,can_like,can_publish;

			if(data.TryGetValue("count",out count))
				_likes.count=(int)(long)count;
			if(data.TryGetValue("can_publish",out can_publish))
				_likes.can_publish=(int)(long)can_publish;
			if(data.TryGetValue("user_likes",out user_likes))
				_likes.user_likes=(int)(long)user_likes;
			if(data.TryGetValue("can_like",out can_like))
				_likes.can_like=(int)(long)can_like;
			return _likes;

		}

        public int count { get; set; }
        public int user_likes { get; set; }
        public int can_like { get; set; }
        public int can_publish { get; set; }
    }

    public partial class VKReposts
    {
		public static VKReposts Deserialize(object reposts)
		{
			var _reposts =new VKReposts();
			var data=(Dictionary<string,object>)reposts;
			object count,user_reposted;
			if(data.TryGetValue("count",out count))
				_reposts.count=(int)(long)count;
			if(data.TryGetValue("user_reposted",out user_reposted))
				_reposts.user_reposted=(int)(long)user_reposted;
			
			return _reposts;
		}

        public int count { get; set; }
        public int user_reposted { get; set; }
    }

    public partial class VKPostSource
    {
		public static VKPostSource Deserialize(object in_data)
		{
			var result =new VKPostSource();
			var data=(Dictionary<string,object>)in_data;
			object platform,type,data1;

			if(data.TryGetValue("platform",out platform))
				result.platform=(string)platform;
			if(data.TryGetValue("type",out type))
				result.type=(string)type;
			if(data.TryGetValue("data",out data1))
				result.data=(string)data1;

			return result;
		}
        public string platform { get; set; }
        public string type { get; set; }
        public string data { get; set; }
    }
}
