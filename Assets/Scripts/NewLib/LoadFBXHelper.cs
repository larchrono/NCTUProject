using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriLibCore.General;
using TriLibCore;
using System;
using System.Threading.Tasks;

public class LoadFBXHelper : SoraLib.SingletonMono<LoadFBXHelper>
{
    public TMPro.TextMeshProUGUI progress;
    public Transform ModelParent;
    public Action<GameObject> OnFileLoaded;
    public Action OnErrorCallback;

    [Header("設定")]
    
    public UnityEngine.Rendering.ShadowCastingMode shadowMode;


    #if UNITY_IOS
    float baseAngel = 180;
    #else
    float baseAngel = 0;
    #endif

    public static void StartLoadFBX(string url, Transform m_parent, TMPro.TextMeshProUGUI progress, Action<GameObject> callback, Action onError){
        instance.progress = progress;
        instance.ModelParent = m_parent;
        instance.OnFileLoaded = callback;
        instance.OnErrorCallback = onError;
        instance.LoadFBX(url);
    }

    void LoadFBX(string url)
    {
        progress.text = "0%";
        //await Task.Run(() => {     });

        var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
        AssetLoader.LoadModelFromFile(url, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions);
    }

    private void OnError(TriLibCore.IContextualizedError obj)
    {
        Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        OnErrorCallback?.Invoke();
    }

    private void OnProgress(TriLibCore.AssetLoaderContext assetLoaderContext, float prog)
    {
        float preview = Mathf.FloorToInt(prog * 100);
        progress.text = $"{preview}%";
    }

    private void OnMaterialsLoad(TriLibCore.AssetLoaderContext assetLoaderContext)
    {
        //Set parent , position, facing
        assetLoaderContext.RootGameObject.transform.parent = ModelParent;
        assetLoaderContext.RootGameObject.transform.localEulerAngles = new Vector3(0, baseAngel, 0);
        assetLoaderContext.RootGameObject.transform.localPosition = Vector3.zero;

        var rends = assetLoaderContext.RootGameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in rends)
        {
            item.shadowCastingMode = shadowMode;
        }

        Debug.Log("Materials loaded. Model fully loaded.");

        OnFileLoaded?.Invoke(assetLoaderContext.RootGameObject);
    }

    private void OnLoad(TriLibCore.AssetLoaderContext assetLoaderContext)
    {
        Debug.Log("Model loaded. Loading materials.");
    }
}
