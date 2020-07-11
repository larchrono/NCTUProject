using System.Collections;
using System.Collections.Generic;
using Ideafixxxer.CsvParser;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AboutMeLayout : SoraLib.SingletonMono<AboutMeLayout>
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;

    public void UpdateAboutMe(string t, string c){
        title.text = t;
        content.text = c;
    }
}
