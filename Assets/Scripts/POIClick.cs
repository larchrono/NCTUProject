using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class POIClick : MonoBehaviour , IPointerDownHandler
{
    POIMarker marker;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(marker == null)
            marker = GetComponentInParent<POIMarker>();

        marker.OnClickPOI?.Invoke(marker);
    }
}
