using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoResizer : MonoBehaviour
{
    public float HeightRate = 0.3715f;
    public float IconRateRefScreen = 0.2f;

    public float portraitRate = 0.3f;
    public float portraitX = 30;
    public float portraitY_R = 0.02f;
    public float landRate = 0.2f;
    public float landX = 15;
    public float landY_R = 0.03f;


    public Image Logo;

    public Text debugText;

    float lastW = 0;

    void Start()
    {
        Logo = GetComponent<Image>();
    }

    public void ChangeIconRate(float f){
        IconRateRefScreen = f;

        if(debugText != null){
            debugText.text = f.ToString("0.00");
        }
    }

    void Update()
    {
        float w = Screen.width;
        float h = Screen.height;

        if(lastW == Screen.width)
            return;

        if(h > w){
            Portrait(w, h);
        } else {
            Landscape(w, h);
        }
    }

    void Portrait(float w, float h){
        lastW = Screen.width;
        float iconW = w * portraitRate;
        float iconH = iconW * HeightRate;
        Vector2 size = new Vector2(iconW, iconH);
        Logo.rectTransform.sizeDelta = size;

        Logo.rectTransform.anchoredPosition = new Vector2(portraitX, Screen.height * portraitY_R);

    }

    void Landscape(float w, float h){
        lastW = Screen.width;
        float iconW = w * landRate;
        float iconH = iconW * HeightRate;
        Vector2 size = new Vector2(iconW, iconH);
        Logo.rectTransform.sizeDelta = size;

        Logo.rectTransform.anchoredPosition = new Vector2(landX, Screen.height * landY_R);

    }
}
