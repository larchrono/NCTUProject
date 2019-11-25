using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasBehaviour : CanvasGroupExtend
{
    void Start()
    {
        CloseSelfImmediate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
