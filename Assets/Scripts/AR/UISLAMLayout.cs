﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SaveScreen))]
public class UISLAMLayout : MonoBehaviour
{
    public Camera ARCamera;
    public Button BTNExit;
    public Button BTNShot;
    public Button BTNTracking;
    public Text TXTFacingAngle;
    public Button BTNDistance;
    public Text TXTDistance;
    public Transform ArtworkPool;
    public GameObject HelpCanvas;

    int displayType;
    GameObject currentStreetPhoto;
    SaveScreen saveScreen;

    enum DisplayType
    {
        TESTING = 0,
        PICTURE = 1,
        MODEL = 2,
    }

    float updateIndex = 0;
    float updateDelay = 0.33f;

    void CheckAngleAndButton(float angle){
        if(displayType == (int)DisplayType.TESTING)
        {
            //if(angle > 65 && angle < 75){
            if(angle > 15 && angle < 25){
                BTNTracking.interactable = true;
                TXTFacingAngle.color = Color.green;
            } else {
                BTNTracking.interactable = false;
                TXTFacingAngle.color = Color.red;
            }
        } 
        else if(displayType == (int)DisplayType.PICTURE)
        {
            if(angle < 10 && angle > 0){
                BTNTracking.interactable = true;
                TXTFacingAngle.color = Color.green;
            } else {
                BTNTracking.interactable = false;
                TXTFacingAngle.color = Color.red;
            }
        }
        else if(displayType == (int)DisplayType.MODEL)
        {
            if(angle < 50 && angle > 10){
                BTNTracking.interactable = true;
                TXTFacingAngle.color = Color.green;
            } else {
                BTNTracking.interactable = false;
                TXTFacingAngle.color = Color.red;
            }
        }
    }

    void Update(){
        if(updateIndex > updateDelay){
            float angle = GetFacingAngle();
            TXTFacingAngle.text = (90-angle).ToString("0");
            updateIndex = 0;
            CheckAngleAndButton(angle);

            TXTDistance.text = Vector3.Distance(ARCamera.transform.position, Vector3.zero).ToString("0.00");
        }
        updateIndex += Time.deltaTime;

        if(displayType == (int)DisplayType.PICTURE && currentStreetPhoto != null){
            Vector3 frontVec = (-ARCamera.transform.position).normalized * 2;
            Vector3 photoPos = ARCamera.transform.position + frontVec;
            currentStreetPhoto.transform.position = photoPos;
        }
    }

    void OnEnable() {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;

        HelpCanvas.SetActive(true);
        updateIndex = 0;
    }

    void OnDisable() {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        BTNExit.onClick.AddListener(DoExit);
        BTNShot.onClick.AddListener(DoShot);
        BTNTracking.onClick.AddListener(DoStartTracking);
        BTNDistance.onClick.AddListener(DoDistance);

        saveScreen = GetComponent<SaveScreen>();
    }

    public void SetupArtwork(GameObject obj){
        foreach (Transform item in ArtworkPool)
        {
            Destroy(item.gameObject);
        }

        displayType = (int)DisplayType.TESTING;

        if(obj)
            Instantiate(obj, ArtworkPool);
    }

    public void SetupOldPictureSLAM(Sprite photo){
        foreach (Transform item in ArtworkPool)
        {
            Destroy(item.gameObject);
        }

        displayType = (int)DisplayType.PICTURE;

        if(POIManager.instance.SLAM_Prefab != null) {
            currentStreetPhoto = Instantiate(POIManager.instance.SLAM_Prefab, ArtworkPool);
            IsPhoto comp = currentStreetPhoto.GetComponent<IsPhoto>();
            comp.SetPictureData(photo);
        }
    }

    public void SetupModelSLAM(Sprite photo){
        foreach (Transform item in ArtworkPool)
        {
            Destroy(item.gameObject);
        }

        displayType = (int)DisplayType.MODEL;

        if(POIManager.instance.SLAM_Prefab != null) {
            currentStreetPhoto = Instantiate(POIManager.instance.SLAM_Prefab, ArtworkPool);
            IsPhoto comp = currentStreetPhoto.GetComponent<IsPhoto>();
            comp.SetPictureData(photo);
        }
    }

    void DoExit(){
        UIARLayout.instance.StopSLAM();
    }

    void DoShot(){
        saveScreen.OnClickScreenCaptureButton();
    }

    void DoStartTracking(){
        VoidAR.GetInstance().startMarkerlessTracking();
        currentStreetPhoto.GetComponent<IsPhoto>().FadeingPicture();
    }

    void DoDistance(){
        if(TXTDistance.gameObject.activeSelf == false) TXTDistance.gameObject.SetActive(true);
        else TXTDistance.gameObject.SetActive(false);
    }

    public float GetFacingAngle(){
        return Mathf.Asin(-Mathf.Clamp(Input.acceleration.z, -1, 1)) *  Mathf.Rad2Deg;
    }

    
}
