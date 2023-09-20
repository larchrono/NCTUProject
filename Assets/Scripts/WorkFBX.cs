using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFBX : MonoBehaviour
{
    public float scaleFactor = 1;
    public float scaleFactorIOS = 0.05f;
    public BoxCollider box;
    bool isInitialized = false;

    public void Initialize()
    {
        if(PlatformManager.enableARFundation == EnableARFundation.ON){
            scaleFactor = scaleFactorIOS;
        }

        if(isInitialized)
            return;

        transform.GetChild(0).transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        var mesh = gameObject.GetComponentInChildren<MeshRenderer>();

        if(mesh == null)
            return;

        var bounds = mesh.bounds;

        // In world-space!
        var size = bounds.size;
        var center = bounds.center;

        box.center = center;
        box.size = size;

        isInitialized = true;

        //if(mesh != null)
        //    mesh.gameObject.AddComponent<BoxCollider>();
    }
}
