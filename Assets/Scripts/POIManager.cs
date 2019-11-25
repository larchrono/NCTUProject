using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ideafixxxer.CsvParser;
using Sirenix.OdinInspector;
using System.Linq;

public class POIManager : SoraLib.SingletonMono<POIManager>
{
    public double defaultMapLat = 24.788465;
    public double defaultMapLon = 120.999091;
    public GameObject POI_Prefab;
    [Space(40)] public TextAsset csvFile;

    [Button]
    public void ImportPOIData(){
        if(csvFile == null)
            return;

        //讀入 CSV 檔案，使其分為 string 二維陣列
        CsvParser csvParser = new CsvParser();
        string[][] csvTable = csvParser.Parse(csvFile.text);

        var tempList = transform.Cast<Transform>().ToList();
        foreach(var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 1; i < csvTable.Length; i++)
        {
            string poiName = csvTable[i][(int)CSVIndex.NAME];
            string fileName_now = csvTable[i][(int)CSVIndex.NOW_PIC];
            string fileName_old = csvTable[i][(int)CSVIndex.OLD_PIC];
            string description = csvTable[i][(int)CSVIndex.DESCRIPTION];
            double Lat_User, Lon_User, Lat_Goal, Lon_Goal;
            double.TryParse(csvTable[i][(int)CSVIndex.LAT_USER], out Lat_User);
            double.TryParse(csvTable[i][(int)CSVIndex.LON_USER], out Lon_User);
            double.TryParse(csvTable[i][(int)CSVIndex.LAT_GOAL], out Lat_Goal);
            double.TryParse(csvTable[i][(int)CSVIndex.LON_GOAL], out Lon_Goal);

            //Debug.Log(poiName + "\n" + Lat_User + "\n" + Lon_User + "\n" + Lat_Goal + "\n" + Lon_Goal + "\n" + description + "\n");

            GameObject poi = new GameObject();
            poi.tag = "POI";
            POIData data = poi.AddComponent<POIData>();
            data.POI_Name = poiName;
            data.Latitude_User = Lat_User;
            data.Longitude_User = Lon_User;
            data.Latitude_Goal = Lat_Goal;
            data.Longitude_Goal = Lon_Goal;
            data.nowPictureName = fileName_now;
            data.oldPictureName = fileName_old;
            data.description = description;

            #if UNITY_EDITOR
            data.nowPicture = SoraLib.FindAssetTool.FindAssetByName<Sprite>(fileName_now);
            data.oldPicture = SoraLib.FindAssetTool.FindAssetByName<Sprite>(fileName_old);
            #endif

            poi.transform.parent = transform;
            poi.name = string.Format("POI_{0}", poiName);

        }
    }


    public enum CSVIndex {
        NAME = 0,
        LAT_USER = 1,
        LON_USER = 2,
        LAT_GOAL = 3,
        LON_GOAL = 4,
        NOW_PIC = 5,
        OLD_PIC = 6,
        DESCRIPTION = 7,
    }
}
