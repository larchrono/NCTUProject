using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeLayout : CanvasGroupExtend
{
    public WorkPanelLayout WorkPanel;

    public Button AR;
    public Button HowTo;
    public Button About;
    
    void Start()
    {
        AR.onClick.AddListener(BTNAR);
        HowTo.onClick.AddListener(BTNHowTo);
        About.onClick.AddListener(BTNAbout);

        OpenSelfImmediate();
    }

    void BTNAR(){
        SwitchToWorkPanel(0);
    }
    void BTNHowTo(){
        SwitchToWorkPanel(1);
    }
    void BTNAbout(){
        SwitchToWorkPanel(2);
    }

    void SwitchToWorkPanel(int index){
        CloseSelf();
        WorkPanel.OpenWorPanel(index);
    }  
}
