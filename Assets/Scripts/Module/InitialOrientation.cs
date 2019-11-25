using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }
}
