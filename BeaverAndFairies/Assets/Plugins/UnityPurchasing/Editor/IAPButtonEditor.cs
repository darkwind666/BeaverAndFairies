using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	public static class IAPButtonMenu
	{
		[MenuItem ("Window/Unity IAP/Create IAP Button", false, 5)]
		public static void CreateUnityIAPButton()
		{
			// Create Button
			EditorApplication.ExecuteMenuItem("GameObject/UI/Button");

			// Get GameObject of Button
			GameObject gO = Selection.activeGameObject;

			// Add IAP Button component to GameObject
			IAPButton iapButton = null;
			if (gO) {
				iapButton = gO.AddComponent<IAPButton>();
			}

			if (iapButton != null) {
				UnityEditorInternal.ComponentUtility.MoveComponentUp(iapButton);
				UnityEditorInternal.ComponentUtility.MoveComponentUp(iapButton);
				UnityEditorInternal.ComponentUtility.MoveComponentUp(iapButton);
			}
		}
	}


	[CustomEditor(typeof(IAPButton))]
	public class IAPButtonEditor : Editor 
	{
		private static readonly string[] excludedFields = new string[] { "m_Script" };
		private const string kNoProduct = "<None>";
		private List<string> validIDs = null;

		public override void OnInspectorGUI()
		{
			IAPButton button = (IAPButton)target;

			serializedObject.Update();

			EditorGUILayout.LabelField(new GUIContent("Product ID:", "Select a product from the IAP catalog"));

			if (validIDs == null) {
				var catalog = ProductCatalog.LoadDefaultCatalog();

				validIDs = new List<string>();
				validIDs.Add(kNoProduct);
				foreach (var product in catalog.allProducts) {
					validIDs.Add(product.id);
				}
			}

			int currentIndex = string.IsNullOrEmpty(button.productId) ? 0 : validIDs.IndexOf(button.productId);
			int newIndex = EditorGUILayout.Popup(currentIndex, validIDs.ToArray());
			if (newIndex > 0 && newIndex < validIDs.Count) {
				button.productId = validIDs[newIndex];
			} else {
				button.productId = string.Empty;
			}

			if (GUILayout.Button("IAP Catalog...")) {
				ProductCatalogEditor.ShowWindow();
			}
			
			DrawPropertiesExcluding(serializedObject, excludedFields);

			serializedObject.ApplyModifiedProperties();
		}
	}
}

