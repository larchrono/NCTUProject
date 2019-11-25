using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("POI")){
            var poi = other.GetComponent<POIMarker>().data;

            InfoBoxLayout.instance.OpenInfoBoxWithPOI(poi);

            //Shake Device
            Handheld.Vibrate();

            if(other.gameObject.transform.childCount == 0)
                return;

            Transform child = other.gameObject.transform.GetChild(0);
            if(child != null)
                child.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("POI"))
        {
            //arrivePOI = null;
            var poi = other.GetComponent<POIMarker>().data;

            if(other.gameObject.transform.childCount == 0)
                return;

            Transform child = other.gameObject.transform.GetChild(0);
            if(child != null)
                child.GetComponent<Renderer>().material.color = Color.white;
            
        }
    }
}