using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using com.playGenesis.VkUnityPlugin.MiniJSON;
namespace com.playGenesis.VkUnityPlugin
{
    /// <summary>
    /// https://vk.com/dev/fields
    /// </summary>
    public partial class VKUser
    {
        public static List<VKUser> Deserialize(object[] Users)
        {
            List<VKUser> result=new List<VKUser>();
            foreach (var user in Users)
            {
                result.Add(VKUser.Deserialize(user));
            }
            return result;
        }

        public static VKUser Deserialize(object User)
		{
			var userDeserialized=new VKUser();
			var usr=(Dictionary<string,object>)User;

			object id,firstName,last_name,hidden,deactivated,verified,blacklisted,sex,bdate,city,country,home_town;
			object photo_50,photo_100,photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig;
			object online,lists,domain,has_mobile,mobile_phone,home_phone,site;

			if(usr.TryGetValue("id", out id))
				userDeserialized.id=(long)id;

			if(usr.TryGetValue("first_name", out firstName))
				userDeserialized.first_name=(string)firstName;

			if(usr.TryGetValue("last_name", out last_name))
				userDeserialized.last_name=(string)last_name;

			if(usr.TryGetValue("deactivated", out deactivated))
				userDeserialized.deactivated=(string)deactivated;

			if(usr.TryGetValue("hidden", out hidden))
				userDeserialized.hidden=(int)(long)hidden;

			if(usr.TryGetValue("verified", out verified))
				userDeserialized.verified=(int)(long)verified;

			if(usr.TryGetValue("blacklisted", out blacklisted))
				userDeserialized.blacklisted=(int)(long)blacklisted;

			if(usr.TryGetValue("sex", out sex))
				userDeserialized.sex=(int)(long)sex;

			if(usr.TryGetValue("bdate", out bdate))
				userDeserialized.bdate=(string)bdate;

			if(usr.TryGetValue("city", out city))
				userDeserialized.city=(string)city;

			if(usr.TryGetValue("country", out country))
				userDeserialized.country=(string)country;

			if(usr.TryGetValue("home_town", out home_town))
				userDeserialized.home_town=(string)home_town;

			if(usr.TryGetValue("home_phone", out home_phone))
				userDeserialized.home_phone=(string)home_phone;

			if(usr.TryGetValue("photo_50", out photo_50))
				userDeserialized.photo_50=(string)photo_50;

			if(usr.TryGetValue("photo_100", out photo_100))
				userDeserialized.photo_100=(string)photo_100;

			if(usr.TryGetValue("photo_200", out photo_200))
				userDeserialized.photo_200=(string)photo_200;

			if(usr.TryGetValue("photo_200_orig", out photo_200_orig))
				userDeserialized.photo_200_orig=(string)photo_200_orig;

			if(usr.TryGetValue("photo_400_orig", out photo_400_orig))
				userDeserialized.photo_400_orig=(string)photo_400_orig;

			if(usr.TryGetValue("photo_max", out photo_max))
				userDeserialized.photo_max=(string)photo_max;

			if(usr.TryGetValue("photo_max_orig", out photo_max_orig))
				userDeserialized.photo_max_orig=(string)photo_max_orig;

			if(usr.TryGetValue("online", out online))
				userDeserialized.online=int.Parse(online.ToString());

			if (usr.TryGetValue ("lists", out lists)) {
				userDeserialized.lists =new List<long>();
				foreach (var i in (List<object>)lists) {
					userDeserialized.lists.Add((long)i);
				}
			}

			if(usr.TryGetValue("domain", out domain))
				userDeserialized.domain=(string)domain;

			if(usr.TryGetValue("has_mobile", out has_mobile))
				userDeserialized.has_mobile=(int)(long)has_mobile;

			if(usr.TryGetValue("mobile_phone", out mobile_phone))
				userDeserialized.mobile_phone=(string)mobile_phone;

			if(usr.TryGetValue("home_phone", out home_phone))
				userDeserialized.home_phone=(string)home_phone;

			if(usr.TryGetValue("site", out site))
				userDeserialized.site=(string)site;

			object university,university_name,faculty,faculty_name,graduation;

			if(usr.TryGetValue("university", out university))
				userDeserialized.university=(long)university;

			if(usr.TryGetValue("university_name", out university_name))
				userDeserialized.university_name=(string)university_name;

			if(usr.TryGetValue("faculty", out faculty))
				userDeserialized.faculty=(long)faculty;

			if(usr.TryGetValue("faculty_name", out faculty_name))
				userDeserialized.faculty_name=(string)faculty_name;

			if(usr.TryGetValue("graduation", out graduation))
				userDeserialized.graduation=(int)(long)graduation;

			object universities,schools,status,status_audio,followers_count,common_count,counters,occupation;

			if(usr.TryGetValue("universities", out universities))
			{
				var _universities=new List<VKUniversity>();
				var unidata=(List<object>)universities;
				foreach(var u in unidata)
				{
					_universities.Add(VKUniversity.Deserialize(u));
				}
					
				userDeserialized.universities=_universities;
			}

			if(usr.TryGetValue("schools", out schools))	{
				var _schools=new List<VKSchool>();
				var unidata=(List<object>)schools;
				foreach(var s in unidata)
				{
					_schools.Add(VKSchool.Deserialize(s));
				}
				
				userDeserialized.schools=_schools;
			}

			if(usr.TryGetValue("status", out status))	
				userDeserialized.status=(string)status;
				
			if(usr.TryGetValue("status_audio", out status_audio))	
				userDeserialized.status_audio=VKAudio.Deserialize(status_audio);

			if(usr.TryGetValue("followers_count", out followers_count))	
				userDeserialized.followers_count=(int)(long)followers_count;

			if(usr.TryGetValue("common_count", out common_count))	
				userDeserialized.common_count=(int)(long)common_count;

			
			if(usr.TryGetValue("counters", out counters))	
				userDeserialized.counters=VKCounters.Deserialize(counters);

			if(usr.TryGetValue("occupation", out occupation))	
				userDeserialized.occupation=VKUserOccupation.Deserialize(occupation);

			object nickname,relatives,relation,personal,facebook,twitter,livejournal,instagram,exports,wall_comments;

			if(usr.TryGetValue("nickname", out nickname))	
				userDeserialized.nickname=(string)nickname;

			if(usr.TryGetValue("relatives", out relatives))
			{
				var rel=(List<object>)relatives;
				var _relatives=new List<VKUserRelative>();
				foreach (var r in rel) 
				{
					_relatives.Add(VKUserRelative.Deserialize(r));	
				}
				userDeserialized.relatives=_relatives;
			}

			if(usr.TryGetValue("relation", out relation))
				userDeserialized.relation=(int)(long)relation;

			if(usr.TryGetValue("personal", out personal))
				userDeserialized.personal=VKUserPersonal.Deserialize(personal);	

			if(usr.TryGetValue("facebook", out facebook))
				userDeserialized.facebook=(string)facebook;

			if(usr.TryGetValue("twitter", out twitter))
				userDeserialized.twitter=(string)twitter;

			if(usr.TryGetValue("livejournal", out livejournal))
				userDeserialized.livejournal=(string)livejournal;

			if(usr.TryGetValue("instagram", out instagram))
				userDeserialized.instagram=(string)instagram;

			if(usr.TryGetValue("exports", out exports))
				userDeserialized.exports=VKUserExports.Deserialize(exports);

			if(usr.TryGetValue("wall_comments", out wall_comments))
				userDeserialized.wall_comments=(int)(long)wall_comments;

			object activities,interests,movies,tv,books,games,about,
			quotes,can_post,can_see_all_posts,can_see_audio,can_write_private_message,
			timezone,screen_name,maiden_name;

			if(usr.TryGetValue("activities", out activities))
				userDeserialized.activities=(string)activities;

			
			if(usr.TryGetValue("interests", out interests))
				userDeserialized.interests=(string)interests;
			
			if(usr.TryGetValue("movies", out movies))
				userDeserialized.movies=(string)movies;

			if(usr.TryGetValue("tv", out tv))
				userDeserialized.tv=(string)tv;

			if(usr.TryGetValue("books", out books))
				userDeserialized.books=(string)books;

			if(usr.TryGetValue("games", out games))
				userDeserialized.games=(string)games;

			if(usr.TryGetValue("about", out about))
				userDeserialized.about=(string)about;

			if(usr.TryGetValue("quotes", out quotes))
				userDeserialized.quotes=(string)quotes;

			if(usr.TryGetValue("can_post", out can_post))
				userDeserialized.can_post=(int)(long)can_post;

			if(usr.TryGetValue("can_see_all_posts", out can_see_all_posts))
				userDeserialized.can_see_all_posts=(int)(long)can_see_all_posts;

			if(usr.TryGetValue("can_see_audio", out can_see_audio))
				userDeserialized.can_see_audio=(int)(long)can_see_audio;

			if(usr.TryGetValue("can_write_private_message", out can_write_private_message))
				userDeserialized.can_write_private_message=(int)(long)can_write_private_message;
			
			if(usr.TryGetValue("timezone", out timezone))
				userDeserialized.timezone=(int)(long)timezone;

			if(usr.TryGetValue("screen_name", out screen_name))
				userDeserialized.screen_name=(string)screen_name;

			if(usr.TryGetValue("maiden_name", out maiden_name))
				userDeserialized.maiden_name=(string)maiden_name;

			return userDeserialized;
		}
		public long id
        {
            get;
            set;
        }
		public string first_name  { get; set; }
        

        
		public string last_name  { get; set; }
        

