using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using Unity.Appodeal.Xcode;
using Unity.Appodeal.Xcode.PBX;

using System;
using System.Diagnostics;
using System.IO;
using System.Collections;

#if UNITY_IPHONE
public class AppodealPostProcess : MonoBehaviour
{
	private static string suffix = ".framework";
	private static string absoluteProjPath;

	#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
	private static string AppodealFramework = "Plugins/iOS/Appodeal.framework";
	private static string AppodealBundle = "Plugins/iOS/Appodeal.bundle";
	#endif

	private static string[] frameworkList = new string[] {
		"Twitter", "AdSupport", "AudioToolbox",
		"AVFoundation", "CoreFoundation", "CFNetwork",
		"CoreGraphics", "CoreImage", "CoreMedia",
		"CoreLocation", "CoreTelephony", "GLKit",
		"JavaScriptCore", "EventKitUI", "EventKit",
		"MediaPlayer", "MessageUI", "QuartzCore", 
		"MobileCoreServices", "Security", "StoreKit",
		"SystemConfiguration", "Twitter", "UIKit",
		"CoreBluetooth" 
	};

	private static string[] weakFrameworkList = new string[] {
		"CoreMotion", "WebKit", "Social"
	};


	private static string[] platformLibs = new string[] {
		"libc++.dylib",
		"libz.dylib",
		"libsqlite3.dylib",
		"libxml2.2.dylib"
	};

	[PostProcessBuild(100)]
	public static void OnPostProcessBuild (BuildTarget target, string pathToBuildProject)
	{		
		if (target.ToString () == "iOS" || target.ToString () == "iPhone") {
			PrepareProject (pathToBuildProject);
			UpdatePlist(pathToBuildProject);
		}
	}

