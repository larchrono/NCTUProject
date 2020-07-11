using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipLight : MonoBehaviour
{
    public RectTransform tipLight;
    public List<float> x_pos = new List<float>() { -202, -100, 0, 100, 202 };

    public void SetLightPosition(int index)
    {
        tipLight.anchoredPosition = new Vector2(x_pos[index], 0);
    }
}
