using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoxLayout : CanvasGroupExtend
{
    public static InfoBoxLayout instance;

    //Bubble Layout
    public Button BTNClose;
    public Text Title;
    public Image MapIcon;
    public Text GoadRange;
    public Button Go3D;
    public Button Open3D;
    public Text AlertMessage;
    
    //Content Layout
    public ContentHeightController contentHeightController;
    public Image ContentPhoto;
    public Text ContentText;
    public POIData currentData;

    //Public Parameter
    public float MinimunDistanceForLook = 20;

    OnlineMapsLocationService locationService;
    float distanceBetweenPOI = 0;

    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        BTNClose.onClick.AddListener(DoCloseWindow);
        Open3D.onClick.AddListener(OnOpenAR);

        locationService = OnlineMapsLocationService.instance;
        if (locationService != null)
            locationService.OnLocationChanged += OnDistanceChange;

        CloseSelfImmediate();
    }

    void DoCloseWindow(){
        CloseSelf();
    }

    void OnOpenAR(){
        StartCoroutine(CheckAR3D());
    }

    IEnumerator CheckAR3D(){
        yield return null;

        #if UNITY_IOS
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            UIManager.instance.WarningCamera.gameObject.SetActive(true);
            while(true){
                yield return new WaitForSeconds(5);
            }
        }
        #endif

        UIManager.instance.AR3DPanel.SetupOldPictureSLAM(currentData.oldPicture);
        UIManager.instance.AR3DPanel.gameObject.SetActive(true);
    }

    public void OpenInfoBoxWithPOI(POIData data){
        currentData = data;

        Title.text = data.POI_Name;
        MapIcon.sprite = data.ColorMarker == null? MapIcon.sprite : data.ColorMarker;
        ContentPhoto.sprite = data.nowPicture;
        ContentText.text = data.description;

        if(locationService != null)
            OnDistanceChange(locationService.position);
        
        if(distanceBetweenPOI < MinimunDistanceForLook)
            AlertMessage.gameObject.SetActive(false);
        else
            AlertMessage.gameObject.SetActive(true);

        contentHeightController.ResizeContent();

        OpenSelf();
    }

    void OnDistanceChange(Vector2 userPoint){
        if(currentData == null)
            return;
        
        Vector2 markerCoordinates = new Vector2((float)currentData.Longitude_User, (float)currentData.Latitude_User);
        Vector2 userCoordinares = userPoint;

        // Calculate the distance in km between locations.
        distanceBetweenPOI = OnlineMapsUtils.DistanceBetweenPoints(userCoordinares, markerCoordinates).magnitude * 1000;

        GoadRange.text = "距離 " + distanceBetweenPOI.ToString("#.#") + " m";
    }
}
