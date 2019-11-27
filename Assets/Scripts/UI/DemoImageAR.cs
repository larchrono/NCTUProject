using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoImageAR : MonoBehaviour
{
    public Button DemoAR;
    void Start()
    {
        DemoAR.onClick.AddListener(DoDemoAR);
    }

    void DoDemoAR(){
        DemoAR.interactable = false;
        StartCoroutine(CheckAR3D());
    }

    IEnumerator CheckAR3D(){
        yield return null;

        #if UNITY_IOS
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            UIManager.instance.WarningCamera.gameObject.SetActive(true);
            while(true){
                yield return new WaitForSeconds(5);
            }
        }
        #endif

        UIARLayout.instance.StartARImage();

        yield return new WaitForSeconds(1);

        DemoAR.interactable = true;
    }
}
