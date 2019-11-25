using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIMarker : MonoBehaviour
{
    public POIData data;
    public Action<POIMarker> OnClickPOI;
}