        public string deactivated
        {
            get;
            set;
        }

        public int hidden
        {
            get;
            set;
        }

        public int verified
        {
            get;
            set;
        }

        public int blacklisted
        {
            get;
            set;
        }

        public int sex
        {
            get;
            set;
        }

        public string bdate { get; set; }

        public string city { get; set; }

        public string country { get; set; }

        public string home_town { get; set; }

        public string photo_50 { get; set; }

        public string photo_100 { get; set; }

        public string photo_200_orig { get; set; }

        public string photo_200 { get; set; }

        public string photo_400_orig { get; set; }

        public string photo_max { get; set; }

        public string photo_max_orig { get; set; }

        public int online { get; set; }

        public List<long> lists { get; set; }

        public string domain { get; set; }

        public int has_mobile { get; set; }

        public string mobile_phone { get; set; }

        public string home_phone { get; set; }

        public string site { get; set; }

        
	
        public long university { get; set; }

        public string university_name { get; set; }

        public long faculty { get; set; }

        public string faculty_name { get; set; }

        public int graduation { get; set; }


        public List<VKUniversity> universities { get; set; }

        public List<VKSchool> schools { get; set; }
        
        public string status { get; set; }

        public VKAudio status_audio { get; set; }

        public int followers_count { get; set; }

        public int common_count { get; set; }

