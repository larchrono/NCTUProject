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
        rootDict.SetString("NSPhotoLibraryUsageDescription", "請啟用設定來開啟拍照功能");
	    rootDict.SetString("NSPhotoLibraryAddUsageDescription", "請啟用設定來開啟拍照功能");
        rootDict.SetString("NSLocationAlwaysAndWhenInUseUsageDescription", "請啟用定位資訊來使用AR導覽");
        rootDict.SetString("NSLocationWhenInUseUsageDescription", "請啟用定位資訊來使用AR導覽");
        rootDict.SetString("NSCameraUsageDescription", "請啟用相機來使用AR相關功能");
        
        var rootDicVal = rootDict.values;
        rootDicVal.Remove("UIApplicationExitsOnSuspend");

        // Write plist
        File.WriteAllText(plistPath, plist.WriteToString());
    }
#endif
}