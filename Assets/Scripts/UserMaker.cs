using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMaker : MonoBehaviour
{
    public float scaleSize = 30;
    public GameObject prefab;

    private OnlineMapsMarker3D locationMarker;
    private Transform markerSprite;

    private Vector2 newPosition;
    private float newCompass;
    float yVelocity = 0.0f;
    float smooth = 1.0f;
    int defaultZoom = 18;

    private void OnValidate() {
        if(locationMarker != null) locationMarker.scale = scaleSize;
    }

    private void Start()
    {
        defaultZoom = OnlineMaps.instance.zoom;
        // Lock map zoom range
        OnlineMaps.instance.zoomRange = new OnlineMapsRange(2, 19.5f);

        // Gets the current 3D control.
        OnlineMapsControlBase3D control = OnlineMapsControlBase3D.instance;
        if (control == null)
        {
            Debug.LogError("You must use the 3D control (Texture or Tileset).");
            return;
        }

        //Create a marker to show the current GPS coordinates.
        //Instead of "null", you can specify the texture desired marker.
        locationMarker = OnlineMapsMarker3DManager.CreateItem(Vector2.zero, prefab);

        //Hide handle until the coordinates are not received.
        locationMarker.enabled = false;

        locationMarker.scale = scaleSize;
        locationMarker.instance.name = "Player";
        markerSprite = locationMarker.instance.transform.GetChild(0);

        // Gets Location Service Component.
        OnlineMapsLocationService ls = OnlineMapsLocationService.instance;

        if (ls == null)
        {
            Debug.LogError(
                "Location Service not found.\nAdd Location Service Component (Component / Infinity Code / Online Maps / Plugins / Location Service).");
            return;
        }

        //Subscribe to the GPS coordinates change
        ls.OnLocationChanged += OnLocationChanged;
        ls.OnCompassChanged += OnCompassChanged;

        //Subscribe to zoom change
        OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
    }

    private void Update() {

        //Smoothly Update rotation
        if(Mathf.Abs(locationMarker.transform.eulerAngles.y - newCompass) >= 5){
            float newAngle = Mathf.SmoothDampAngle(locationMarker.transform.eulerAngles.y, newCompass, ref yVelocity, 0.25f);
            locationMarker.transform.rotation = Quaternion.Euler(0, newAngle, 0);
        }

        //Smoothly move pointer to updated position
        locationMarker.position = Vector2.Lerp(locationMarker.position, newPosition, 0.5f);
    }

    private void OnChangeZoom()
    {
        float originalScale = 1 << defaultZoom;
        float currentScale = 1 << OnlineMaps.instance.zoom;

        locationMarker.scale = currentScale / originalScale;
        
        markerSprite.localScale = Vector3.one / (currentScale / originalScale);
        
        //OnlineMapsLocationService.instance.StartCoroutine(DelayScale());
    }

    private void OnCompassChanged(float f)
    {
        newCompass = f * 360;

        //Set marker rotation
        //Transform markerTransform = locationMarker.transform;
        //if (markerTransform != null) markerTransform.rotation = Quaternion.Euler(0, f * 360, 0);
    }

    //This event occurs at each change of GPS coordinates
    private void OnLocationChanged(Vector2 position)
    {
        //Change the position of the marker to GPS coordinates
        //locationMarker.position = position;
        if(Vector2.Distance(Vector2.zero, locationMarker.position) == 0)
            locationMarker.position = position;

        newPosition = position;

        //If the marker is hidden, show it
        if (!locationMarker.enabled) locationMarker.enabled = true;
    }

    IEnumerator DelayScale(){
        yield return null;
        yield return null;
        markerSprite.localScale = Vector3.one / locationMarker.instance.transform.localScale.x;
    }
}
