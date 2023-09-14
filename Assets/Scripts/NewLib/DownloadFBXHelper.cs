using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadFBXHelper : SoraLib.SingletonMono<DownloadFBXHelper>
{
    public TMPro.TextMeshProUGUI progress;
    public Action<string> OnFileDownloaded;
    public static void StartDownloadFBX(string url, TMPro.TextMeshProUGUI progress, Action<string> callback){
        instance.progress = progress;
        instance.OnFileDownloaded = callback;
        instance.StartCoroutine(instance.LoadVideoFromThisURL(url));
    }

    IEnumerator LoadVideoFromThisURL(string _url)
    {
        progress.text = "0%";
        
        UnityWebRequest _modelRequest = UnityWebRequest.Get (_url);

        var asyncOp = _modelRequest.SendWebRequest();

        while(!asyncOp.isDone){
            //OnProgressUpdate?.Invoke(asyncOp.progress);

            float preview = Mathf.FloorToInt(asyncOp.progress * 100);
            progress.text = $"{preview}%";
            yield return null;
        }

        if (_modelRequest.isDone == false || _modelRequest.error != null)
        {
            Debug.Log ("Request = " + _modelRequest.error );
        }

        Debug.Log ("FBX Download Done - " + _modelRequest.isDone);

        byte[] _fbxBytes = _modelRequest.downloadHandler.data;

        string _pathToFile = Path.Combine (Application.persistentDataPath, "temp_fbx.fbx");
        File.WriteAllBytes (_pathToFile, _fbxBytes);
        
        Debug.Log (_pathToFile);

        OnFileDownloaded?.Invoke(_pathToFile);

        yield return null;
    }
}
