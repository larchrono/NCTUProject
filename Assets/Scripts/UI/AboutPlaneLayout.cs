using System.Collections;
using System.Collections.Generic;
using Ideafixxxer.CsvParser;
using UnityEngine;
using UnityEngine.UI;

public class AboutPlaneLayout : SoraLib.SingletonMono<AboutPlaneLayout>
{
    public Text title;
    public Text content;

    public void UpdateAboutMe(string t, string c){
        title.text = t;
        content.text = c;
    }
}
