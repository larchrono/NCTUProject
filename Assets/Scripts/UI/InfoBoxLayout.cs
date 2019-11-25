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
    //public NavingLayout PanelNaving;
    
    //Content Layout
    public ContentHeightController contentHeightController;
    public Image ContentPhoto;
    public Text ContentText;
    public POIData currentData;

    OnlineMapsLocationService locationService;

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
        
    }

    public void OpenInfoBoxWithPOI(POIData data){
        currentData = data;

        Title.text = data.POI_Name;
        MapIcon.sprite = data.ColorMarker == null? MapIcon.sprite : data.ColorMarker;
        ContentPhoto.sprite = data.nowPicture;
        ContentText.text = data.description;

        if(locationService != null)
            OnDistanceChange(locationService.position);

        contentHeightController.ResizeContent();

        OpenSelf();
    }

    void OnDistanceChange(Vector2 userPoint){
        if(currentData == null)
            return;
        
        Vector2 markerCoordinates = new Vector2((float)currentData.Longitude_User, (float)currentData.Latitude_User);
        Vector2 userCoordinares = userPoint;

        // Calculate the distance in km between locations.
        float distance = OnlineMapsUtils.DistanceBetweenPoints(userCoordinares, markerCoordinates).magnitude * 1000;

        GoadRange.text = "距離 " + distance.ToString("#.#") + " m";
    }
}
