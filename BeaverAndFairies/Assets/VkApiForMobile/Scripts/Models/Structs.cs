using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin.MiniJSON;


namespace com.playGenesis.VkUnityPlugin {
	public class ShareImage
	{
		public string imageName;
		public byte[] data;
		public ImageType imagetype;

	}
	public class FileForUpload
	{
		public string filename;
		public byte[] data;
		public string mimeType;
	}

	public sealed class ImageType {
		
		private readonly String name;
#pragma warning disable 0414
		private readonly int value;
#pragma warning restore 0414
		
		public static readonly ImageType Jpeg = new ImageType (1, "image/jpeg");
		public static readonly ImageType Png = new ImageType (2, "image/png");

		
		private ImageType(int value, String name){
			this.name = name;
			this.value = value;
		}
		
		public override String ToString(){
			return name;
		}
		public static explicit operator String(ImageType i)  // explicit byte to digit conversion operator
		{
			return i.name;
		}
		
	}

	[System.Serializable]
	public class Error:EventArgs
	{
		public string error_code;
		public string error_msg;
		public string fullJson;

		public static Error Deserialize(string json)
		{
			var dict=Json.Deserialize(json) as Dictionary<string,object>;
			object error;
			if (dict.TryGetValue ("error", out error)) {
				var errordict=(Dictionary<string,object>)error;
				var _error= new Error();
				object error_code,error_msg;
				if(errordict.TryGetValue("error_code",out error_code))
					_error.error_code=((long)error_code).ToString();
				if(errordict.TryGetValue("error_msg",out error_msg))
					_error.error_msg=(string)error_msg;
				return _error; 
			}
			return null;
		}
		public static Error ParseSerializedFromFromNativeSdk(string errormessage){
			string[] error=errormessage.Split('#');
			Error ei=new Error();
			ei.error_code = error [0];
			ei.error_msg = error [1];
			return ei;
		}
		public static Error ParseVkError(string resp)
		{
			if(string.IsNullOrEmpty(resp))
			{
				Error ei=new Error();
				ei.error_code="404";
				ei.error_msg="No connection";
				//ei.errorType=ErrorType.VKError;
				return ei;
				
			}
			//ErrorContainer e = JsonConvert.DeserializeObject<ErrorContainer>(resp);//js jr.Read<ErrorContainer> (resp);
			Error e =Error.Deserialize(resp);//js jr.Read<ErrorContainer> (resp);
			
			if (e!=null && !String.IsNullOrEmpty(e.error_code))  
			{
				//Debug.LogError(e.error_code+" "+e.error_msg);
				Error ei=new Error();
				ei.error_code=e.error_code;
				ei.error_msg=e.error_msg;
				//ei.errorType=ErrorType.VKError;
				return ei;
			}
			return null;
		}

	}
	public struct VKCaptcha
	{
		public string id;
		public string url;
		public string key;

	} 
	public enum VkApiMethods
	{
		users_get,
		users_search,
		users_isAppUser,
		users_getSubscriptions,
		users_getFollowers,
		users_report,
		
		groups_isMember,
		groups_getById,
		groups_get,
		groups_getMembers,
		groups_join,
		groups_leave,
		groups_search,
		groups_getInvites,
		groups_banUser,
		groups_unbanUser,
		groups_getBanned,
		
		friends_get,
		friends_getOnline,
		friends_getMutual,
		friends_getRecent,
		friends_getRequests,
		friends_add,
		friends_edit,
		friends_delete,
		friends_getLists,
		friends_addList,
		friends_editList,
		friends_deleteList,
		friends_getAppUsers,
		friends_getByPhones,
		friends_deleteAllRequests,
		friends_getSuggestions,
		friends_areFriends,
		
		wall_get,
		wall_search,
		wall_getById,
		wall_post,
		wall_repost,
		wall_getReposts,
		wall_edit,
		wall_delete,
		wall_restore,
		wall_getComments,
		wall_addComment,
		wall_editComment,
		wall_deleteComment,
		wall_restoreComment,
		wall_reportPost,
		wall_reportComment,
		
		photos_createAlbum,
		photos_editAlbum,
		photos_getAlbums,
		photos_get,
		photos_getAlbumsCount,
		photos_getProfile,
		photos_getById,
		photos_getUploadServer,
		photos_getOwnerPhotoUploadServer,
		photos_getChatUploadServer,
		photos_saveOwnerPhoto,
		photos_saveWallPhot,
		photos_getWallUploadServer,
		photos_getMessagesUploadServer,
		photos_saveMessagesPhoto,
		photos_report,
		photos_reportComment,
		photos_search,
		photos_save,
		photos_copy,
		photos_edit,
		photos_move,
		photos_makeCover,
		photos_reorderAlbums,
		photos_reorderPhotos,
		photos_getAll,
		photos_getUserPhotos,
		photos_deleteAlbum,
		photos_delete,
		photos_confirmTag,
		photos_getComments,
		photos_getAllComments,
		photos_createComment,
		photos_deleteComment,
		photos_restoreComment,
		photos_editComment,
		photos_getTags,
		photos_putTag,
		photos_removeTag,
		photos_getNewTags,
		
