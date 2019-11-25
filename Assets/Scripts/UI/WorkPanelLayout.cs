using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorkPanelLayout : CanvasGroupExtend
{
    public Button TabPlan;
    public Button TabAR;
    public Button TabAbout;
    public CanvasBehaviour CVPlan;
    public CanvasBehaviour CVAR;
    public CanvasBehaviour CVAbout;

    List<Button> Tabs;
    List<CanvasBehaviour> CVs;
    void Awake(){
        Tabs = new List<Button>();
        Tabs.Add(TabPlan);
        Tabs.Add(TabAR);
        Tabs.Add(TabAbout);
        CVs = new List<CanvasBehaviour>();
        CVs.Add(CVPlan);
        CVs.Add(CVAR);
        CVs.Add(CVAbout);
    }
    void Start()
    {
        TabPlan.onClick.AddListener(SwitchToPlan);
        TabAR.onClick.AddListener(SwitchToAR);
        TabAbout.onClick.AddListener(SwitchToAbout);

        CloseSelfImmediate();
    }

    public void OpenWorPanel(int index){
        OpenSelf();

        if(index == 0)
            SwitchToPlan();
        else if (index == 1)
            SwitchToAR();
        else if (index == 2)
            SwitchToAbout();

        EventSystem.current.SetSelectedGameObject(null);
    }

    void SwitchTab(int index){
        for (int i = 0; i < Tabs.Count; i++)
        {
            if(i == index){
                Tabs[i].interactable = false;
                CVs[i].OpenSelf();
            }
            else {
                Tabs[i].interactable = true;
                CVs[i].CloseSelf();
            }
        }
    }

    public void SwitchToPlan(){
        SwitchTab(0);
    }

    public void SwitchToAR(){
        SwitchTab(1);
    }

    public void SwitchToAbout(){
        SwitchTab(2);
    }
}
