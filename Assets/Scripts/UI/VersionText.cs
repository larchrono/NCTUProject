using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionText : MonoBehaviour
{
    public Text TXT_Version;
    public TextMeshProUGUI TMP_Version;

    void Start()
    {
        TXT_Version = GetComponent<Text>();
        TMP_Version = GetComponent<TextMeshProUGUI>();

        string v_text = $"Version {Application.version}";

        if(TXT_Version)
            TXT_Version.text = v_text;

        if(TMP_Version)
            TMP_Version.text = v_text;
    }
}
