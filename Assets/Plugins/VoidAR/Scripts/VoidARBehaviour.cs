﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;

#if !UNITY_IOS
public class VoidARBehaviour : VoidARBase
{
    public bool isReady;
    
    protected override void Awake()
    {
        if(!isReady)
            return;

        //自定义对焦：CameraFocusMode的值为-1时，为infinity模式，关闭对焦，值为2时为continuous-video模式，持续对焦；
        //设置后将修改SDK内部自动默认值
        //CameraFocusMode = -1;
        base.Awake();
#if UNITY_STANDALONE_WIN
        String currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        String dllPath = Application.dataPath + Path.DirectorySeparatorChar + "Plugins";
        if (currentPath.Contains(dllPath) == false)
        {
            Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
        }
#endif

#if UNITY_ANDROID
        //SDK内部已经在打开设备Camera时处理了Camera权限，在录屏时处理了扩展卡和音频权限，可使用下面接口申请其他权限
        //初始化时申请单个其他权限
        /*requestAndroidPermission("android.permission.WRITE_EXTERNAL_STORAGE", () =>
        {
            //成功回调
        });*/

        //初始化时申请多个其他权限
        /*
        string[] requestPermissionNames = { "android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.RECORD_AUDIO" };
        sdkBase.requestAndroidPermissions(requestPermissionNames, () =>
        {
             //全部成功后回调
        });*/
#endif
    }

    public void StartAR(VoidARBase.EMarkerType type, Action callback){
        isReady = true;
        markerType = type;
        EnableAR(false);
        StartCoroutine(DoReawake(callback));
    }

    IEnumerator DoReawake(Action callback){
        yield return null;
        yield return new WaitForSeconds(0.1f);
        Awake();
        callback?.Invoke();
    }

    public void ReAwake(){
        base.Awake();
    }
}

#else
public class VoidARBehaviour : MonoBehaviour
{
    
}
#endif