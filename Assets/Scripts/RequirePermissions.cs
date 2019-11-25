using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2018_3 && UNITY_ANDROID
using UnityEngine.Android;
#endif

public class RequirePermissions : MonoBehaviour
{
    public GameObject MapSystem;

    void Awake()
    {
        #if UNITY_2018_3_OR_NEWER && UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        #endif
    }

    IEnumerator Start(){
        #if UNITY_2018_3_OR_NEWER && UNITY_ANDROID
        /* Since 2018.3, Unity doesn't automatically handle permissions on Android, so as soon as
         * the menu is displayed, ask for camera permissions. */
         while(true){
            int totalRequest = 0;

            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation)) {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
            } else totalRequest++;

            yield return new WaitForSeconds(1.0f);

            if(totalRequest >= 1)
                break;
         }
        #endif
        if(MapSystem != null)
            MapSystem.SetActive(true);

        #if UNITY_2018_3_OR_NEWER && UNITY_ANDROID
        while(true){
            int totalRequest = 0;

            if(!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite)){
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
            } else totalRequest++;
            if(!UnityEngine.Android.Permission.HasUserAuthorizedPermission("android.permission.INTERNET")){
                UnityEngine.Android.Permission.RequestUserPermission("android.permission.INTERNET");
            } else totalRequest++;
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera)) {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
            } else totalRequest++;

            yield return new WaitForSeconds(1.0f);

            if(totalRequest >= 3)
                break;
        }
        #endif
        yield return null;
    }

    public void QuitApplication(){
        Application.Quit();
    }
}
