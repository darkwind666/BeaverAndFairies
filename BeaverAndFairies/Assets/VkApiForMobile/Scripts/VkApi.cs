
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Runtime.InteropServices;



namespace com.playGenesis.VkUnityPlugin 
{

	public class VkApi:MonoBehaviour
	{
		public bool IsUserLoggedIn;
		public string VkRequestUrlBase="https://api.vk.com/method/";
		public static VkApi VkApiInstance;
		public static VKToken CurrentToken;
		public static VkSettings VkSetts;
		public static Downloader Downloader;
		public LoginLogoutBridge nativeBridge=new LoginLogoutBridge();
		private bool loginProccessSterted;
		#region Events
		public event EventHandler<Error> AccessDenied;
		public event EventHandler<VKToken> ReceivedNewToken;
		public event Action LoggedIn;
		public event Action LoggedOut;
		private event Action<VKRequest> GlobalErrorHandler;
		#endregion

		public void KillAllReqeusts()
		{
			StopAllCoroutines ();
		}
		public void Login()
		{
			if (!loginProccessSterted) {
				nativeBridge.Login ();
				StartCoroutine(LockLogin5Sec());
			}
			
		}

		private IEnumerator LockLogin5Sec()
		{
		    loginProccessSterted = true;
			yield return new WaitForSeconds (5.0f);
		    loginProccessSterted = false;
		}
		public void Logout()
		{
			nativeBridge.Logout ();
		}
		#region EventTriggers

		
		public void onReceiveNewToken(VKToken e)
		{
			CurrentToken.access_token=e.access_token;
			CurrentToken.expires_in = e.expires_in;
			CurrentToken.tokenRecievedTime = e.tokenRecievedTime;
			CurrentToken.user_id = e.user_id;
		    CurrentToken.Save();
			if(ReceivedNewToken!=null)
				ReceivedNewToken (this,e);
			onLoggedIn ();
			Debug.Log("New token is"+e.access_token);
		}
		public void onLoggedIn()
		{
			StartCoroutine (WaitAndGoOn ());

		}
		IEnumerator WaitAndGoOn(){
			while(string.IsNullOrEmpty(CurrentToken.access_token)){
				yield return null;
			}
			IsUserLoggedIn = true;
			if(LoggedIn!=null)
				LoggedIn();
		}
		public void onLoggedOut()
		{
			IsUserLoggedIn = false;
			if(LoggedOut!=null)
				LoggedOut();

			VKToken.ResetToken();

		}
		public void onAccessDenied (Error e)
		{
			if (AccessDenied != null) {
				AccessDenied (this, e);
			}
		}
		#endregion
        
		public void CheckEditorSetup()
		{
			if (string.IsNullOrEmpty (CurrentToken.access_token)|| 
			    !VKToken.IsValidToken(CurrentToken)) {
				VkSetts.ProcessAuthUrl();
			}
			if(!VKToken.IsValidToken(CurrentToken)){
				Debug.LogError("Token has expired, please relogin to vk in editor");
				Debug.Break();
			}
		}
		public void SubscribeToGlobalErrorEvent(Action<VKRequest> handler){
			if (GlobalErrorHandler != null) {
				var globalHandlers = GlobalErrorHandler.GetInvocationList ();
				//Remove all handler must be only one Global
				foreach (var d in globalHandlers) {
					GlobalErrorHandler -= (d as Action<VKRequest>); 

				}
			}
			GlobalErrorHandler += handler;
		}
		public void UnsubscribeFromGlobalErrorEvent(Action<VKRequest> handler){

			GlobalErrorHandler -= handler;
		}

		void InitToken ()
		{
		    CurrentToken = VKToken.LoadPersistent();
		}

		void Awake()
		{
			VkSetts=Resources.Load<VkSettings> ("VkSettings");
			InitToken ();
			DontDestroyOnLoad(transform.gameObject);
			if (VkApiInstance == null)
				VkApiInstance = this;
			if(Downloader==null)
			Downloader = GetComponent<Downloader> ();

#if UNITY_EDITOR
			CheckEditorSetup();
#endif


			if (VKToken.IsValidToken(CurrentToken))
			{
				IsUserLoggedIn = true;
			} 
			else 
			{
				IsUserLoggedIn = false;
			}

		}
#if UNITY_IOS && !UNITY_EDITOR
	//handling back to app button for ios
		public void OnApplicationFocus(bool focus){
			if (nativeBridge!=null) {
				nativeBridge.OnApplicationFocus(focus,gameObject);
			}
		}
#endif
		private WWW GenerateWWWForm(VKRequest httprequest)
		{
			VKRequest _request = httprequest;
			WWWForm form = new WWWForm();
			form.AddBinaryData("file", (byte[])_request.data[0], (string)_request.data[1], (string)_request.data[2]);
			return new WWW (System.Uri.EscapeUriString (_request.fullurl),form);
		}
		private WWW GenerateWWWForm(VKRequest httprequest,FileForUpload file)
		{
			VKRequest _request = httprequest;
			WWWForm form = new WWWForm();
			form.AddBinaryData("file", file.data, file.filename, file.mimeType);
			return new WWW (System.Uri.EscapeUriString (_request.fullurl),form);
		}
		
