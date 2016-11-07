using UnityEngine;
using System.Collections;

public class ContactDevelopersController : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	public void contactDeveloper()
	{
		string email = "darkwinddev@gmail.com";
		string subject = MyEscapeURL("FEEDBACK/SUGGESTION");
		string body = MyEscapeURL("Please Enter your message here\n\n\n\n" +
			"________" +
			"\n\nPlease Do Not Modify This\n\n" +
			"Model: "+SystemInfo.deviceModel+"\n\n"+
			"OS: "+SystemInfo.operatingSystem+"\n\n" +
			"________");
		Application.OpenURL ("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}

	string MyEscapeURL (string url) 
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
}