        public VKCounters counters { get; set; }

        public VKUserOccupation occupation
        {
            get;
            set;
        }



        public string nickname { get; set; }

        public List<VKUserRelative> relatives { get; set; }

        public int relation { get; set; }

        public VKUserPersonal personal { get; set; }

        public string facebook { get; set; }

        public string twitter { get; set; }

        public string livejournal { get; set; }

        public string instagram { get; set; }


        public VKUserExports exports { get; set;}

        public int wall_comments { get; set; }


		public string activities { get; set; }
        
		public string interests  { get; set; }
        
		public string movies  { get; set; }
        
		public string tv  { get; set; }
       
		public string books  { get; set; }
        
		public string games  { get; set; }
        
		public string about  { get; set; }
      
		public string quotes { get; set; }
       
		public int can_post { get; set; }

        public int can_see_all_posts { get; set; }

        public int can_see_audio { get; set; }

        public int can_write_private_message { get; set; }

        public int timezone { get; set; }

        public string screen_name { get; set; }

        public string maiden_name { get; set; }

    }


    public partial class VKUserExports
    {
		public static VKUserExports Deserialize(object UserExports)
		{
			var UserExportsDeserialized=new VKUserExports();
			var usrExports=(Dictionary<string,object>)UserExports;
			
			object twitter;
			object facebook;
			object livejournal;
			object instagram;

			if(usrExports.TryGetValue("twitter", out twitter))
				UserExportsDeserialized.twitter=(int)(long)twitter;

			if(usrExports.TryGetValue("facebook", out facebook))
				UserExportsDeserialized.facebook=(int)(long)facebook;

			if(usrExports.TryGetValue("livejournal", out livejournal))
				UserExportsDeserialized.livejournal=(int)(long)livejournal;

			if(usrExports.TryGetValue("instagram", out instagram))
				UserExportsDeserialized.instagram=(int)(long)instagram;


			return UserExportsDeserialized;
		}


        public int twitter { get; set; }

        public int facebook { get; set; }

        public int livejournal { get; set; }

        public int instagram { get; set; }
    }

