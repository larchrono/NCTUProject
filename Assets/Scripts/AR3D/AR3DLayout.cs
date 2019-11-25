// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// [RequireComponent(typeof(SaveScreen))]
// public class AR3DLayout : MonoBehaviour
// {
//     public static AR3DLayout instance;

//     public Camera ARCamera;
//     public Button BTNExit;
//     public Button BTNShot;
//     public Button BTNTracking;
//     public Text TXTFacingAngle;
//     public Button BTNDistance;
//     public Text TXTDistance;
//     public Transform ArtworkPool;

//     public GameObject HelpCanvas;

//     int displayType;
//     GameObject currentStreetPhoto;

//     SaveScreen saveScreen;

//     float updateIndex = 0;
//     float updateDelay = 0.33f;

//     void Awake(){
//         instance = this;
//     }

//     void CheckAngleAndButton(float angle){
//         if(displayType == 0)
//         {
//             //if(angle > 65 && angle < 75){
//             if(angle > 15 && angle < 25){
//                 BTNTracking.interactable = true;
//                 TXTFacingAngle.color = Color.green;
//             } else {
//                 BTNTracking.interactable = false;
//                 TXTFacingAngle.color = Color.red;
//             }
//         } 
//         else if(displayType == 1)
//         {
//             if(angle < 10 && angle > 0){
//                 BTNTracking.interactable = true;
//                 TXTFacingAngle.color = Color.green;
//             } else {
//                 BTNTracking.interactable = false;
//                 TXTFacingAngle.color = Color.red;
//             }
//         }
//     }

//     void Update(){
//         if(updateIndex > updateDelay){
//             float angle = GetFacingAngle();
//             TXTFacingAngle.text = (90-angle).ToString("0");
//             updateIndex = 0;
//             CheckAngleAndButton(angle);

//             TXTDistance.text = Vector3.Distance(ARCamera.transform.position, Vector3.zero).ToString("0.00");
//         }
//         updateIndex += Time.deltaTime;

//         if(displayType == 1 && currentStreetPhoto != null){
//             Vector3 frontVec = (-ARCamera.transform.position).normalized * 2;
//             Vector3 photoPos = ARCamera.transform.position + frontVec;
//             currentStreetPhoto.transform.position = photoPos;
//         }
//     }

//     void OnEnable() {
//         Screen.autorotateToLandscapeLeft = true;
//         Screen.autorotateToLandscapeRight = true;
//         Screen.orientation = ScreenOrientation.AutoRotation;

//         HelpCanvas.SetActive(true);
//         updateIndex = 0;
//     }

//     void OnDisable() {
//         Screen.autorotateToLandscapeLeft = false;
//         Screen.autorotateToLandscapeRight = false;
//         Screen.orientation = ScreenOrientation.Portrait;
//     }

//     void Start()
//     {
//         BTNExit.onClick.AddListener(DoExit);
//         BTNShot.onClick.AddListener(DoShot);
//         BTNTracking.onClick.AddListener(DoStartTracking);
//         BTNDistance.onClick.AddListener(DoDistance);

//         saveScreen = GetComponent<SaveScreen>();
//     }

//     public void SetupArtwork(GameObject obj){
//         foreach (Transform item in ArtworkPool)
//         {
//             Destroy(item.gameObject);
//         }

//         displayType = 0;

//         if(obj)
//             Instantiate(obj, ArtworkPool);
//     }

//     public void SetupStreetPhoto(GameObject obj){
//         foreach (Transform item in ArtworkPool)
//         {
//             Destroy(item.gameObject);
//         }

//         displayType = 1;

//         if(obj)
//             currentStreetPhoto = Instantiate(obj, ArtworkPool);
//     }

//     void DoExit(){
//         gameObject.SetActive(false);
//     }

//     void DoShot(){
//         saveScreen.OnClickScreenCaptureButton();
//     }

//     void DoStartTracking(){
//         VoidAR.GetInstance().startMarkerlessTracking();
//     }

//     void DoDistance(){
//         if(TXTDistance.gameObject.activeSelf == false) TXTDistance.gameObject.SetActive(true);
//         else TXTDistance.gameObject.SetActive(false);
//     }

//     public float GetFacingAngle(){
//         return Mathf.Asin(-Mathf.Clamp(Input.acceleration.z, -1, 1)) *  Mathf.Rad2Deg;
//     }

    
// }
