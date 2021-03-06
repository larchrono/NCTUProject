﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoidARBehaviour))]
[RequireComponent(typeof(Camera))]
public class UIARLayout : MonoBehaviour
{
    public static UIARLayout instance;
    public UIImageLayout CVAR;
    public UISLAMLayout CVSLAM;
    public GameObject ImageTarget;
    
    Camera compCamera;
    VoidARBehaviour compAR;
    Transform TrackerSlam;
    
    void Awake(){
        if(instance == null)
            instance = this;
            
        compCamera = GetComponent<Camera>();
        compAR = GetComponent<VoidARBehaviour>();
        TrackerSlam = compAR.markerlessParent.transform;
    }

    void Start(){
        VisibleCamera(false);
    }

    public void StartARImage(){
        ClearTrackerObject();
        ImageTarget.SetActive(true);
        CVAR.gameObject.SetActive(true);
        CVSLAM.gameObject.SetActive(false);
        compAR.StartAR(VoidARBase.EMarkerType.Image, () => {
            VisibleCamera(true);
        });
    }

    public void StartSLAM(){
        CVSLAM.gameObject.SetActive(true);
        CVAR.gameObject.SetActive(false);
        compAR.StartAR(VoidARBase.EMarkerType.Markerless, () => {
            VisibleCamera(true);
        });
    }

    public void StopARImage(){
        CVAR.gameObject.SetActive(false);
        ClearImageTarget();
        VisibleCamera(false);
        compAR.EnableAR(false);
    }

    public void StopSLAM(){
        CVSLAM.gameObject.SetActive(false);
        ClearTrackerObject();
        VisibleCamera(false);
        compAR.EnableAR(false);
    }

    void VisibleCamera(bool visible){
        if(visible)
            compCamera.depth = 5;
        else
            compCamera.depth = -5;
    }

    void ClearImageTarget(){
        ImageTarget.SetActive(false);
    }

    void ClearTrackerObject(){
        foreach (Transform item in TrackerSlam)
        {
            Destroy(item.gameObject);
        }
    }
}
