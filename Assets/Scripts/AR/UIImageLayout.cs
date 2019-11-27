using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageLayout : MonoBehaviour
{
    public Button BTNExit;

    void OnEnable() {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void OnDisable() {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        BTNExit.onClick.AddListener(DoExit);
    }

    void DoExit(){
        UIARLayout.instance.StopARImage();
    }
}
