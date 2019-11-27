using UnityEngine;
using DG.Tweening;

public class ImageTargetBehaviour : ImageTargetBase
{
    public SpriteRenderer picture;
    public float FadeToAlpha = 0.8f;
    public float FadeDuration = 2;
    void Awake()
    {
        AddEventListener(VoidAREvent.FIND, OnFind);
        //AddEventListener(VoidAREvent.LOST, OnFind);
    }

    void OnFind(VoidAREvent evt)
    {
        Debug.Log(" ImageTargetBehaviour OnFind Event target:" + evt.currentTarget + " data = " + evt.data + " type = " + evt.name);
        
        if(picture == null)
            return;
        
        picture.color = new Color(1, 1, 1, 0);
        picture.DOFade(FadeToAlpha, FadeDuration);
    }
}