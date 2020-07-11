using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WelcomeLayout : CanvasGroupExtend
{
    public Button Skip;
    public Button Close;
    public RectTransform pageSlide;
    public float slideTime = 0.4f;
    void Start()
    {
        OpenSelfImmediate();

        Skip.onClick.AddListener(ToHome);
        Close.onClick.AddListener(ToHome);
    }

    void ToHome(){
        CloseSelf();
    }

    public void ToStep(int step){
        pageSlide.DOAnchorMin(new Vector2(0 - step, 0), slideTime);
        pageSlide.DOAnchorMax(new Vector2(1 - step, 1), slideTime);
    }
}
