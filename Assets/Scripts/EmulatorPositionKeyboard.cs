using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorPositionKeyboard : MonoBehaviour
{
    OnlineMapsLocationService ls;
    public float moveSpeed = 1;
    void Start()
    {
        ls = OnlineMapsLocationService.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (ls == null)
            return;
        if(!ls.useGPSEmulator)
            return;

        if(Input.GetKey(KeyCode.W)){
            ls.emulatorPosition = ls.emulatorPosition + new Vector2(0, 1) * moveSpeed * 0.00001f;
        }
        if(Input.GetKey(KeyCode.A)){
            ls.emulatorPosition = ls.emulatorPosition + new Vector2(-1, 0) * moveSpeed * 0.00001f;
        }
        if(Input.GetKey(KeyCode.S)){
            ls.emulatorPosition = ls.emulatorPosition + new Vector2(0, -1) * moveSpeed * 0.00001f;
        }
        if(Input.GetKey(KeyCode.D)){
            ls.emulatorPosition = ls.emulatorPosition + new Vector2(1, 0) * moveSpeed * 0.00001f;
        }
    }
}
