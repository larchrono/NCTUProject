using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//這份Code 必定要存在
//因為使用到WebCamTexture 才能夠使裝置註冊 相機使用權限
public class ScriptsCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture runningWebcam;
    private Texture defaultBackground;

    public RawImage background;
    private int lastRotationAngle = 0;
    public AspectRatioFitter fitter;

    IEnumerator DoStartWebcam(){
        StopWebcam();
        yield return new WaitForEndOfFrame();

        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            yield break;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices [i].isFrontFacing) {
                runningWebcam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (runningWebcam == null) {
            Debug.Log("Unable to find back camera");
            yield break;
        }

        runningWebcam.Play();
        background.texture = runningWebcam;

        camAvailable = true;
    }

    public void StartWebcam(){
        StartCoroutine(DoStartWebcam());
    }

    public void StopWebcam(){
        if(runningWebcam == null)
            return;

        runningWebcam.Pause();
        runningWebcam.Stop();
        runningWebcam = null;
        camAvailable = false;
    }

    void JudgeCamera(){
        //int rotAngle = -runningWebcam.videoRotationAngle;
        //while( rotAngle < 0 ) rotAngle += 360;
        //while( rotAngle > 360 ) rotAngle -= 360;

        float scaleY = runningWebcam.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not  
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera  

        if (runningWebcam != null && lastRotationAngle != runningWebcam.videoRotationAngle)
        {
            OnOrientationChanged();
            lastRotationAngle = runningWebcam.videoRotationAngle;
        }
    }

    private void OnOrientationChanged()
    {
        // 旋转rawimage，为什么加一个负号呢？因为rawimage的z轴是背对图像的，直接使用videoRotationAngle旋转，相对于图片是逆时针旋转
        background.transform.localRotation = Quaternion.Euler(0, 0, -runningWebcam.videoRotationAngle);

        // 判断是否是竖屏，竖屏时由于旋转的关系，需要将width和height调换
        if (runningWebcam.videoRotationAngle % 180 != 0)
            background.rectTransform.sizeDelta = new Vector2(Screen.height, Screen.width);
        else
            background.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        /*
        if (runningWebcam.videoRotationAngle % 180 != 0)
            background.rectTransform.sizeDelta = new Vector2(Display.main.systemHeight, Display.main.systemWidth);
        else
            background.rectTransform.sizeDelta = new Vector2(Display.main.systemWidth, Display.main.systemHeight);
        */
    }

    void Start()
    {
        //StartWebcam();
    }

    void Update()
    {
        if (!camAvailable)
            return;

        JudgeCamera();

        if(!fitter)
            return;

        float ratio = (float)runningWebcam.width / (float)runningWebcam.height;
        fitter.aspectRatio = ratio;

        float scaleY = runningWebcam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3 (1f, scaleY, 1f);    //非鏡像
        //background.rectTransform.localScale = new Vector3(-1f, scaleY, 1f);    //鏡像

        int orient = -runningWebcam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    void OnApplicationQuit() {
        StopWebcam();
    }
}