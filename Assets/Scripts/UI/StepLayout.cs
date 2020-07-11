using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepLayout : CanvasGroupExtend
{
    public bool isInitOpen;
    public CanvasGroup nextPage;
    public int toLight;
    Button clickPage;

    void Awake(){
        clickPage = GetComponent<Button>();
    }

    void Start()
    {
        clickPage.onClick.AddListener(PageClick);
        if(isInitOpen){
            OpenSelfImmediate();
        }
        else {
            CloseSelfImmediate();
        }
    }

    void PageClick(){
        if(nextPage == null)
            return;
            
        UIManager.instance.tipLight.SetLightPosition(toLight);
        CloseSelfImmediate();
        OpenPanelImmediate(nextPage);
    }
}
