using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeightController : MonoBehaviour
{
    public RectTransform lastChildObject;
    public float constSpace = 50;
    RectTransform rectTransform;
    IEnumerator Start()
    {
        rectTransform = GetComponent<RectTransform>();

        yield return null;
        float constHeight = Mathf.Abs(lastChildObject.anchoredPosition.y);
        float constSize = lastChildObject.sizeDelta.y;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, constSize + constHeight + constSpace);
    }

    public void ResizeContent(){
        StartCoroutine(Start());
    }
}
