using UnityEngine;
using System.Collections;

// This is used to workaround https://issuetracker.unity3d.com/issues/ios-changing-the-screen-orientation-via-a-script-sometimes-results-in-corrupted-view-on-ios-10
// Bug shows screen in portrait with content rotated 90 offscreen. Caused by explicitly setting Landscape orientation on iOS 10.
// 
// On iOS this just switches to LandscapeLeft, back to Portrait, and then back to LandscapeLeft, which seems to work.
// SUGGESTION: blank out the screen before executing this, since the screen jumps around as it switches back and forth.
public class ForceOrientation : MonoBehaviour
{
    public void ForcePortrait()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        StartCoroutine(ForceAndFixPortrait());
        #else
        Screen.orientation = ScreenOrientation.Portrait;
        #endif
    }

    IEnumerator ForceAndFixPortrait()
    {
        ScreenOrientation prev = ScreenOrientation.Landscape;
        for (int i = 0; i < 3; i++)
        {
            Screen.orientation = 
                (prev == ScreenOrientation.Landscape ? ScreenOrientation.Portrait : ScreenOrientation.Landscape);
            yield return new WaitWhile(() => {
                return prev == Screen.orientation;
            });
            prev = Screen.orientation;
            yield return new WaitForSeconds(0.5f); //this is an arbitrary wait value -- it may need tweaking for different iPhones!
        }
    }
    
}