using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIData : MonoBehaviour
{
    public string POI_Name;
    public double Latitude_User;
    public double Longitude_User;
    public double Latitude_Goal;
    public double Longitude_Goal;
    public string oldPictureName;
    public string nowPictureName;
    public Sprite oldPicture;
    public Sprite nowPicture;
    public Sprite ColorMarker; 
    [TextArea(5,20)] public string description;

    
    void Start()
    {
        OnlineMaps map = OnlineMaps.instance;

        // Add OnClick events to dynamic markers
        OnlineMapsMarker3D dynamicMarker = OnlineMapsMarker3DManager.CreateItem(Longitude_User, Latitude_User, POIManager.instance.POI_Prefab);
        //dynamicMarker.OnClick += OnMarkerClick;
        dynamicMarker.label = POI_Name;
        dynamicMarker.instance.name = string.Format("Marker_{0}", POI_Name);
        //dynamicMarker.SetDraggable();

        POIMarker markerPOI = dynamicMarker.instance.AddComponent<POIMarker>();
        markerPOI.data = this;
        markerPOI.OnClickPOI += OnMarkerClick;
    }

    private void OnMarkerClick(POIMarker markerPOI)
    {
        InfoBoxLayout.instance.OpenInfoBoxWithPOI(this);
    }

    private void OnValidate() {
        gameObject.name = string.Format("POI_{0}", POI_Name);
    }
}
