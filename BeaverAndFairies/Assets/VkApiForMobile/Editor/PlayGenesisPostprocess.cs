using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

using UnityEditor.VKXCodeEditor;

namespace UnityEditor.VKEditor
{
	public static class XCodePostProcess
	{
		[PostProcessBuild(300)]
		public static void OnPostProcessBuild(BuildTarget target, string path)
		{
			
			if (target.ToString() == "iOS" || target.ToString() == "iPhone")
			{
				// Create a new project object from build target
				XCProject project = new XCProject( path );
				//project.AddFrameworkSearchPaths (System.IO.Path.Combine (Application.dataPath, "iOSProjectFix/Editor/VKSdk"));
				//project.AddFrameworkSearchPaths ("iOSProjectFix/Editor/VKSdk");
				string[] files = Directory.GetFiles( Application.dataPath, "*.vkprojmods", SearchOption.AllDirectories );
				foreach( string file in files ) {
					UnityEngine.Debug.Log("ProjMod File: "+file);
					project.ApplyMod( file );
				}
				
				project.Save();
				
				FixupFiles.FixSimulator(path);
				
			}
			
			/*if (target == BuildTarget.MetroPlayer) {
				FixupFiles.AddInternetCapabilitieWSA(path);
				FixupFiles.AddSomeMethodsToMainPage(path);
			}*/
			
			
		}
		
		
	}
}
