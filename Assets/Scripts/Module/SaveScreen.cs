using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScreen : MonoBehaviour
{
    public Canvas UICanvas;
    public GameObject Blink;
    public CaptureAndSave snapShotHelper;

    private void OnEnable() {
        CaptureAndSaveEventListener.onSuccess += OnSuccess;
    }

    private void OnDisable() {
        CaptureAndSaveEventListener.onSuccess -= OnSuccess;
    }

    public void OnClickScreenCaptureButton()
    {
        StartCoroutine(DoSaveToGallery());
    }

    public IEnumerator DoSaveToGallery(){
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        UICanvas.enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        snapShotHelper.CaptureAndSaveToAlbum();

        //Wait for snap callback below
    }

    void OnSuccess(string msg){
        // Show UI after we're done
        UICanvas.enabled = true;

        if(Blink)
            Instantiate(Blink, UICanvas.transform);
    }


    //Legacy Method for Screen capture

    public IEnumerator DoCaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        UICanvas.enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave, 0);
        Debug.Log("File is save to :" + Application.persistentDataPath + "/" + fileName);

        // Show UI after we're done
        UICanvas.enabled = true;

        yield return new WaitForEndOfFrame();

        if(Blink)
            Instantiate(Blink, UICanvas.transform);
    }


    IEnumerator ScreenShotEncode()
    {
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        yield return new WaitForEndOfFrame();
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();
        //自訂scale
        Texture2D newScreenshot = new Texture2D(screenshot.width / 2, screenshot.height / 2);
        newScreenshot.SetPixels(screenshot.GetPixels(1));
        newScreenshot.Apply();
        byte[] bytes = newScreenshot.EncodeToPNG();
        //自訂儲存路徑
        string localURL = Application.persistentDataPath + "/image.png";
        System.IO.File.WriteAllBytes(localURL, bytes);
    }
}
