using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFBX : MonoBehaviour
{
    public float scaleFactor = 1;
    public float scaleFactorIOS = 0.05f;
    public BoxCollider box;

    void Start()
    {
        #if UNITY_IOS
        scaleFactor = scaleFactorIOS;
        #endif
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

        //if(mesh != null)
        //    mesh.gameObject.AddComponent<BoxCollider>();
    }
}
