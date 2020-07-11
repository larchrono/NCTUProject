using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeLayout : CanvasGroupExtend
{
    public Button Skip;
    public Button Close;
    void Start()
    {
        OpenSelfImmediate();

        Skip.onClick.AddListener(ToHome);
        Close.onClick.AddListener(ToHome);
    }

    void ToHome(){
        CloseSelf();
    }
}
