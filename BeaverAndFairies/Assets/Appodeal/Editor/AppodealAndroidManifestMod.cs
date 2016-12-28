using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

public class AppodealAndroidManifestMod {

	private const string activity = "activity";
	private const string metadata = "meta-data";
	private const string service = "service";
	private const string receiver = "receiver";
	private const string permission = "uses-permission";

	//permissions
	private const string accessNetworkStatePermissionName = "android.permission.ACCESS_NETWORK_STATE";
	private const string internetPermissionName = "android.permission.INTERNET";
	private const string writePermissionName = "android.permission.WRITE_EXTERNAL_STORAGE";
	private const string locationPermissionName = "android.permission.ACCESS_COARSE_LOCATION";

	//Meta-data
	private const string appodealMetaDataName = "com.appodeal.framework";
	private const string googleMetaDataName = "com.google.android.gms.version";

	//Activities
	private const string appodealInterstitialActivityName = "com.appodeal.ads.InterstitialActivity";
	private const string appodealVideoActivityName = "com.appodeal.ads.VideoActivity";
	private const string appodealLoaderActivityName = "com.appodeal.ads.LoaderActivity";
	private const string admobActivityName = "com.google.android.gms.ads.AdActivity";
	private const string chartboostActivityName = "com.chartboost.sdk.CBImpressionActivity";
	private const string applovinActivityName = "com.applovin.adview.AppLovinInterstitialActivity";
	private const string mopubActivityName = "com.mopub.mobileads.MoPubActivity";
	private const string mopubBrowserActivityName = "com.mopub.common.MoPubBrowser";
	private const string mopubMraidActivityName = "com.mopub.mobileads.MraidActivity";
	private const string mopubMraidVideoActivityName = "com.mopub.mobileads.MraidVideoPlayerActivity";
	private const string nexageMraidActivityName = "org.nexage.sourcekit.mraid.MRAIDBrowser";
	private const string nexageVastActivityName = "org.nexage.sourcekit.vast.activity.VASTActivity";
	private const string nexageVpaidActivityName = "org.nexage.sourcekit.vast.activity.VPAIDActivity";
	private const string appodealVpaidActivityName = "com.appodeal.ads.networks.vpaid.VPAIDActivity";
	private const string amazonAdsActivityName = "com.amazon.device.ads.AdActivity";
	private const string mailruActivityName = "com.my.target.ads.MyTargetActivity";
	private const string spotxActivityName = "com.appodeal.ads.networks.SpotXActivity";
	private const string facebookActivityName = "com.facebook.ads.InterstitialAdActivity";
	private const string unityAdsAdUnitActivityName = "com.unity3d.ads.adunit.AdUnitActivity";
	private const string unityAdsAdUnitSoftwareActivityName = "com.unity3d.ads.adunit.AdUnitSoftwareActivity";
	private const string unityAdsAdUnitActivityName2 = "com.unity3d.ads2.adunit.AdUnitActivity";
	private const string unityAdsAdUnitSoftwareActivityName2 = "com.unity3d.ads2.adunit.AdUnitSoftwareActivity";
	private const string adcolonyOverlayActivityName = "com.jirbo.adcolony.AdColonyOverlay";
	private const string adcolonyFullscreenActivityName = "com.jirbo.adcolony.AdColonyFullscreen";
	private const string adcolonyBrowserActivityName = "com.jirbo.adcolony.AdColonyBrowser";
	private const string vungleActivityName = "com.vungle.publisher.FullScreenAdActivity";
	private const string startapp3dActivityName = "com.startapp.android.publish.list3d.List3DActivity";
	private const string startappOverlayActivityName = "com.startapp.android.publish.OverlayActivity";
	private const string startappFullscreenActivityName = "com.startapp.android.publish.FullScreenActivity";
	private const string yandexAdActivityName = "com.yandex.mobile.ads.AdActivity";
	private const string flurryActivityName = "com.flurry.android.FlurryFullscreenTakeoverActivity";
	private const string appodealVideoPlayerActivityName = "com.appodeal.ads.VideoPlayerActivity";
	private const string TJAdUnitActivityName = "com.tapjoy.TJAdUnitActivity";
	private const string TJActionHandlerActivityName = "com.tapjoy.mraid.view.ActionHandler";
	private const string TJBrowserActivityName = "com.tapjoy.mraid.view.Browser";
	private const string TJContentActivityActivityName = "com.tapjoy.TJContentActivity";
	private const string RevmobActivityName = "com.revmob.FullscreenActivity";

	//Recievers
	private const string appodealPackageAddedReceiverName = "com.appodeal.ads.AppodealPackageAddedReceiver";
	private const string yandexReceiverName = "com.yandex.metrica.MetricaEventHandler";
	
	//Services
	private const string yandexServiceName = "com.yandex.metrica.MetricaService";


	public static void GenerateManifest() {
		var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
		if (!File.Exists (outputFile)) {
			bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", "AndroidManifest.xml does not exists, would you like to create new one?", "Yes", "No");
			if (val) { 
				var inputFile = Path.Combine (Application.dataPath, "Plugins/Android/AppodealAndroidManifest.xml");
				File.Copy (inputFile, outputFile);
			}
		} else {
			UpdateManifest(outputFile, false);
		}

	}