	private static void PrepareProject(string buildPath) {
		UnityEngine.Debug.Log("preparing your xcode project for appodeal");
		string projPath = Path.Combine (buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
		absoluteProjPath = Path.GetFullPath(buildPath);
		PBXProject project = new PBXProject ();
		project.ReadFromString (File.ReadAllText(projPath));
		string target = project.TargetGuidByName ("Unity-iPhone");

		AddProjectFrameworks (frameworkList, project, target, false);
		AddProjectFrameworks (weakFrameworkList, project, target, true);
		AddProjectLibs (platformLibs, project, target);
		project.AddBuildProperty (target, "OTHER_LDFLAGS", "-ObjC");
		project.AddBuildProperty (target, "ENABLE_BITCODE", "NO");
		project.AddBuildProperty (target, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");

		string apdFolder = "Adapters";
		string fullPath = Path.Combine (Application.dataPath, string.Format ("Appodeal/{0}", apdFolder));
		if (Directory.Exists(fullPath)) {
			foreach (string file in System.IO.Directory.GetFiles(fullPath)) {
				if(Path.GetExtension(file).Equals(".zip")) {
					ExtractZip (file, Path.Combine (absoluteProjPath, apdFolder));
					AddAdaptersDirectory (apdFolder, project, target);
				}
			}
		}

		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
		project.AddBuildProperty (target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks/Plugins/iOS");
		project.SetBuildProperty (target, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");
		CopyAndReplaceDirectory ("Assets/" + AppodealFramework, Path.Combine(buildPath, "Frameworks/" + AppodealFramework));
		project.AddFileToBuild(target, project.AddFile("Frameworks/" + AppodealFramework, "Frameworks/" + AppodealFramework, PBXSourceTree.Source));
		CopyAndReplaceDirectory ("Assets/" + AppodealBundle, Path.Combine(buildPath, "Frameworks/" + AppodealBundle));
		project.AddFileToBuild(target,  project.AddFile("Frameworks/" + AppodealBundle,  "Frameworks/" + AppodealBundle, PBXSourceTree.Source));
		#endif

		File.WriteAllText (projPath, project.WriteToString());
	}

	protected static void AddProjectFrameworks(string[] frameworks, PBXProject project, string target, bool weak)
	{
		foreach (string framework in frameworks) {
			if (!project.HasFramework (framework)) {
				project.AddFrameworkToProject (target, framework + suffix, weak);
			}
		}
	}

	protected static void AddProjectLibs(string[] libs, PBXProject project, string target)
	{
		foreach (string lib in libs) {
			string libGUID = project.AddFile ("usr/lib/" + lib, "Libraries/" + lib, PBXSourceTree.Sdk);
			project.AddFileToBuild (target, libGUID);
		}	
	}

	private static void UpdatePlist (string buildPath)
	{
		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
		string plistPath = Path.Combine (buildPath, "Info.plist");
		PlistDocument plist = new PlistDocument ();
		plist.ReadFromString(File.ReadAllText (plistPath));	
		PlistElementDict dict = plist.root.CreateDict ("NSAppTransportSecurity");
		dict.SetBoolean ("NSAllowsArbitraryLoads", true);
		File.WriteAllText(plistPath, plist.WriteToString());
		#endif
	}

	private static void CopyAndReplaceDirectory(string srcPath, string dstPath)
	{
		if (Directory.Exists(dstPath)) {
			Directory.Delete(dstPath);
		}
		if (File.Exists(dstPath)) {
			File.Delete(dstPath);
		}

		Directory.CreateDirectory(dstPath);

		foreach (var file in Directory.GetFiles(srcPath)) {
			if(!file.Contains(".meta")) {
				File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));
			}
		}

		foreach (var dir in Directory.GetDirectories(srcPath)) {
			CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
		}
	}

	private static void ExtractZip(string filePath, string destFolder){
		using(Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(filePath)){			
			foreach(Ionic.Zip.ZipEntry z in zip){
				z.Extract(destFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
			}
		}
	}

	private static void AddAdaptersDirectory(string path, PBXProject proj, string targetGuid)
	{
		if (path.EndsWith ("__MACOSX",StringComparison.CurrentCultureIgnoreCase))
			return;	

		if (path.EndsWith (".framework", StringComparison.CurrentCultureIgnoreCase)) {
			proj.AddFileToBuild (targetGuid, proj.AddFile (path, path));
			string tmp = Utils.FixSlashesInPath(string.Format ("$(PROJECT_DIR)/{0}", path.Substring (0, path.LastIndexOf (Path.DirectorySeparatorChar))));
			proj.AddBuildProperty (targetGuid, "FRAMEWORK_SEARCH_PATHS", tmp);
			return;
		} else if(path.EndsWith (".bundle", StringComparison.CurrentCultureIgnoreCase)){			
			proj.AddFileToBuild (targetGuid, proj.AddFile (path, path));
			return;
		}

		string fileName;
		bool libPathAdded = false;
		bool headPathAdded = false;

		string realDstPath = Path.Combine (absoluteProjPath, path);
		foreach (var filePath in Directory.GetFiles(realDstPath)) {
			fileName = Path.GetFileName (filePath);

			if (fileName.EndsWith (".DS_Store"))
				continue;

			proj.AddFileToBuild (targetGuid, proj.AddFile (Path.Combine (path, fileName), Path.Combine (path, fileName), PBXSourceTree.Source));
			if(!libPathAdded && fileName.EndsWith(".a")){				
				proj.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", Utils.FixSlashesInPath(string.Format("$(PROJECT_DIR)/{0}", path)));			
				libPathAdded = true;	
			}

			if(!headPathAdded && fileName.EndsWith(".h")){				
						proj.AddBuildProperty(targetGuid, "HEADER_SEARCH_PATHS", Utils.FixSlashesInPath(string.Format("$(PROJECT_DIR)/{0}", path)));			
				headPathAdded = true;	
			}
		}

		foreach (var subPath in Directory.GetDirectories(realDstPath)){	
			AddAdaptersDirectory(Path.Combine(path,Path.GetFileName(subPath)), proj, targetGuid);
		}
	}

}
#endif