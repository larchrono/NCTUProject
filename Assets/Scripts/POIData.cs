using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIData : MonoBehaviour
{
    public string POI_Name;
    public string artist;
    public string format;
    public double Latitude_User;
    public double Longitude_User;
    public double Latitude_Goal;
    public double Longitude_Goal;
    public string modelName;
    public string fullModelPath;
    public string previewName;
    public Sprite artpreview;
    public Sprite ColorMarker;
    public string YoutubeURL;
    [TextArea(5,20)] public string description;

    OnlineMapsMarker3D dynamicMarker;
    int defaultZoom = 18;

    void Start()
    {
        defaultZoom = OnlineMaps.instance.zoom;

        // Add OnClick events to dynamic markers
        dynamicMarker = OnlineMapsMarker3DManager.CreateItem(Longitude_User, Latitude_User, POIManager.instance.POI_Prefab);
        dynamicMarker.instance.name = string.Format("Marker_{0}", POI_Name);
        //dynamicMarker.OnClick += OnMarkerClick;
        //dynamicMarker.label = POI_Name;
        //dynamicMarker.SetDraggable();
        SpriteRenderer render = dynamicMarker.instance.GetComponentInChildren<SpriteRenderer>();
        render.sprite = ColorMarker == null ? render.sprite : ColorMarker;

        POIMarker markerPOI = dynamicMarker.instance.AddComponent<POIMarker>();
        markerPOI.data = this;
        markerPOI.OnClickPOI += OnMarkerClick;

        //Subscribe to zoom change
        OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
    }

    private void OnMarkerClick(POIMarker markerPOI)
    {
        InfoBoxLayout.instance.OpenInfoBoxWithPOI(this);
    }

    private void OnChangeZoom()
    {
        //Example of scaling object
        //int zoom = OnlineMaps.instance.zoom;

        //float s = 10f / (2 << (zoom - 5));

        float originalScale = 1 << defaultZoom;
        float currentScale = 1 << OnlineMaps.instance.zoom;

        ZoomHelper helper = dynamicMarker.instance.GetComponent<ZoomHelper>();
        helper.SetGizmoRange(currentScale / originalScale);
    }

    private void OnValidate() {
        gameObject.name = string.Format("POI_{0}", POI_Name);
    }

    public void ModelSetter(Sprite spt){
        //artmodel = spt;
    }

    public void ArtPreviewSetter(Sprite spt){
        artpreview = spt;
    }
}
