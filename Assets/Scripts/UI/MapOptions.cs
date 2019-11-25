using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOptions : MonoBehaviour
{
    public Dropdown mapSource;
    public Dropdown mapType;
    int [] selections = new int[] {0, 1, 1};

    string [] mapSourceKey = new string[] {
        "arcgis",
        "google",
        "nokia",
    };

    string [,] mapTypeKey = new string[,] {
        {"worldtopomap", "worldstreetmap"},
        {"satellite", "terrain"},
        {"terrain", "map"},
    };

    void Start()
    {
        mapSource.onValueChanged.AddListener(OnMapSource);
        mapType.onValueChanged.AddListener(OnMapType);

        GetTypes(0);
    }

    void OnMapSource(int index){
        GetTypes(index);
        
        if(mapType.value == selections[index]){
            SwitchMap();
        } else {
            mapType.value = selections[index];
        }
    }

    void OnMapType(int index){
        selections[mapSource.value] = index;
        SwitchMap();
    }

    void SwitchMap(){
        string MapKey = string.Format("{0}.{1}", mapSourceKey[mapSource.value], mapTypeKey[mapSource.value, mapType.value]);
        OnlineMaps.instance.mapType = MapKey;
        //Debug.Log(MapKey);
    }

    void GetTypes(int index){
        if(index == 0){
            mapType.options.Clear ();
            mapType.options.Add (new Dropdown.OptionData() {text = "worldtopomap"});
            mapType.options.Add (new Dropdown.OptionData() {text = "worldstreetmap"});
        }
        else if (index == 1){
            mapType.options.Clear ();
            mapType.options.Add (new Dropdown.OptionData() {text = "satellite"});
            mapType.options.Add (new Dropdown.OptionData() {text = "terrain"});
        }
        else if (index == 2){
            mapType.options.Clear ();
            mapType.options.Add (new Dropdown.OptionData() {text = "terrain"});
            mapType.options.Add (new Dropdown.OptionData() {text = "map"});
        }
    }
}
