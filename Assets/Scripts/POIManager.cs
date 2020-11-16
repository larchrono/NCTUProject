using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ideafixxxer.CsvParser;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine.Networking;

public class POIManager : SoraLib.SingletonMono<POIManager>
{
    public double defaultMapLat = 24.788465;
    public double defaultMapLon = 120.999091;
    public GameObject POI_Prefab;
    public GameObject SLAM_Prefab;
    public List<Sprite> IconPack;
    string ImageServerURL = "";

    IEnumerator Start()
    {
        while(CheckIntenetConnection.instance.InternetStats == false){
            yield return null;
        }
        DownloadManager.GoogleGetCSV(ImportPOIData, OnlineDataManager.instance.webService, OnlineDataManager.instance.sheetID, OnlineDataManager.instance.POI_pageID);
        yield return new WaitForSeconds(2.0f);
        DownloadManager.GoogleGetCSV(GetInfos, OnlineDataManager.instance.webService, OnlineDataManager.instance.sheetID, OnlineDataManager.instance.Infos_PageID);
    }

    public void ImportPOIData(string csvFile)
    {

        //讀入 CSV 檔案，使其分為 string 二維陣列
        CsvParser csvParser = new CsvParser();
        string[][] csvTable = csvParser.Parse(csvFile);

        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 1; i < csvTable.Length; i++)
        {
            string poiName = csvTable[i][(int)CSVIndex.NAME];
            string fileName_now = csvTable[i][(int)CSVIndex.NOW_PIC];
            string fileName_old = csvTable[i][(int)CSVIndex.OLD_PIC];
            string description = csvTable[i][(int)CSVIndex.DESCRIPTION];
            string m_color = csvTable[i][(int)CSVIndex.MARKER_COLOR];
            string youtube = csvTable[i][(int)CSVIndex.YOUTUBE];
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
            data.YoutubeURL = youtube;
            data.ColorMarker = IconPack.Find((x) => x.name == m_color);

            if(!string.IsNullOrEmpty(fileName_now))
                StartCoroutine(DownloadImage(fileName_now, data.NowPictureSetter));
            if(!string.IsNullOrEmpty(fileName_old))
                StartCoroutine(DownloadImage(fileName_old, data.OldPictureSetter));

            poi.transform.parent = transform;
            poi.name = string.Format("POI_{0}", poiName);

        }
    }

    public void GetInfos(string csvFile){
        //讀入 CSV 檔案，使其分為 string 二維陣列
        CsvParser csvParser = new CsvParser();
        string[][] csvTable = csvParser.Parse(csvFile);

        if(csvTable.Length == 0 || csvTable[0].Length == 0){
            Debug.LogError("Online info is error format");
            return;
        }

        string about_title = csvTable[0][1];
        string about_content = csvTable[1][1];
        string url = csvTable[2][1];
        string initPosition = csvTable[3][1];

        try {
            double lat = 0, lon = 0;
            if(!string.IsNullOrEmpty(initPosition)){
                string[] slt = initPosition.Split(',');
                double.TryParse(slt[0], out lat);
                double.TryParse(slt[1], out lon);

                defaultMapLat = lat;
                defaultMapLon = lon;

                OnlineMaps.instance.SetPosition(lon, lat);
                Debug.Log($"Set map view to {lat}, {lon}");
            }
        }
        catch(Exception e) {
            Debug.Log(e.Message.ToString());
        }

        AboutMeLayout.instance.UpdateAboutMe(about_title, about_content);

        ImageServerURL = url;
        Debug.Log($"Use Image URL : {ImageServerURL}");
    }

    IEnumerator DownloadImage(string fileName, Action<Sprite> callback)
    {
        while (string.IsNullOrEmpty(ImageServerURL))
        {
            yield return null;
        }
        string path = ImageServerURL + fileName + ".jpg";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log($"{request.error} ({path})");
        }
        else
        {
            Texture2D webTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            // if(webTexture.width > 640)
            //     webTexture.Resize(640, (int)((webTexture.height * 1.0f) / webTexture.width * 640));

            Sprite webSprite = SpriteFromTexture2D(webTexture);
            webSprite.name = $"{fileName}.spt";
            callback?.Invoke(webSprite);
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }


    IEnumerator PingConnect()
    {
        bool result = false;
        WaitForSeconds wait = new WaitForSeconds(5);
        while (!result)
        {
            UnityWebRequest request = new UnityWebRequest("http://google.com");
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.Log("Have no Internet, retry after 5 seconds...");
            }
            else
            {
                result = true;
            }
            yield return wait;
        }

        Debug.Log("Network access success.");
    }

    public enum CSVIndex
    {
        NAME = 0,
        LAT_USER = 1,
        LON_USER = 2,
        LAT_GOAL = 3,
        LON_GOAL = 4,
        NOW_PIC = 5,
        OLD_PIC = 6,
        DESCRIPTION = 7,
        MARKER_COLOR = 8,
        YOUTUBE = 9,
    }
}
