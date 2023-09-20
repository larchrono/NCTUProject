using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseARFundation : MonoBehaviour
{
    public GameObject VoidCAM;
    public GameObject ARKitCAM;
    void Start()
    {
        if(PlatformManager.enableARFundation == EnableARFundation.OFF){
            ARKitCAM.SetActive(false);
        }
    }
}