    public partial class VKUserPersonal
    {
		public static VKUserPersonal Deserialize(object UserPersonal)
		{
			var UserPersonalDeserialized=new VKUserPersonal();
			var usrPersonal=(Dictionary<string,object>)UserPersonal;
			
			object political,langs,religion,inspired_by,people_main,life_main,smoking,alcohol;
		
			if(usrPersonal.TryGetValue("political", out political))
				UserPersonalDeserialized.political=(int)(long)political;
			
			if (usrPersonal.TryGetValue ("langs", out langs)) {
				UserPersonalDeserialized.langs = new List<string>();
				foreach (var i in (List<object>)langs) {
					UserPersonalDeserialized.langs.Add((string)i);
				}
			}
			
			if(usrPersonal.TryGetValue("religion", out religion))
				UserPersonalDeserialized.religion=(string)religion;
			
			if(usrPersonal.TryGetValue("inspired_by", out inspired_by))
				UserPersonalDeserialized.inspired_by=(string)inspired_by;

			if(usrPersonal.TryGetValue("people_main", out people_main))
				UserPersonalDeserialized.people_main=(int)(long)people_main;

			if(usrPersonal.TryGetValue("life_main", out life_main))
				UserPersonalDeserialized.life_main=(int)(long)life_main;

			if(usrPersonal.TryGetValue("smoking", out smoking))
				UserPersonalDeserialized.smoking=(int)(long)smoking;

			if(usrPersonal.TryGetValue("alcohol", out alcohol))
				UserPersonalDeserialized.alcohol=(int)(long)alcohol;

			return UserPersonalDeserialized;
		}
        public int political { get; set; }

        public List<string> langs { get; set; }

        public string religion
        {
            get;
            set;
        }


      
		public string inspired_by  { get; set; }
        

        public int people_main
        {
            get;
            set;
        }

        public int life_main
        {
            get;
            set;
        }

        public int smoking
        {
            get;
            set;
        }

        public int alcohol
        {
            get;
            set;
        }
    }

    public partial class VKUserRelative
    {       
		public static VKUserRelative Deserialize(object UserRelative)
		{
			var u_rel_deser=new VKUserRelative();
			var data=(Dictionary<string,object>)UserRelative;

			object id,name,type;

			if(data.TryGetValue("id", out id))
				u_rel_deser.id=(int)(long)id;

			if(data.TryGetValue("name", out name))
				u_rel_deser.name=(string)name;

			if(data.TryGetValue("type", out type))
				u_rel_deser.name=(string)type;

			return u_rel_deser;

		}
        public long id { get; set; }

        public string name {get;set;}

        public string type { get; set; }
    }

    public partial class VKUserOccupation
    {
		public static VKUserOccupation Deserialize(object UserOccupation)
		{
			var u_occ_des=new VKUserOccupation();
			var data = (Dictionary<string,object>)UserOccupation;

			object type, id, name;

			if(data.TryGetValue("type",out type))
			   u_occ_des.type=(string)type;

			if(data.TryGetValue("id", out id))
			   u_occ_des.id=(long)id;

			if(data.TryGetValue("name", out name))
				u_occ_des.name=(string)name;

			return u_occ_des;

		}
        public static readonly string OCCUPATION_TYPE_WORK = "work";
        public static readonly string OCCUPATION_TYPE_SCHOOL = "school";
        public static readonly string OCCUPATION_TYPE_UNIVERSITY = "university";

        public string type { get; set; }

        public long id { get; set; }

        public string name { get; set; }
    }

    public partial class VKCounters
    {
		public static VKCounters Deserialize(object Countries)
		{
			var counrties_des=new VKCounters();
			var data=(Dictionary<string,object>)Countries;

			object albums,videos,audios,notes,groups,photos,friends,online_friends,mutual_friends,
				   user_videos,user_photos,followers,subscriptions,topics,docs,pages;
				
			if(data.TryGetValue("albums",out albums))
				counrties_des.albums=(int)(long)albums;
			if(data.TryGetValue("videos",out videos))
				counrties_des.videos=(int)(long)videos;
			if(data.TryGetValue("audios",out audios))
				counrties_des.audios=(int)(long)audios;
			if(data.TryGetValue("notes",out notes))
				counrties_des.notes=(int)(long)notes;
			if(data.TryGetValue("groups",out groups))
				counrties_des.groups=(int)(long)groups;
			if(data.TryGetValue("photos",out photos))
				counrties_des.photos=(int)(long)photos;
			if(data.TryGetValue("friends",out friends))
				counrties_des.friends=(int)(long)friends;
			if(data.TryGetValue("online_friends",out online_friends))
				counrties_des.online_friends=(int)(long)online_friends;
			if(data.TryGetValue("mutual_friends",out mutual_friends))
				counrties_des.mutual_friends=(int)(long)mutual_friends;
			if(data.TryGetValue("user_videos",out user_videos))
				counrties_des.user_videos=(int)(long)user_videos;
			if(data.TryGetValue("user_photos",out user_photos))
				counrties_des.user_photos=(int)(long)user_photos;
			if(data.TryGetValue("followers",out followers))
				counrties_des.followers=(int)(long)followers;
			if(data.TryGetValue("subscriptions",out subscriptions))
				counrties_des.subscriptions=(int)(long) subscriptions;
			if(data.TryGetValue("topics",out topics))
				counrties_des.topics=(int)(long)topics;
			if(data.TryGetValue("docs",out docs))
				counrties_des.docs=(int)(long)docs;
			if(data.TryGetValue("pages",out pages))
				counrties_des.pages=(int)(long)pages;

			return counrties_des;
		}

