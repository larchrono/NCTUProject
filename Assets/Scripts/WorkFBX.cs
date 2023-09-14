using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFBX : MonoBehaviour
{
    public float scaleFactor = 1;
    void Start()
    {
        transform.GetChild(0).transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
