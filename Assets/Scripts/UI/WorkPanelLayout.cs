using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class WorkPanelLayout : CanvasGroupExtend
{
    public HomeLayout Home;
    public Button TabAR;
    public Button TabHowTo;
    public Button TabAbout;
    public Button BTNBackToHome;
    
    public CanvasBehaviour CVAR;
    public CanvasBehaviour CVHowTo;
    public CanvasBehaviour CVAbout;

    List<Button> Tabs;
    List<CanvasBehaviour> CVs;

    void Awake(){
        Tabs = new List<Button>();
        Tabs.Add(TabAR);
        Tabs.Add(TabHowTo);
        Tabs.Add(TabAbout);
        CVs = new List<CanvasBehaviour>();
        CVs.Add(CVAR);
        CVs.Add(CVHowTo);
        CVs.Add(CVAbout);
    }
    void Start()
    {
        TabHowTo.onClick.AddListener(SwitchToHowTo);
        TabAR.onClick.AddListener(SwitchToAR);
        TabAbout.onClick.AddListener(SwitchToAbout);
        BTNBackToHome.onClick.AddListener(BackToHome);

        CloseSelfImmediate();
    }

    public void OpenWorPanel(int index){
        OpenSelfImmediate();

        SwitchTab(index);

        EventSystem.current.SetSelectedGameObject(null);
    }

    void SwitchTab(int index){
        for (int i = 0; i < Tabs.Count; i++)
        {
            if(i == index){
                Tabs[i].interactable = false;
                CVs[i].OpenSelfImmediate();
            }
            else {
                Tabs[i].interactable = true;
                CVs[i].CloseSelfImmediate();
            }
        }
    }

    public void SwitchToAR(){
        SwitchTab(0);
    }

    public void SwitchToHowTo(){
        SwitchTab(1);
    }

    public void SwitchToAbout(){
        SwitchTab(2);
    }

    public void BackToHome(){
        Home.OpenSelf();
        //DoCloseSelf();
    }

    async void DoCloseSelf(){
        await Task.Delay(1000);
        CloseSelfImmediate();
    }
}
