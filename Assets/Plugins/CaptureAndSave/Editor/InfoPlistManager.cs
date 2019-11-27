using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;
using System.Collections.Generic;

public class InfoPlistManager : MonoBehaviour
{

#if UNITY_IOS
    [PostProcessBuild]
    static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        // Read plist
        var plistPath = Path.Combine(path, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        // Update value
        PlistElementDict rootDict = plist.root;
        rootDict.SetString("NSPhotoLibraryUsageDescription", "將用於拍照存檔功能");
	    rootDict.SetString("NSPhotoLibraryAddUsageDescription", "將用於拍照存檔功能");
        rootDict.SetString("NSLocationAlwaysAndWhenInUseUsageDescription", "將用於地圖定位，可引導您至AR觀看點");
        rootDict.SetString("NSLocationWhenInUseUsageDescription", "將用於地圖定位，可引導您至AR觀看點");
        rootDict.SetString("NSCameraUsageDescription", "必須啟用相機才能觀看擴增實境(AR)內容");
        
        var rootDicVal = rootDict.values;
        rootDicVal.Remove("UIApplicationExitsOnSuspend");

        // Write plist
        File.WriteAllText(plistPath, plist.WriteToString());
    }
#endif
}