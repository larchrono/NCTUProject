using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARHelper : SoraLib.SingletonMono<ARHelper>
{
    public Camera arCamera;
    public Vector3 GetNewARPosition(){
        return arCamera.transform.position + arCamera.transform.forward * 0.33f;
    }
}
