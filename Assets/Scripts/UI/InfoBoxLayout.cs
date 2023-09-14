using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoBoxLayout : CanvasGroupExtend
{
    public static InfoBoxLayout instance;

    public Image AnimTreasure;

    //Bubble Layout
    public Button BTNPanelClose;
    public Button BTNClose;
    public Text Title;
    public Text Artist;
    public Text Format;
    public Image MapIcon;
    public Text GoadRange;
    public Button Go3D;
    public Button Open3D;
    public Text AlertMessage;
    
    //Content Layout
    public ContentHeightController contentHeightController;
    public RawImage ContentYoutube;
    public Button BTNPlayYoutube;
    public Text PlayTip;
    public Image ContentPhoto;
    public Text ContentText;
    public POIData currentData;

    //Public Parameter
    public float treasureAnimTime = 0.4f;
    public Ease treasureEase = Ease.OutCirc;
    public float treasurePunchVal = 10;
    public float MinimunDistanceForLook = 20;

    OnlineMapsLocationService locationService;
    float distanceBetweenPOI = 0;

    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        AnimTreasure.transform.localScale = new Vector3(0, 0, 0);

        BTNClose.onClick.AddListener(DoCloseWindow);
        BTNPanelClose.onClick.AddListener(DoCloseWindow);
        BTNPlayYoutube.onClick.AddListener(DoPlayYoutube);
        Open3D.onClick.AddListener(OnOpenAR);

        locationService = OnlineMapsLocationService.instance;
        if (locationService != null)
            locationService.OnLocationChanged += OnDistanceChange;

        CloseSelfImmediate();
    }

    void DoCloseWindow(){
        CloseSelf();
        YoutubeManager.instance.Stop();
    }

    void OnOpenAR(){
        StartCoroutine(CheckAR3D());
    }

    IEnumerator CheckAR3D(){
        yield return null;

        #if UNITY_IOS
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            UIManager.instance.WarningCamera.gameObject.SetActive(true);
            while(true){
                yield return new WaitForSeconds(5);
            }
        }
        #endif

        Open3D.interactable = false;
        UIARLayout.instance.CVSLAM.SetupOldPictureSLAM(currentData.artmodel);
        UIARLayout.instance.StartSLAM();
        
        yield return new WaitForSeconds(1);

        Open3D.interactable = true;
    }

    void DoPlayYoutube(){
        if(!YoutubeManager.instance.CanPlay())
            return;

        ContentPhoto.gameObject.SetActive(false);
        ContentYoutube.gameObject.SetActive(true);
        YoutubeManager.instance.Play();
    }

    public void OpenInfoBoxWithPOI(POIData data){
        currentData = data;

        Title.text = data.POI_Name;
        Artist.text = data.artist;
        Format.text = data.format;
        MapIcon.sprite = data.ColorMarker == null? MapIcon.sprite : data.ColorMarker;
        ContentYoutube.gameObject.SetActive(false);
        ContentPhoto.gameObject.SetActive(true);
        ContentPhoto.sprite = data.artpreview;
        ContentText.text = data.description;

        if(string.IsNullOrEmpty(data.YoutubeURL)){
            PlayTip.gameObject.SetActive(false);
            YoutubeManager.instance.currentURL = string.Empty;
        } else {
            PlayTip.gameObject.SetActive(true);
            YoutubeManager.instance.currentURL = data.YoutubeURL;
        }

        if(locationService != null)
            OnDistanceChange(locationService.position);
        
        if(distanceBetweenPOI < MinimunDistanceForLook)
            AlertMessage.gameObject.SetActive(false);
        else
            AlertMessage.gameObject.SetActive(true);

        contentHeightController.ResizeContent();

        DoTreasureAnim();
    }

    public void DoTreasureAnim(){
        AnimTreasure.rectTransform.DOPunchAnchorPos(new Vector2(0, treasurePunchVal), treasureAnimTime);
        AnimTreasure.transform.DOScale(new Vector3(1, 1, 1), treasureAnimTime).SetEase(treasureEase).OnComplete(()=>{
            OpenSelf();
            AnimTreasure.DOFade(0, 0.5f).OnComplete(() => {
                AnimTreasure.transform.localScale = new Vector3(0, 0, 0);
                AnimTreasure.DOFade(1, 0);
            });
        });
    }

    void OnDistanceChange(Vector2 userPoint){
        if(currentData == null)
            return;
        
        Vector2 markerCoordinates = new Vector2((float)currentData.Longitude_User, (float)currentData.Latitude_User);
        Vector2 userCoordinares = userPoint;

        // Calculate the distance in km between locations.
        distanceBetweenPOI = OnlineMapsUtils.DistanceBetweenPoints(userCoordinares, markerCoordinates).magnitude * 1000;

        if(distanceBetweenPOI > 1000)
            GoadRange.text = string.Format("距離 {0} km", (distanceBetweenPOI / 1000).ToString("0.00"));
        else
            GoadRange.text = string.Format("距離 {0} m", distanceBetweenPOI.ToString("0.0"));
            
        //GoadRange.text = "距離 " + distanceBetweenPOI.ToString("0.0") + " m";
    }
}
