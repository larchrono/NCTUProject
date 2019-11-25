using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenBlink : MonoBehaviour
{
    void Start()
    {
        Image self = GetComponent<Image>();
        self.DOFade(0,0.5f).OnComplete(()=>{
            Destroy(gameObject, 1.0f);
        });
    }
}
