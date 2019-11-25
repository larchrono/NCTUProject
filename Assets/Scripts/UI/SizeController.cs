using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    public float constHeight;
    public List<RectTransform> items;

    RectTransform rectTransform;

    IEnumerator Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //yield return new WaitForSeconds(0.1f);
        yield return null;
        float total = 0;
        foreach (var item in items)
        {
            total += item.sizeDelta.y;
        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, total + constHeight);
    }

    public void ResizeContent(){
        StartCoroutine(Start());
    }
}
