using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntenetConnection : MonoBehaviour
{
    private void Start()
    {
        // Begin to check your Internet connection.
        OnlineMaps.instance.CheckServerConnection(OnCheckConnectionComplete);
    }

    // When the connection test is completed, this function will be called.
    private void OnCheckConnectionComplete(bool status)
    {
        // If the test is successful, then allow the user to manipulate the map.
        OnlineMapsControlBase.instance.allowUserControl = status;

        // Showing test result in console.
        Debug.Log(status ? "Has connection" : "No connection");
    }
}
