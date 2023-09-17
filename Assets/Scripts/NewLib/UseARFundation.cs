using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseARFundation : MonoBehaviour
{
    public GameObject VoidCAM;
    public GameObject ARKitCAM;
    void Start()
    {
        #if !UNITY_IOS
            ARKitCAM.SetActive(false);
        #else

        #endif
    }
}