	public static void CheckManifest() {
		var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
		if (!File.Exists (outputFile)) {
			bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", "AndroidManifest.xml does not exists, would you like to create new one?", "Yes", "No");
			if (val) { 
				var inputFile = Path.Combine (Application.dataPath, "Plugins/Android/AppodealAndroidManifest.xml");
				File.Copy (inputFile, outputFile);
			}
		} else {
			UpdateManifest(outputFile, true);
		}
	}

	public static void UpdateManifest(string fullPath, bool check) {
		XmlDocument doc = new XmlDocument();
		doc.Load(fullPath);
		
		if (doc == null) {
			bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", "Error parsing " + fullPath + " would you like to replace it with new configurated AndroidMaifest.xml?", "Yes", "No");
			if (val) { 
				var inputFile = Path.Combine (Application.dataPath, "Plugins/Android/AppodealAndroidManifest.xml");
				var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
				File.Copy (inputFile, outputFile);
			}
			return;
		}
		
		XmlNode manNode = FindChildNode(doc, "manifest");
		XmlNode dict = FindChildNode(manNode, "application");
		
		if (dict == null) {
			bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", "Error parsing " + fullPath + " would you like to replace it with new configurated AndroidManifest.xml?", "Yes", "No");
			if (val) { 
				var inputFile = Path.Combine (Application.dataPath, "Plugins/Android/AppodealAndroidManifest.xml");
				var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
				File.Copy (inputFile, outputFile);
			}
			return;
		}
		
		string ns = dict.GetNamespaceOfPrefix("android");

		//PERMISSION'S
		XmlElement accessNetworkStatePermission = FindElementWithAndroidName(permission, "name", ns, accessNetworkStatePermissionName, manNode);
		if (accessNetworkStatePermission == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", accessNetworkStatePermissionName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					accessNetworkStatePermission = CreatePermission(doc, ns, accessNetworkStatePermissionName);
					manNode.AppendChild(accessNetworkStatePermission);
				}
			} else {
				accessNetworkStatePermission = CreatePermission(doc, ns, accessNetworkStatePermissionName);
				manNode.AppendChild(accessNetworkStatePermission);
			}
		}

		XmlElement internetPermission = FindElementWithAndroidName(permission, "name", ns, internetPermissionName, manNode);
		if (internetPermission == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", internetPermissionName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					internetPermission = CreatePermission(doc, ns, internetPermissionName);
					manNode.AppendChild(internetPermission);
				}
			} else {
				internetPermission = CreatePermission(doc, ns, internetPermissionName);
				manNode.AppendChild(internetPermission);
			}
		}

		XmlElement writePermission = FindElementWithAndroidName(permission, "name", ns, writePermissionName, manNode);
		if (writePermission == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", writePermissionName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					writePermission = CreatePermission(doc, ns, writePermissionName);
					manNode.AppendChild(writePermission);
				}
			} else {
				writePermission = CreatePermission(doc, ns, writePermissionName);
				manNode.AppendChild(writePermission);
			}
		}

		XmlElement locationPermission = FindElementWithAndroidName(permission, "name", ns, locationPermissionName, manNode);
		if (locationPermission == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", locationPermissionName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					locationPermission = CreatePermission(doc, ns, locationPermissionName);
					manNode.AppendChild(locationPermission);
				}
			} else {
				locationPermission = CreatePermission(doc, ns, locationPermissionName);
				manNode.AppendChild(locationPermission);
			}
		}

		//APPLICATION
		XmlElement appodealReceiverElement = FindElementWithAndroidName(receiver, "name", ns, appodealPackageAddedReceiverName, dict);
		if (appodealReceiverElement == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealPackageAddedReceiverName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealReceiverElement = doc.CreateElement(receiver);
					appodealReceiverElement.SetAttribute("name", ns, appodealPackageAddedReceiverName);
					appodealReceiverElement.SetAttribute("enabled", ns, "true");
					appodealReceiverElement.SetAttribute("exported", ns, "true");
					XmlElement intentElement = doc.CreateElement("intent-filter");
					XmlElement actionElement = doc.CreateElement("action");
					XmlElement dataElement = doc.CreateElement("data");
					actionElement.SetAttribute("name", ns, "android.intent.action.PACKAGE_ADDED");
					dataElement.SetAttribute("scheme", ns, "package");
					dict.AppendChild(appodealReceiverElement);
					appodealReceiverElement.AppendChild(intentElement);
					intentElement.AppendChild(actionElement);
					intentElement.AppendChild(dataElement);
				}
			} else {
				appodealReceiverElement = doc.CreateElement(receiver);
				appodealReceiverElement.SetAttribute("name", ns, appodealPackageAddedReceiverName);
				appodealReceiverElement.SetAttribute("enabled", ns, "true");
				appodealReceiverElement.SetAttribute("exported", ns, "true");
				XmlElement intentElement = doc.CreateElement("intent-filter");
				XmlElement actionElement = doc.CreateElement("action");
				XmlElement dataElement = doc.CreateElement("data");
				actionElement.SetAttribute("name", ns, "android.intent.action.PACKAGE_ADDED");
				dataElement.SetAttribute("scheme", ns, "package");
				dict.AppendChild(appodealReceiverElement);
				appodealReceiverElement.AppendChild(intentElement);
				intentElement.AppendChild(actionElement);
				intentElement.AppendChild(dataElement);
			}
		}

		XmlElement appodealMetaDataElement = FindElementWithAndroidName(metadata, "name", ns, appodealMetaDataName, dict);
		if (appodealMetaDataElement == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealMetaDataName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealMetaDataElement = CreateMetaDataElement(doc, ns, appodealMetaDataName, "unity");
					dict.AppendChild(appodealMetaDataElement);
				}
			} else {
				appodealMetaDataElement = CreateMetaDataElement(doc, ns, appodealMetaDataName, "unity");
				dict.AppendChild(appodealMetaDataElement);
			}
		}

		XmlElement appodealInterstitialActivity = FindElementWithAndroidName(activity, "name", ns, appodealInterstitialActivityName, dict);
		if (appodealInterstitialActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealInterstitialActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealInterstitialActivity = CreateActivityElement(doc, ns, appodealInterstitialActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
					dict.AppendChild(appodealInterstitialActivity);
				}
			} else {
				appodealInterstitialActivity = CreateActivityElement(doc, ns, appodealInterstitialActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
				dict.AppendChild(appodealInterstitialActivity);
			}
		}

		XmlElement appodealVideoActivity = FindElementWithAndroidName(activity, "name", ns, appodealVideoActivityName, dict);
		if (appodealVideoActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealVideoActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealVideoActivity = CreateActivityElement(doc, ns, appodealVideoActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
					dict.AppendChild(appodealVideoActivity);
				}
			} else {
				appodealVideoActivity = CreateActivityElement(doc, ns, appodealVideoActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
				dict.AppendChild(appodealVideoActivity);
			}
		}

		XmlElement appodealLoaderActivity = FindElementWithAndroidName(activity, "name", ns, appodealLoaderActivityName, dict);
		if (appodealLoaderActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealLoaderActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealLoaderActivity = CreateActivityElement(doc, ns, appodealLoaderActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
					dict.AppendChild(appodealLoaderActivity);
				}
			} else {
				appodealLoaderActivity = CreateActivityElement(doc, ns, appodealLoaderActivityName, "orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar");
				dict.AppendChild(appodealLoaderActivity);
			}
		}

		XmlElement googleMetaDataElement = FindElementWithAndroidName(metadata, "name", ns, googleMetaDataName, dict);
		if (googleMetaDataElement == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", googleMetaDataName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					googleMetaDataElement = CreateMetaDataElement(doc, ns, googleMetaDataName, "@integer/google_play_services_version");
					dict.AppendChild(googleMetaDataElement);
				}
			} else {
				googleMetaDataElement = CreateMetaDataElement(doc, ns, googleMetaDataName, "@integer/google_play_services_version");
				dict.AppendChild(googleMetaDataElement);
			}
		}

		XmlElement admobActivity = FindElementWithAndroidName(activity, "name", ns, admobActivityName, dict);
		if (admobActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", admobActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					admobActivity = CreateActivityElement(doc, ns, admobActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", "@android:style/Theme.Translucent");
					dict.AppendChild(admobActivity);
				}
			} else {
				admobActivity = CreateActivityElement(doc, ns, admobActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", "@android:style/Theme.Translucent");
				dict.AppendChild(admobActivity);
			}
		}

		XmlElement cbActivity = FindElementWithAndroidName(activity, "name", ns, chartboostActivityName, dict);
		if (cbActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", chartboostActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					cbActivity = CreateExcludeHardwareActivityElement(doc, ns, chartboostActivityName, "true", "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen", "true");
					dict.AppendChild(cbActivity);
				}
			} else {
				cbActivity = CreateExcludeHardwareActivityElement(doc, ns, chartboostActivityName, "true", "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen", "true");
				dict.AppendChild(cbActivity);
			}
		}

		XmlElement applovinActivity = FindElementWithAndroidName(activity, "name", ns, applovinActivityName, dict);
		if (applovinActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", applovinActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					applovinActivity = CreateActivityElement(doc, ns, "com.applovin.adview.AppLovinInterstitialActivity", null, "@android:style/Theme.Translucent");
					dict.AppendChild(applovinActivity);
				}
			} else {
				applovinActivity = CreateActivityElement(doc, ns, "com.applovin.adview.AppLovinInterstitialActivity", null, "@android:style/Theme.Translucent");
				dict.AppendChild(applovinActivity);
			}
		}

		XmlElement mopubActivity = FindElementWithAndroidName(activity, "name", ns, mopubActivityName, dict);
		if (mopubActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", mopubActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					mopubActivity = CreateActivityElement(doc, ns, mopubActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent");
					dict.AppendChild(mopubActivity);
				}
			} else {
				mopubActivity = CreateActivityElement(doc, ns, mopubActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent");
				dict.AppendChild(mopubActivity);
			}
		}

		XmlElement mopubBrowserActivity = FindElementWithAndroidName(activity, "name", ns, mopubBrowserActivityName, dict);
		if (mopubBrowserActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", mopubBrowserActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					mopubBrowserActivity = CreateActivityElement(doc, ns, mopubBrowserActivityName, "keyboardHidden|orientation|screenSize", null);
					dict.AppendChild(mopubBrowserActivity);
				}
			} else {
				mopubBrowserActivity = CreateActivityElement(doc, ns, mopubBrowserActivityName, "keyboardHidden|orientation|screenSize", null);
				dict.AppendChild(mopubBrowserActivity);
			}
		}

		XmlElement mopubMraidActivity = FindElementWithAndroidName(activity, "name", ns, mopubMraidActivityName, dict);
		if (mopubMraidActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", mopubMraidActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					mopubBrowserActivity = CreateActivityElement(doc, ns, mopubMraidActivityName, "keyboardHidden|orientation|screenSize", null);
					dict.AppendChild(mopubBrowserActivity);
				}
			} else {
				mopubBrowserActivity = CreateActivityElement(doc, ns, mopubMraidActivityName, "keyboardHidden|orientation|screenSize", null);
				dict.AppendChild(mopubBrowserActivity);
			}
		}

		XmlElement mopubMraidVideoPlayerActivity = FindElementWithAndroidName(activity, "name", ns, mopubMraidVideoActivityName, dict);
		if (mopubMraidVideoPlayerActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", mopubMraidVideoActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					mopubMraidVideoPlayerActivity = CreateActivityElement(doc, ns, mopubMraidVideoActivityName, "keyboardHidden|orientation|screenSize", null);
					dict.AppendChild(mopubMraidVideoPlayerActivity);
				}
			} else {
				mopubMraidVideoPlayerActivity = CreateActivityElement(doc, ns, mopubMraidVideoActivityName, "keyboardHidden|orientation|screenSize", null);
				dict.AppendChild(mopubMraidVideoPlayerActivity);
			}
		}

		XmlElement nexageMraidActivity = FindElementWithAndroidName(activity, "name", ns, nexageMraidActivityName, dict);
		if (nexageMraidActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", nexageMraidActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					nexageMraidActivity = CreateActivityElement(doc, ns, nexageMraidActivityName, "orientation|keyboard|keyboardHidden|screenSize", "@android:style/Theme.Translucent");
					dict.AppendChild(nexageMraidActivity);
				}
			} else {
				nexageMraidActivity = CreateActivityElement(doc, ns, nexageMraidActivityName, "orientation|keyboard|keyboardHidden|screenSize", "@android:style/Theme.Translucent");
				dict.AppendChild(nexageMraidActivity);
			}
		}

		XmlElement amazonAdActivity = FindElementWithAndroidName(activity, "name", ns, amazonAdsActivityName, dict);
		if (amazonAdActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", amazonAdsActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					amazonAdActivity = CreateActivityElement(doc, ns, amazonAdsActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent");
					dict.AppendChild(amazonAdActivity);
				}
			} else {
				amazonAdActivity = CreateActivityElement(doc, ns, amazonAdsActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent");
				dict.AppendChild(amazonAdActivity);
			}
		}

		XmlElement mailruActivity = FindElementWithAndroidName(activity, "name", ns, mailruActivityName, dict);
		if (mailruActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", mailruActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					mailruActivity = CreateHardwareActivity(doc, ns, mailruActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null, "true");
					dict.AppendChild(mailruActivity);
				}
			} else {
				mailruActivity = CreateHardwareActivity(doc, ns, mailruActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null, "true");
				dict.AppendChild(mailruActivity);
			}
		}

		XmlElement nexageVastActivity = FindElementWithAndroidName(activity, "name", ns, nexageVastActivityName, dict);
		if (nexageVastActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", nexageVastActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					nexageVastActivity = CreateActivityElement(doc, ns, nexageVastActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
					dict.AppendChild(nexageVastActivity);
				}
			} else {
				nexageVastActivity = CreateActivityElement(doc, ns, nexageVastActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
				dict.AppendChild(nexageVastActivity);
			}
		}

		XmlElement nexageVpaidActivity = FindElementWithAndroidName(activity, "name", ns, nexageVpaidActivityName, dict);
		if (nexageVpaidActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", nexageVpaidActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					nexageVpaidActivity = CreateActivityElement(doc, ns, nexageVpaidActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
					dict.AppendChild(nexageVpaidActivity);
				}
			} else {
				nexageVpaidActivity = CreateActivityElement(doc, ns, nexageVpaidActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
				dict.AppendChild(nexageVpaidActivity);
			}
		}

		XmlElement appodealVpaidActivity = FindElementWithAndroidName(activity, "name", ns, appodealVpaidActivityName, dict);
		if (appodealVpaidActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealVpaidActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealVpaidActivity = CreateActivityElement(doc, ns, appodealVpaidActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
					dict.AppendChild(appodealVpaidActivity);
				}
			} else {
				appodealVpaidActivity = CreateActivityElement(doc, ns, appodealVpaidActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
				dict.AppendChild(appodealVpaidActivity);
			}
		}

		XmlElement spotxActivity = FindElementWithAndroidName(activity, "name", ns, spotxActivityName, dict);
		if (spotxActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", spotxActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					spotxActivity = CreateActivityElement(doc, ns, spotxActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
					dict.AppendChild(spotxActivity);
				}
			} else {
				spotxActivity = CreateActivityElement(doc, ns, spotxActivityName, null, "@android:style/Theme.NoTitleBar.Fullscreen");
				dict.AppendChild(spotxActivity);
			}
		}

		XmlElement facebookActivity = FindElementWithAndroidName(activity, "name", ns, facebookActivityName, dict);
		if (facebookActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", facebookActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					facebookActivity = CreateActivityElement(doc, ns, facebookActivityName, "keyboardHidden|orientation|screenSize", null);
					dict.AppendChild(facebookActivity);
				}
			} else {
				facebookActivity = CreateActivityElement(doc, ns, facebookActivityName, "keyboardHidden|orientation|screenSize", null);
				dict.AppendChild(facebookActivity);
			}
		}

		XmlElement unityAdsAdUnitActivity = FindElementWithAndroidName(activity, "name", ns, unityAdsAdUnitActivityName, dict);
		if (unityAdsAdUnitActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", unityAdsAdUnitActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					unityAdsAdUnitActivity = CreateHardwareActivity(doc, ns, unityAdsAdUnitActivityName, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "true");
					dict.AppendChild(unityAdsAdUnitActivity);
				}
			} else {
				unityAdsAdUnitActivity = CreateHardwareActivity(doc, ns, unityAdsAdUnitActivityName, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "true");
				dict.AppendChild(unityAdsAdUnitActivity);
			}
		}

		XmlElement unityAdsAdUnitSoftwareActivity = FindElementWithAndroidName(activity, "name", ns, unityAdsAdUnitSoftwareActivityName, dict);
		if (unityAdsAdUnitSoftwareActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", unityAdsAdUnitSoftwareActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					unityAdsAdUnitSoftwareActivity = CreateHardwareActivity(doc, ns, unityAdsAdUnitSoftwareActivityName, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "false");
					dict.AppendChild(unityAdsAdUnitSoftwareActivity);
				}
			} else {
				unityAdsAdUnitSoftwareActivity = CreateHardwareActivity(doc, ns, unityAdsAdUnitSoftwareActivityName, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "false");
				dict.AppendChild(unityAdsAdUnitSoftwareActivity);
			}
		}

		XmlElement unityAdsAdUnitActivity2 = FindElementWithAndroidName(activity, "name", ns, unityAdsAdUnitActivityName2, dict);
		if (unityAdsAdUnitActivity2 == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", unityAdsAdUnitActivityName2 + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					unityAdsAdUnitActivity2 = CreateHardwareActivity(doc, ns, unityAdsAdUnitActivityName2, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "true");
					dict.AppendChild(unityAdsAdUnitActivity2);
				}
			} else {
				unityAdsAdUnitActivity2 = CreateHardwareActivity(doc, ns, unityAdsAdUnitActivityName2, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "true");
				dict.AppendChild(unityAdsAdUnitActivity2);
			}
		}

		XmlElement unityAdsAdUnitSoftwareActivity2 = FindElementWithAndroidName(activity, "name", ns, unityAdsAdUnitSoftwareActivityName2, dict);
		if (unityAdsAdUnitSoftwareActivity2 == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", unityAdsAdUnitSoftwareActivityName2 + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					unityAdsAdUnitSoftwareActivity2 = CreateHardwareActivity(doc, ns, unityAdsAdUnitSoftwareActivityName2, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "false");
					dict.AppendChild(unityAdsAdUnitSoftwareActivity2);
				}
			} else {
				unityAdsAdUnitSoftwareActivity2 = CreateHardwareActivity(doc, ns, unityAdsAdUnitSoftwareActivityName2, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen", "@android:style/Theme.NoTitleBar.Fullscreen", "false");
				dict.AppendChild(unityAdsAdUnitSoftwareActivity2);
			}
		}

		XmlElement adcolonyActivity = FindElementWithAndroidName(activity, "name", ns, adcolonyOverlayActivityName, dict);
		if (adcolonyActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", adcolonyOverlayActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					adcolonyActivity = CreateActivityElement(doc, ns, adcolonyOverlayActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
					dict.AppendChild(adcolonyActivity);
				}
			} else {
				adcolonyActivity = CreateActivityElement(doc, ns, adcolonyOverlayActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
				dict.AppendChild(adcolonyActivity);
			}
		}

		XmlElement adcolonyFullscreenActivity = FindElementWithAndroidName(activity, "name", ns, adcolonyFullscreenActivityName, dict);
		if (adcolonyFullscreenActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", adcolonyFullscreenActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					adcolonyFullscreenActivity = CreateActivityElement(doc, ns, adcolonyFullscreenActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
					dict.AppendChild(adcolonyFullscreenActivity);
				}
			} else {
				adcolonyFullscreenActivity = CreateActivityElement(doc, ns, adcolonyFullscreenActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
				dict.AppendChild(adcolonyFullscreenActivity);
			}
		}

		XmlElement adcolonyBrowserActivity = FindElementWithAndroidName(activity, "name", ns, adcolonyBrowserActivityName, dict);
		if (adcolonyBrowserActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", adcolonyBrowserActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					adcolonyBrowserActivity = CreateActivityElement(doc, ns, adcolonyBrowserActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
					dict.AppendChild(adcolonyBrowserActivity);
				}
			} else {
				adcolonyBrowserActivity = CreateActivityElement(doc, ns, adcolonyBrowserActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
				dict.AppendChild(adcolonyBrowserActivity);
			}
		}

		XmlElement vungleActivity = FindElementWithAndroidName(activity, "name", ns, vungleActivityName, dict);
		if (vungleActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", vungleActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					vungleActivity = CreateActivityElement(doc, ns, vungleActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.NoTitleBar.Fullscreen");
					dict.AppendChild(vungleActivity);
				}
			} else {
				vungleActivity = CreateActivityElement(doc, ns, vungleActivityName, "keyboardHidden|orientation|screenSize", "@android:style/Theme.NoTitleBar.Fullscreen");
				dict.AppendChild(vungleActivity);
			}
		}

		XmlElement startapp3DActivity = FindElementWithAndroidName(activity, "name", ns, startapp3dActivityName, dict);
		if (startapp3DActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", startapp3dActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					startapp3DActivity = CreateActivityElement(doc, ns, startapp3dActivityName, null, "@android:style/Theme");
					dict.AppendChild(startapp3DActivity);
				}
			} else {
				startapp3DActivity = CreateActivityElement(doc, ns, startapp3dActivityName, null, "@android:style/Theme");
				dict.AppendChild(startapp3DActivity);
			}
		}

		XmlElement startappOverlayActivity = FindElementWithAndroidName(activity, "name", ns, startappOverlayActivityName, dict);
		if (startappOverlayActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", startappOverlayActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					startappOverlayActivity = CreateActivityElement(doc, ns, startappOverlayActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent");
					dict.AppendChild(startappOverlayActivity);
				}
			} else {
				startappOverlayActivity = CreateActivityElement(doc, ns, startappOverlayActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent");
				dict.AppendChild(startappOverlayActivity);
			}
		}

		XmlElement startappActivity = FindElementWithAndroidName(activity, "name", ns, startappFullscreenActivityName, dict);
		if (startappActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", startappFullscreenActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					startappActivity = CreateActivityElement(doc, ns, startappFullscreenActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme");
					dict.AppendChild(startappActivity);
				}
			} else {
				startappActivity = CreateActivityElement(doc, ns, startappFullscreenActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme");
				dict.AppendChild(startappActivity);
			}
		}

		XmlElement yandexService = FindElementWithAndroidName(service, "name", ns, yandexServiceName, dict);
		if (yandexService == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", yandexServiceName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					yandexService = doc.CreateElement(service);
					yandexService.SetAttribute("name", ns, yandexServiceName);
					yandexService.SetAttribute("enabled", ns, "true");
					yandexService.SetAttribute("exported", ns, "true");
					yandexService.SetAttribute("process", ns, ":Metrica");
					XmlElement intentElement = doc.CreateElement("intent-filter");
					XmlElement categoryElement = doc.CreateElement("category");
					categoryElement.SetAttribute("name", ns, "android.intent.category.DEFAULT");
					XmlElement actionElement = doc.CreateElement("action");
					actionElement.SetAttribute("name", ns, "com.yandex.metrica.IMetricaService");
					XmlElement dataElement = doc.CreateElement("data");
					dataElement.SetAttribute("scheme", ns, "metrica");
					XmlElement yandexMetaDataElement = doc.CreateElement("meta-data");
					yandexMetaDataElement.SetAttribute("name", ns, "metrica:api:level");
					yandexMetaDataElement.SetAttribute("value", ns, "48");
					dict.AppendChild(yandexService);
					yandexService.AppendChild(intentElement);
					yandexService.AppendChild(yandexMetaDataElement);
					intentElement.AppendChild(categoryElement);
					intentElement.AppendChild(actionElement);
					intentElement.AppendChild(categoryElement);
				}
			} else {
				yandexService = doc.CreateElement(service);
				yandexService.SetAttribute("name", ns, yandexServiceName);
				yandexService.SetAttribute("enabled", ns, "true");
				yandexService.SetAttribute("exported", ns, "true");
				yandexService.SetAttribute("process", ns, ":Metrica");
				XmlElement intentElement = doc.CreateElement("intent-filter");
				XmlElement categoryElement = doc.CreateElement("category");
				categoryElement.SetAttribute("name", ns, "android.intent.category.DEFAULT");
				XmlElement actionElement = doc.CreateElement("action");
				actionElement.SetAttribute("name", ns, "com.yandex.metrica.IMetricaService");
				XmlElement dataElement = doc.CreateElement("data");
				dataElement.SetAttribute("scheme", ns, "metrica");
				XmlElement yandexMetaDataElement = doc.CreateElement("meta-data");
				yandexMetaDataElement.SetAttribute("name", ns, "metrica:api:level");
				yandexMetaDataElement.SetAttribute("value", ns, "48");
				dict.AppendChild(yandexService);
				yandexService.AppendChild(intentElement);
				yandexService.AppendChild(yandexMetaDataElement);
				intentElement.AppendChild(categoryElement);
				intentElement.AppendChild(actionElement);
				intentElement.AppendChild(categoryElement);
			}
		}

		XmlElement yandexReceiver = FindElementWithAndroidName(receiver, "name", ns, yandexReceiverName, dict);
		if (yandexReceiver == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", yandexReceiverName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					yandexReceiver = doc.CreateElement(receiver);
					yandexReceiver.SetAttribute("name", ns, yandexReceiverName);
					yandexReceiver.SetAttribute("enabled", ns, "true");
					yandexReceiver.SetAttribute("exported", ns, "true");
					XmlElement intentElement = doc.CreateElement("intent-filter");
					XmlElement actionElement = doc.CreateElement("action");
					actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
					dict.AppendChild(yandexReceiver);
					yandexReceiver.AppendChild(intentElement);
					intentElement.AppendChild(actionElement);
				}
			} else {
				yandexReceiver = doc.CreateElement(receiver);
				yandexReceiver.SetAttribute("name", ns, yandexReceiverName);
				yandexReceiver.SetAttribute("enabled", ns, "true");
				yandexReceiver.SetAttribute("exported", ns, "true");
				XmlElement intentElement = doc.CreateElement("intent-filter");
				XmlElement actionElement = doc.CreateElement("action");
				actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
				dict.AppendChild(yandexReceiver);
				yandexReceiver.AppendChild(intentElement);
				intentElement.AppendChild(actionElement);
			}
		}

		XmlElement yandexAdsActivity = FindElementWithAndroidName(activity, "name", ns, yandexAdActivityName, dict);
		if (yandexAdsActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", yandexAdActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					yandexAdsActivity = CreateActivityElement(doc, ns, yandexAdActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
					dict.AppendChild(yandexAdsActivity);
				}
			} else {
				yandexAdsActivity = CreateActivityElement(doc, ns, yandexAdActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
				dict.AppendChild(yandexAdsActivity);
			}
		}

		XmlElement flurryActivity = FindElementWithAndroidName(activity, "name", ns, flurryActivityName, dict);
		if (flurryActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", flurryActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					flurryActivity = CreateActivityElement(doc, ns, flurryActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
					dict.AppendChild(flurryActivity);
				}
			} else {
				flurryActivity = CreateActivityElement(doc, ns, flurryActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
				dict.AppendChild(flurryActivity);
			}
		}

		XmlElement appodealVideoPlayerActivity = FindElementWithAndroidName(activity, "name", ns, appodealVideoPlayerActivityName, dict);
		if (appodealVideoPlayerActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", appodealVideoPlayerActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					appodealVideoPlayerActivity = CreateActivityElement(doc, ns, appodealVideoPlayerActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
					dict.AppendChild(appodealVideoPlayerActivity);
				}
			} else {
				appodealVideoPlayerActivity = CreateActivityElement(doc, ns, appodealVideoPlayerActivityName, "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize", null);
				dict.AppendChild(appodealVideoPlayerActivity);
			}
		}

		XmlElement TJActionHandlerActivity = FindElementWithAndroidName(activity, "name", ns, TJActionHandlerActivityName, dict);
		if (TJActionHandlerActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", TJActionHandlerActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					TJActionHandlerActivity = CreateActivityElement(doc, ns, TJActionHandlerActivityName, "orientation|keyboardHidden|screenSize", null);
					dict.AppendChild(TJActionHandlerActivity);
				}
			} else {
				TJActionHandlerActivity = CreateActivityElement(doc, ns, TJActionHandlerActivityName, "orientation|keyboardHidden|screenSize", null);
				dict.AppendChild(TJActionHandlerActivity);
			}
		}

		XmlElement TJAdUnitActivity = FindElementWithAndroidName(activity, "name", ns, TJAdUnitActivityName, dict);
		if (TJAdUnitActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", TJAdUnitActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					TJAdUnitActivity = CreateHardwareActivity(doc, ns, TJAdUnitActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen", "true");
					dict.AppendChild(TJAdUnitActivity);
				}
			} else {
				TJAdUnitActivity = CreateHardwareActivity(doc, ns, TJAdUnitActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen", "true");
				dict.AppendChild(TJAdUnitActivity);
			}
		}

		XmlElement TJBrowserActivity = FindElementWithAndroidName(activity, "name", ns, TJBrowserActivityName, dict);
		if (TJBrowserActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", TJBrowserActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					TJBrowserActivity = CreateActivityElement(doc, ns, TJBrowserActivityName, "orientation|keyboardHidden|screenSize", null);
					dict.AppendChild(TJBrowserActivity);
				}
			} else {
				TJBrowserActivity = CreateActivityElement(doc, ns, TJBrowserActivityName, "orientation|keyboardHidden|screenSize", null);
				dict.AppendChild(TJBrowserActivity);
			}
		}

		XmlElement TJContentActivityActivity = FindElementWithAndroidName(activity, "name", ns, TJContentActivityActivityName, dict);
		if (TJContentActivityActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", TJContentActivityActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					TJContentActivityActivity = CreateHardwareActivity(doc, ns, TJContentActivityActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent.NoTitleBar", "true");
					dict.AppendChild(TJContentActivityActivity);
				}
			} else {
				TJContentActivityActivity = CreateHardwareActivity(doc, ns, TJContentActivityActivityName, "orientation|keyboardHidden|screenSize", "@android:style/Theme.Translucent.NoTitleBar", "true");
				dict.AppendChild(TJContentActivityActivity);
			}
		}

		XmlElement RevmobActivity = FindElementWithAndroidName(activity, "name", ns, RevmobActivityName, dict);
		if (RevmobActivity == null) {
			if(check) {
				bool val = EditorUtility.DisplayDialog("Appodeal Manifest Editor", RevmobActivityName + " is missing from AdnroidManifest.xml, would you like to add it?", "Yes", "No");
				if (val) { 
					RevmobActivity = CreateActivityElement(doc, ns, RevmobActivityName, "keyboardHidden|orientation", "@android:style/Theme.Translucent");
					dict.AppendChild(RevmobActivity);
				}
			} else {
				RevmobActivity = CreateActivityElement(doc, ns, RevmobActivityName, "keyboardHidden|orientation", "@android:style/Theme.Translucent");
				dict.AppendChild(RevmobActivity);
			}
		}

		doc.Save(fullPath);
		EditorUtility.DisplayDialog("Appodeal Manifest Editor", "Your AndroidManifest.xml is up to date", "Ok");
	}

	private static XmlNode FindChildNode(XmlNode parent, string name) {
		XmlNode curr = parent.FirstChild;
		while (curr != null) {
			if (curr.Name.Equals(name)) {
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}
	
	private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent) {
		var curr = parent.FirstChild;
		while (curr != null) {
			if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value) {
				return curr as XmlElement;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static XmlElement CreatePermission(XmlDocument doc, string ns, string name) {
		XmlElement permissionElement = doc.CreateElement(permission);
		permissionElement.SetAttribute("name", ns, name);
		return permissionElement;
	}

	private static XmlElement CreateMetaDataElement(XmlDocument doc, string ns, string name, string value) {
		XmlElement metaElement = doc.CreateElement(metadata);
		metaElement.SetAttribute("name", ns, name);
		if(value != null) {
			metaElement.SetAttribute("value", ns, value);
		}
		return metaElement;
	}

	private static XmlElement CreateExcludeHardwareActivityElement(XmlDocument doc, string ns, string name, string excludeFromRecents, string config, string theme, string hardware) {
		XmlElement activityElement = doc.CreateElement(activity);
		activityElement.SetAttribute("name", ns, name);
		if (config != null) { 
			activityElement.SetAttribute ("configChanges", ns, config);
		}
		if (theme != null) { 
			activityElement.SetAttribute ("theme", ns, theme);
		}
		if (hardware != null) { 
			activityElement.SetAttribute("hardwareAccelerated", ns, hardware);
		}
		if (excludeFromRecents != null) { 
			activityElement.SetAttribute("excludeFromRecents", ns, excludeFromRecents);
		}
		return activityElement;
	}

	private static XmlElement CreateHardwareActivity(XmlDocument doc, string ns, string name, string config, string theme, string hardware)	{
		XmlElement activityElement = doc.CreateElement(activity);
		activityElement.SetAttribute("name", ns, name);
		if (config != null) { 
			activityElement.SetAttribute ("configChanges", ns, config);
		}
		if (theme != null) { 
			activityElement.SetAttribute ("theme", ns, theme);
		}
		if (hardware != null) { 
			activityElement.SetAttribute("hardwareAccelerated", ns, hardware);
		}
		return activityElement;
	}

	private static XmlElement CreateActivityElement(XmlDocument doc, string ns, string name, string configChanges, string theme)	{
		XmlElement activityElement = doc.CreateElement(activity);
		activityElement.SetAttribute("name", ns, name);
		if (configChanges != null) { 
			activityElement.SetAttribute ("configChanges", ns, configChanges);
		}
		if (theme != null) { 
			activityElement.SetAttribute ("theme", ns, theme);
		}
		return activityElement;
	}
	
}