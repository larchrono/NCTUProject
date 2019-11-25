using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalloutObject : MonoBehaviour
{
    public List<GameObject> objectList;

    public Transform pool2D;
    public Transform pool3D;

    int maxIndex;
    int index;
    void Start(){
        index = 0;
        maxIndex = objectList.Count;
    }

    public void ChangeModel(){
        foreach (Transform item in pool2D)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in pool3D)
        {
            Destroy(item.gameObject);
        }

        Instantiate(objectList[index], pool2D);
        Instantiate(objectList[index], pool3D);

        index = (index + 1) % maxIndex;
    }
}
