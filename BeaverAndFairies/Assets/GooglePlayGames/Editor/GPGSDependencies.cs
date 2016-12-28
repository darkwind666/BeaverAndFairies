#if UNITY_ANDROID

using Google.JarResolver;
using UnityEditor;

[InitializeOnLoad]
public static class AppodealDependencies
{

		private static readonly string PluginName = "AppodealUnity";
		
		static AppodealDependencies()
		{
			PlayServicesSupport svcSupport = PlayServicesSupport.CreateInstance(
				PluginName,
				EditorPrefs.GetString("AndroidSdkRoot"),
				"ProjectSettings");

			svcSupport.DependOn("com.google.android.gms",
			                    "play-services-ads",
			                    "LATEST");
			
			svcSupport.DependOn("com.google.android.gms",
			                    "play-services-location",
			                    "LATEST");
			
			svcSupport.DependOn("com.android.support",
			                    "support-v4",
			                    "23.1+");
		}
}
#endif