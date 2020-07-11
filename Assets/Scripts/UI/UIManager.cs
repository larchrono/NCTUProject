using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform WarningLocation;
    public RectTransform WarningCamera;
    public TipLight tipLight;

    private void Awake() {
        instance = this;
    }
}
