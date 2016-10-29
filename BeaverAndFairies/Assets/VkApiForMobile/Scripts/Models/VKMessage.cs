using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKMessage
    {
		public static VKMessage Deserialize(object message)
		{
			var data=(Dictionary<string,object>)message;
			var _message=new VKMessage();
			object id,user_id,date,read_state,@out,title,body,attachments,
			geo,fwd_messages,emoji,important,deleted,chat_id,chat_active,users_count,admin_id;

			if(data.TryGetValue("id",out id))
				_message.id=(long)id;

			if(data.TryGetValue("user_id",out user_id))
				_message.user_id=(long)user_id;
			if(data.TryGetValue("date",out date))
				_message.date=(long)date;

			if(data.TryGetValue("read_state",out read_state))
				_message.read_state=(int)(long)read_state;

			if(data.TryGetValue("out",out @out))
				_message.@out=(int)(long)@out;

			if(data.TryGetValue("title",out title))
				_message.title=(string)title;

			if(data.TryGetValue("body",out body))
				_message.body=(string) body;

			if(data.TryGetValue("attachments",out attachments))
			{
				var _attachments=new List<VKAttachment>();
				var att=(List<object>)attachments;
				foreach (var a in att) {
					_attachments.Add(VKAttachment.Deserialize(a));
				}
				_message.attachments=_attachments;
			}
				

			if(data.TryGetValue("geo",out geo))
				_message.geo=VKGeo.Deserialize(geo);

			if(data.TryGetValue("fwd_messages",out fwd_messages))
			{
				var _msg=new List<VKMessage>();
				var data1=(List<VKMessage>) fwd_messages;
				foreach(var d in data1)
				{
					_msg.Add(VKMessage.Deserialize(d));

				}
				_message.fwd_messages=_msg;
			}

			if(data.TryGetValue("emoji",out emoji))
				_message.emoji=(int)(long)emoji;

			if(data.TryGetValue("important",out important))
				_message.important=(int)(long)important;

			if(data.TryGetValue("deleted",out deleted))
				_message.deleted=(int)(long)deleted;

			if(data.TryGetValue("chat_id",out chat_id))
				_message.chat_id=(long)chat_id;

			if (data.TryGetValue ("chat_active", out chat_active)) {

				_message.chat_active = new List<long>();
				foreach (var i in (List<object>)chat_active) {
					_message.chat_active.Add((long)i);
				}
			}

			if(data.TryGetValue("users_count",out users_count))
				_message.users_count=(int)(long)users_count;

			if(data.TryGetValue("admin_id",out admin_id))
				_message.admin_id=(long)admin_id;

			object push_settings,action,action_mid,action_email,action_text,photo_50,photo_100,photo_200;

			if(data.TryGetValue("push_settings",out push_settings))
				_message.push_settings=VKPushSettings.Deserialize(push_settings);

			if(data.TryGetValue("action",out action))
				_message.action=(string)action;

			if(data.TryGetValue("action_mid",out action_mid))
				_message.action_mid=(long)action_mid;

			if(data.TryGetValue("action_email",out action_email))
				_message.action_email=(string)action_email;

			if(data.TryGetValue("action_text",out action_text))
				_message.action_text=(string)action_text;

			if(data.TryGetValue("photo_50",out photo_50))
				_message.photo_50=(string)photo_50;

			if(data.TryGetValue("photo_100",out photo_100))
				_message.photo_100=(string)photo_100;

			if(data.TryGetValue("photo_200",out photo_200))
				_message.photo_200=(string)photo_200;

			return _message;
		}
        public long id { get; set; }

        public long user_id { get; set; }

        public long date { get; set;}

        public int read_state { get; set; }

        public int @out { get; set; }

       
		public string title { get; set; }
        
		public string body { get; set; }
        
        public List<VKAttachment> attachments { get; set; }
        public VKGeo geo { get; set; }
        public List<VKMessage> fwd_messages { get; set; }
    
        public int emoji { get; set; }
        public int important { get; set; }

        public int deleted { get; set; }
        
        public long chat_id { get; set; }

        public List<long> chat_active { get; set; }

        public int users_count { get; set; }
        public long admin_id { get; set; }


        public VKPushSettings push_settings
        {
            get;
            set;
        }
       
        private string _action = "";
        public string action
        {
            get { return _action; }

            set
            {
                _action = (value ?? "");
            }
        }

        public long action_mid
        {
            get;
            set;
        }

       
		public string action_email { get; set; }
        
		public string action_text { get; set; }
       

        public string photo_50 { get; set; }

        public string photo_100 { get; set; }

        public string photo_200 { get; set; }
    }

    public partial class VKPushSettings
    {
		public static VKPushSettings Deserialize(object settings)
		{
			var data=(Dictionary<string,object>)settings;
			var _settings=new VKPushSettings();
			object disabled_until,sound;

			if(data.TryGetValue("disabled_until",out disabled_until))
				_settings.disabled_until=(int)(long)disabled_until;
			if(data.TryGetValue("sound",out sound))
				_settings.sound=(int)(long)sound;

			return _settings;
		}
        public int disabled_until { get; set; }

        public int sound { get; set; }      
    }
}
