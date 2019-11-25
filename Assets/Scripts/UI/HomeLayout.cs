using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeLayout : CanvasGroupExtend
{
    public WorkPanelLayout WorkPanle;

    public Button Plan;
    public Button AR;
    public Button About;
    
    void Start()
    {
        Plan.onClick.AddListener(BTNPlan);
        AR.onClick.AddListener(BTNAR);
        About.onClick.AddListener(BTNAbout);

        OpenSelfImmediate();
    }

    void BTNPlan(){
        SwitchToWorkPanel(0);
    }

    void BTNAR(){
        SwitchToWorkPanel(1);
    }

    void BTNAbout(){
        SwitchToWorkPanel(2);
    }

    void SwitchToWorkPanel(int index){
        CloseSelf();
        WorkPanle.OpenWorPanel(index);
    }  
}