        public int albums { get; set; }

        public int videos { get; set; }

        public int audios { get; set; }

        public int notes { get; set; }
        public int groups { get; set; }

        public int photos { get; set; }
        public int friends { get; set; }
        public int online_friends { get; set; }
        public int mutual_friends { get; set; }
        public int user_videos { get; set; }
        public int user_photos { get; set; }
        public int followers { get; set; }
        public int subscriptions { get; set; }

        public int topics { get; set; }
        public int docs { get; set; }

        public int pages { get; set; }

    }


    public partial class VKUserStatus
    {
		public static VKUserStatus Deserialize(object UserStatus)
		{
			object time,platform;

			var stat_ders=new VKUserStatus();
			var data=(Dictionary<string,object>)UserStatus;

			if(data.TryGetValue("time",out time))
				stat_ders.time=(long)time;
			if(data.TryGetValue("platform",out platform))
				stat_ders.platform=(int)(long)platform;

			return stat_ders;
		}
        public long time { get; set; }
 
        public int platform { get; set; }
    }

    public partial class VKUniversity
    {
		public static VKUniversity Deserialize(object University)
		{
			object id,country,city,name,faculty,faculty_name,chair,chair_name,graduation;
			var uni_des=new VKUniversity();
			var  data=(Dictionary<string,object>)University;

			if(data.TryGetValue("id",out id))
				uni_des.id=(long)id;
			if(data.TryGetValue("country",out country))
				uni_des.country=(long)country;
			if(data.TryGetValue("city",out city))
				uni_des.city=(long)city;
			if(data.TryGetValue("name",out name))
				uni_des.name=(string)name;
			if(data.TryGetValue("faculty",out faculty))
				uni_des.faculty=(long)faculty;
			if(data.TryGetValue("faculty_name",out faculty_name))
				uni_des.faculty_name=(string)faculty_name;
			if(data.TryGetValue("chair",out chair))
				uni_des.chair=(long)chair;
			if(data.TryGetValue("chair_name",out chair_name))
				uni_des.chair_name=(string)chair_name;
			if(data.TryGetValue("graduation",out graduation))
				uni_des.graduation=(int)(long)graduation;

			return uni_des;
		}
        public long id { get; set; }

        public long country { get; set; }

        public long city { get; set; }

        public string name { get; set; }

        public long faculty { get; set; }

        public string faculty_name { get; set; }

        public long chair { get; set; }

        public string chair_name { get; set; }

        public int graduation { get; set; }
    
    }

    public partial class VKSchool
    {
		public static VKSchool Deserialize(object School)
		{
			object id, country, city,name,year_from,year_to,year_graduated,@class,speciality,type,type_str;

			var school_des=new VKSchool();
			var data=(Dictionary<string,object>)School;

			if(data.TryGetValue("id",out id))
			   school_des.id=(long)id;
			if(data.TryGetValue("country",out country))
				school_des.country=(long)country;
			if(data.TryGetValue("city",out city))
				school_des.city=(long)city;
			if(data.TryGetValue("name",out name))
				school_des.name=(string)name;
			if(data.TryGetValue("year_from",out year_from))
				school_des.year_from=(int)(long)year_from;
			if(data.TryGetValue("year_to",out year_to))
				school_des.year_to=(int)(long)year_to;
			if(data.TryGetValue("year_graduated",out year_graduated))
				school_des.year_graduated=(int)(long)year_graduated;
			if(data.TryGetValue("class",out @class))
				school_des.@class=(string)@class;
			if(data.TryGetValue("speciality",out speciality))
				school_des.speciality=(string)speciality;
			if(data.TryGetValue("type",out type))
				school_des.type=(long)type;
			if(data.TryGetValue("type_str",out type_str))
				school_des.type_str=(string)type_str;

			return school_des;
		}

		public long id { get; set; }

        public long country { get; set; }

        public long city { get; set; }

        public string name { get; set; }

        public int year_from { get; set; }

        public int year_to { get; set; }

        public int year_graduated { get; set; }

        public string @class {get;set;}

        public string speciality { get; set; }

        public long type { get; set; }

        public string type_str { get; set; }


    }
}
