using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using PlistCS;


namespace UnityEditor.VKEditor
{
	public class FixupFiles
	{
		protected static string Load(string fullPath)
		{
			string data;
			FileInfo projectFileInfo = new FileInfo( fullPath );
			StreamReader fs = projectFileInfo.OpenText();
			data = fs.ReadToEnd();
			fs.Close();
			
			return data;
		}
		
		protected static void Save(string fullPath, string data)
		{
			System.IO.StreamWriter writer = new System.IO.StreamWriter(fullPath, false);
			writer.Write(data);
			writer.Close();
		}
		public static void AddInternetCapabilitieWSA(string path){
			string[] files = Directory.GetFiles( path, "Package.appxmanifest", SearchOption.AllDirectories );
			if (files.Length == 0) {
				Debug.LogError("Serch for Package.appmanifest failed");
				return;
			}
			foreach (var i in files) {
				string fullPath = i;
				string data = Load (fullPath);
				if (string.IsNullOrEmpty (data)) {
					Debug.LogError("Failed loading Package.appmanifest add Internet capability manualy");
					return;
				}
				if(data.Contains("<Capability Name=\"internetClientServer\" />"))
				{
					return;
				}
				if(data.Contains("<Capabilities />")){
					data=Regex.Replace(data,@"<Capabilities />","<Capabilities>\n    <Capability Name=\"internetClientServer\" />\n  </Capabilities>");
				}else{
					data=Regex.Replace(data,@"<Capabilities>","<Capabilities>\n    <Capability Name=\"internetClientServer\" />");
				}
				Save(fullPath,data);
			}
		}
		public static void AddSomeMethodsToMainPage(string path){
			string[] files = Directory.GetFiles( path, "MainPage.xaml.cs", SearchOption.AllDirectories );
			if (files.Length != 1) {
				Debug.LogError("Serch for MainPage.xaml.cs failed");
				return;
			}
			string fullPath = files [0];
			string data = Load (fullPath);

			string[] files1 = Directory.GetFiles( Application.dataPath, "WSAMethods", SearchOption.AllDirectories );
			if (files1.Length != 1) {
				Debug.LogError("Serch for WSAMethods failed");
				return;
			}
			string fullPath1 = files1 [0];
			string data1 = Load (fullPath1);

			if (string.IsNullOrEmpty (data1)) {
				Debug.LogError("Failed loading WSAMethods");
				return;
			}
			data=Regex.Replace(data,"using System;","using System;\nusing com.playGenesis.VkUnityPlugin;\nusing UnityPlayer;\nusing VK.WindowsPhone.SDK;\nusing VK.WindowsPhone.SDK_XAML.Pages;");
			data=Regex.Replace(data,"Window.Current.SizeChanged \\+= onResizeHandler;\\n\\t\\t}",
			                   "Window.Current.SizeChanged += onResizeHandler;\n"+ data1);
			Save(fullPath,data);	

		}
		public static void AddIdCapWebBrowserComponent(string path)
		{
			string[] files = Directory.GetFiles( path, "WMAppManifest.xml", SearchOption.AllDirectories );
			if (files.Length > 1) {
				Debug.LogError("More than one WMAppManifest.xml file in the project");
				return;
			}
			
			string fullPath = files [0];
			string data = Load (fullPath);
			
			if (string.IsNullOrEmpty (data))
				return;
			
			if (data.Contains("<Capability Name=\"ID_CAP_WEBBROWSERCOMPONENT\" />"))
			{
				Console.WriteLine("Already exists");
				return;
				
			}
			
			data = Regex.Replace(data, @"<Capabilities>", "<Capabilities>\n\t\t\t<Capability Name=\"ID_CAP_WEBBROWSERCOMPONENT\" />");
			Save(fullPath,data);
		}
		
		
		public static void FixSimulator(string path)
		{



			string fullPath = Path.Combine(path,  "Info.plist");
			Dictionary<string, object> dict = (Dictionary<string, object>)Plist.readPlist(fullPath);

			AddQueriesSchemes (dict, fullPath);

			var appid = Resources.Load<VkSettings> ("VkSettings").VkAppId;
			var urltipe = new Dictionary<string,object> ();
			urltipe.Add ("CFBundleTypeRole", "Editor");
			urltipe.Add ("CFBundleURLName", "vk"+appid);
			urltipe.Add ("CFBundleURLSchemes", new List<object>{"vk"+appid});
			var listofurltipe = new List<object>{urltipe};

			var check=CheckUrlTypes (dict, fullPath, appid);

			if (check == CFBundleURLTypesExistance.ExistsAndHasNeededKeys) {
				return;
			} else if (check == CFBundleURLTypesExistance.ExistsButNoNeededKeys) {
				var _urltypes = (List<object>)dict ["CFBundleURLTypes"];
				_urltypes.Add (urltipe);
			} else {
				dict.Add ("CFBundleURLTypes", listofurltipe);
			}


			Plist.writeXml(dict, fullPath);
			
		}

		private static CFBundleURLTypesExistance CheckUrlTypes(Dictionary<string, object> dict,string fullPath,int appid)
        { 
			object cfbundleurltypes;
			if (dict.TryGetValue ("CFBundleURLTypes", out cfbundleurltypes)) {
				bool urlTypeAlreadyPresent=false;
				object cfBundleURLName;
				var _urltypes = (List<object>)cfbundleurltypes;
				var array=new List<Dictionary<string,object>> ();
				
				_urltypes.ForEach(t=>{
					array.Add( (Dictionary<string,object>)t );
				});
				array.ForEach(a=>{
					if(a.TryGetValue("CFBundleURLName",out cfBundleURLName))
					{
						if((string)cfBundleURLName=="vk"+appid)
							urlTypeAlreadyPresent=true;
					}
				});
				if(!urlTypeAlreadyPresent)
				{
					return CFBundleURLTypesExistance.ExistsButNoNeededKeys;
				}else{
					return CFBundleURLTypesExistance.ExistsAndHasNeededKeys;
				}
			} else {
				return CFBundleURLTypesExistance.NotExists;
			}
			
		}


		private static void AddQueriesSchemes(Dictionary<string, object> dict,string fullPath)
		{
			var schemesarray = new List<object> ();
			schemesarray.Add ("vkauthorize");
			
			object LSApplicationQueriesSchemes;
			
			if (dict.TryGetValue ("LSApplicationQueriesSchemes", out LSApplicationQueriesSchemes)) 
			{
				var _LSSchemesObjArray = LSApplicationQueriesSchemes as List<object>;
				var _LSSchemesArray=new List<string>();
				foreach (var scheme in _LSSchemesObjArray) {
					_LSSchemesArray.Add((string)scheme);
				}

				if(!_LSSchemesArray.Contains("vkauthorize"))
					_LSSchemesObjArray.Add ("vkauthorize");
				dict["LSApplicationQueriesSchemes"]=(List<object>)_LSSchemesObjArray;
				
			} else {
				dict.Add ("LSApplicationQueriesSchemes", schemesarray);
			}
		}
	}
	enum CFBundleURLTypesExistance
	{
		ExistsAndHasNeededKeys,
		ExistsButNoNeededKeys,
		NotExists
	}
}
