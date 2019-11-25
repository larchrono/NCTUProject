using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomHelper : MonoBehaviour
{
    SphereCollider eventCollider;
    public Transform GizmoColliRange;
    float defaultSize;
    void Awake()
    {
        eventCollider = GetComponent<SphereCollider>();
        defaultSize = eventCollider.radius;
    }

    public void SetGizmoRange(float src){
        eventCollider.radius = defaultSize * src;

        if(GizmoColliRange)
            GizmoColliRange.localScale = new Vector3(defaultSize * src * 2, defaultSize * src * 2, defaultSize * src * 2);
    }
}