		video_get, 
		video_edit,
		video_add,
		video_save,
		video_delete,
		video_restore,
		video_search,
		video_getUserVideos,
		video_reportComment,
		video_getAlbums,  
		video_addAlbum,  
		video_editAlbum,  
		video_deleteAlbum,  
		video_moveToAlbum,  
		video_getComments,  
		video_createComment,  
		video_deleteComment,  
		video_restoreComment,  
		video_editComment,  
		video_getTags,  
		video_putTag,  
		video_removeTag,  
		video_getNewTags,  
		video_report,
		
		audio_get,
		audio_getById,
		audio_getLyrics,
		audio_search,
		audio_getUploadServer,
		audio_save,
		audio_add,
		audio_delete,
		audio_edit,
		audio_reorder,
		audio_restore,
		audio_getAlbums,
		audio_addAlbum,
		audio_editAlbum,
		audio_deleteAlbum,
		audio_moveToAlbum,
		audio_setBroadcast,
		audio_getBroadcastList,
		audio_getRecommendations,
		audio_getPopular,
		audio_getCount,
		
		messages_get,
		messages_getDialogs,
		messages_getById,
		messages_search,
		messages_getHistory,
		messages_send,
		messages_delete,
		messages_deleteDialog,
		messages_restore,
		messages_markAsRead,
		messages_markAsImportant,
		messages_getLongPollServer,
		messages_getLongPollHistory,
		messages_getChat,
		messages_createChat,
		messages_editChat,
		messages_getChatUsers,
		messages_setActivity,
		messages_searchDialogs,
		messages_addChatUser,
		messages_removeChatUser,
		messages_getLastActivity,
		messages_setChatPhoto,
		messages_deleteChatPhoto,
		
		newsfeed_get,
		newsfeed_getRecommended,
		newsfeed_getComments,
		newsfeed_getMentions,  
		newsfeed_getBanned,  
		newsfeed_addBan,  
		newsfeed_deleteBan,
		newsfeed_ignoreItem,
		newsfeed_unignoreItem,
		newsfeed_search, 
		newsfeed_getLists,
		newsfeed_saveList,  
		newsfeed_deleteList,
		newsfeed_unsubscribe,
		newsfeed_getSuggestedSources,
		
		
		likes_getList,
		likes_add,
		likes_delete,
		likes_isLiked,
		
		storage_get,
		storage_set,
		storage_getKeys,
		
		account_getCounters,
		account_setNameInMenu,
		account_setOnline,
		account_setOffline,
		account_lookupContacts,
		account_registerDevice,
		account_unregisterDevice,
		account_setSilenceMode,
		account_getPushSettings,
		account_getAppPermission,
		saccount_getActiveOffers,
		account_banUser,
		account_unbanUser,
		account_getBanned,
		account_getInfo,
		account_setInfo,
		account_changePassword,
		account_getProfileInfo,
		account_saveProfileInfo,
		
		auth_checkPhone,
		auth_signup,
		auth_confirm,
		auth_restore,
		auth_externalSignUp,
		auth_externalCheckAuth,
		
		widgets_getComments,
		widgets_getPages,
		
		status_get,
		status_set,
		
		pages_get,
		pages_save,
		pages_saveAccess,
		pages_getHistory,
		pages_getTitles,
		pages_getVersion,
		pages_parseWiki,
		pages_clearCache,
		
		board_getTopics, 		
		board_getComments, 		
		board_addTopic, 		
		board_addComment, 		
		board_deleteTopic, 		
		board_editTopic, 		
		board_editComment, 		
		board_restoreComment, 		
		board_deleteComment, 		
		board_openTopic, 		
		board_closeTopic, 		
		board_fixTopic, 		
		board_unfixTopic,

		notes_get, 		
		notes_getById, 		
		notes_getFriendsNotes, 		
		notes_add, 		
		notes_edit, 		
		notes_delete, 		
		notes_getComments, 		
		notes_createComment, 		
		notes_editComment, 		
		notes_deleteComment, 		
		notes_restoreComment,

		places_add, 		
		places_getById, 		
		places_search,
		places_checkin, 	
		places_getCheckins, 	
		places_getTypes,

		polls_getById, 		
		polls_addVote, 	
		polls_deleteVote, 	
		polls_getVoters, 	
		polls_create, 		
		polls_edit,

		docs_get, 
		docs_getById, 
		docs_getUploadServer,
		docs_getWallUploadServer, 	
		docs_save, 
		docs_delete, 	
		docs_add,
		
		fave_getUsers,
		fave_getPhotos, 	
		fave_getPosts, 
		fave_getVideos, 	
		fave_getLinks,
		
		notifications_get,
		notifications_markAsViewed,
		
		stats_get,
		
		search_getHints,
		
		apps_getCatalog,
		apps_sendRequest,
		
		utils_checkLink,
		utils_resolveScreenName,
		utils_getServerTime,
		database_getCountries, 	
		database_getRegions,
		database_getStreetsById,
		database_getCountriesById,
		database_getCities, 
		database_getCitiesById, 		
		database_getUniversities,
		database_getSchools,
		database_getSchoolClasses, 	
		database_getFaculties, 	
		database_getChairs,
		
		execute,

		
	}

}