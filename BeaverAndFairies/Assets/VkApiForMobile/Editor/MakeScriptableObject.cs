using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Linq;
using com.playGenesis.VkUnityPlugin;
public class MakeScriptableObject  {
	[MenuItem("Vk/Create/VKSetting")]
	public static void CreateVkSettingsItem()
	{
		if (Resources.Load<VkSettings> ("VkSettings") != null) {
			EditorUtility.DisplayDialog("Hint","file Assets/VkApiForMobile/Resources/VkSettings.asset already exists," +
			                            "use this option only if accidentaly deleted","ok");
			return;
		}
		VkSettings asset = ScriptableObject.CreateInstance<VkSettings> ();
		AssetDatabase.CreateAsset (asset, "Assets/VkApiForMobile/Resources/VkSettings.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
	[MenuItem("Vk/Edit Vk Settings")]
	public static void EditVkSettings()
	{
		var winds = Resources.FindObjectsOfTypeAll<EditorWindow> ();

		var inspector=winds.Where (t => t.title == "UnityEditor.InspectorWindow").ToList();
		if (inspector.Count == 0) {
			EditorUtility.DisplayDialog("Hint","In order to edit vk setting open Inspector window","ok");
		}
		if (Resources.Load<VkSettings> ("VkSettings") == null) {
			CreateVkSettingsItem ();
		} else {
			VkSettings asset= Resources.Load<VkSettings> ("VkSettings");
			EditorUtility.FocusProjectWindow ();
			Selection.activeObject = asset;
		}


	}

}
