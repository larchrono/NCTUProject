#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
class AutoIncreaseBundle : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Auto Increase bundleVersionCode...");
        PlayerSettings.Android.bundleVersionCode++;
    }
}
#endif
