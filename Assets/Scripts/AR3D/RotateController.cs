using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public float RotateSpeed = 60;
    private Camera ControllerCamera;
    private Transform _activeObject = null;
    private bool isPhoto = false;

    private void Start() {
        ControllerCamera = GetComponent<Camera>();
    }

    private void SetRotateObject(Transform newObject) {
        if(newObject.gameObject.tag == "ARObject") {
            _activeObject = newObject;

            isPhoto = _activeObject.GetComponent<IsPhoto>() ? true : false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            RaycastHit[] hit;

            /* If we're currently not dragging any augmentation, do a raycast to find one in the scene. */
            if (_activeObject == null) {

                hit = Physics.RaycastAll(ControllerCamera.ScreenPointToRay(touch.position));
                foreach (var item in hit)
                {
                    SetRotateObject(item.transform);
                }
            }

            if (_activeObject != null) {

                if(Input.touchCount == 1)
                    if(isPhoto == false)
                        _activeObject.Rotate(0, RotateSpeed * Time.deltaTime, 0);
            }
            
        }

        if(Input.touchCount == 0 && !Input.GetMouseButton(0)){
            /* If there are no touches, stop dragging the currently dragged augmentation, if there is one. */
            _activeObject = null;
        }
    }
}
