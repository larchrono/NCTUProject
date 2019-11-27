using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModelInfo : MonoBehaviour
{
    Text _text;
    
    public GameObject _model;
    public Camera _camera;

    Transform _target;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _target = _model.transform.GetChild(0);

        _text.text = "";

        if(_target != null)
            _text.text += "" + _target.gameObject.name + "\n" +  _target.transform.position + "\n\n";

         _text.text += _camera.transform.position + "\n";
         _text.text += _camera.transform.rotation.eulerAngles;
    }
}
