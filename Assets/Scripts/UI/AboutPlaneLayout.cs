using System.Collections;
using System.Collections.Generic;
using Ideafixxxer.CsvParser;
using UnityEngine;
using UnityEngine.UI;

public class AboutPlaneLayout : MonoBehaviour
{
    public Text title;
    public Text content;
    IEnumerator Start(){
        while(CheckIntenetConnection.instance.InternetStats == false){
            yield return null;
        }

        DownloadManager.GoogleGetCSV(UpdateAboutMe, OnlineDataManager.instance.webService, OnlineDataManager.instance.sheetID, OnlineDataManager.instance.AboutPlan_PageID);
    }

    void UpdateAboutMe(string result){
        //讀入 CSV 檔案，使其分為 string 二維陣列
        CsvParser csvParser = new CsvParser();
        string[][] csvTable = csvParser.Parse(result);

        if(csvTable.Length >= 2 && csvTable[0].Length >= 2){
            title.text = csvTable[0][1];
            content.text = csvTable[1][1];
        }
    }
}
