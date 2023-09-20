using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : SoraLib.SingletonMono<PlatformManager>
{
#if UNITY_IOS
    public static Platform platform = Platform.IOS;
    public static EnableARFundation enableARFundation = EnableARFundation.ON;
#elif UNITY_ANDROID
    public static Platform platform = Platform.ANDROID;
    public static EnableARFundation enableARFundation = EnableARFundation.ON;
#else
    public static Platform platform = Platform.OTHER;
    public static EnableARFundation enableARFundation = EnableARFundation.ON;
#endif
}

public enum Platform
{
    ANDROID = 0,
    IOS = 1,
    OTHER = 9,
}

public enum EnableARFundation
{
    OFF = 0,
    ON = 1,
}