		private void HandleTokenExpired(VKRequest httprequest)
		{
				Debug.Log("Invalid token. Are you logged in?");
				VKRequest vkargs = httprequest;
				vkargs.response = "";
				
				vkargs.error=new Error{error_code="401",error_msg="invalid token" };

				if (GlobalErrorHandler != null){
					GlobalErrorHandler (vkargs);
				} else if(vkargs.CallBackFunction!=null) {
					vkargs.CallBackFunction(vkargs);
				}


		}
		private void HandleWWWError( WWW www,VKRequest httprequest){
			var vkerror=new Error()
			{
				error_code="404",
				error_msg=www.error
			};
			
			VKRequest vkargs = httprequest;
			vkargs.response = www.text;
			vkargs.error=vkerror;

			if (GlobalErrorHandler != null){
				GlobalErrorHandler (vkargs);
			} else if(vkargs.CallBackFunction!=null) {
				vkargs.CallBackFunction(vkargs);
			}
		}
		private void HandleVKError( WWW www,VKRequest httprequest){
			var vkerror=Error.ParseVkError(www.text);

			VKRequest vkargs = httprequest;
			vkargs.response = www.text;
			vkargs.error=vkerror;

			if (GlobalErrorHandler != null) {
				GlobalErrorHandler (vkargs);
			} else if(vkargs.CallBackFunction!=null) {
				vkargs.CallBackFunction(vkargs);
			}
			
		}
		private void HandleNoError( WWW www,VKRequest httprequest){
			
			VKRequest vkargs = httprequest;
			vkargs.response = www.text;
			if(httprequest.CallBackFunction!=null)
				httprequest.CallBackFunction (vkargs);
		}
		private void HandleResponse(WWW www,VKRequest httpRequest)
		{
			Error vkerror = Error.ParseVkError (www.text);
			if (!string.IsNullOrEmpty (www.error)) {
				HandleWWWError(www,httpRequest);
			}
			
			if (string.IsNullOrEmpty (www.error) && vkerror != null) {
				HandleVKError(www,httpRequest);
			}
			
			if (String.IsNullOrEmpty (www.error)&& vkerror==null) 
			{
				HandleNoError(www,httpRequest);
				
			} 
		}
		public void Call( VKRequest httprequest)
		{
			httprequest.error = null;
			StartCoroutine (_Call (httprequest));
		}
		private IEnumerator _Call( VKRequest httprequest)
		{
			httprequest.url=httprequest.url.Contains("?")?httprequest.url:httprequest.url+"?";
			if (string.IsNullOrEmpty (httprequest.fullurl)) {
				if(httprequest.url.StartsWith("http"))
				{
					httprequest.fullurl=httprequest.url;
				}else{
					httprequest.fullurl = Utilities.GenerateFullHttpReqString (httprequest.url);
				}
			}

			if (VKToken.IsValidToken(CurrentToken)) 
			{
				WWW www = new WWW (httprequest.fullurl);
				yield return www;
				HandleResponse(www,httprequest);
				
			} else 
			{
			
				HandleTokenExpired(httprequest);
			}
		}
		public void UploadToserverCall( VKRequest httprequest)
		{
			if (string.IsNullOrEmpty (httprequest.fullurl)) {
				if(httprequest.url.StartsWith("http"))
				{
					httprequest.fullurl=httprequest.url;
				}else{
					httprequest.fullurl =Utilities.GenerateFullHttpReqString(httprequest.url);
				}
			}
			StartCoroutine(_UploadToserverCall(httprequest));
		}
		public void UploadToserverCall(VKRequest requestString,FileForUpload file){
			StartCoroutine(_UploadToserverCall( requestString,file));
		}
		private IEnumerator _UploadToserverCall( VKRequest httprequest)
		{
			var www=GenerateWWWForm (httprequest);
			yield return www;
			HandleResponse (www, httprequest);	
		}
		private IEnumerator _UploadToserverCall(VKRequest httprequest,FileForUpload file)
		{
			var www=GenerateWWWForm (httprequest,file);
			yield return www;
			HandleResponse (www, httprequest);
		}

	}

}
