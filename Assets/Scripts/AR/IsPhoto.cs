using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IsPhoto : MonoBehaviour
{
    public SpriteRenderer renderPicture;
    public float FadeToAlpha = 0.8f;
    public float FadeDuration = 2;
    public float TouchingTransparent = 0.5f;
    public float TouchingFadeDuration = 0.5f;

    bool IsTouching = false;

    Tweener currentTween;

    public void SetPictureData(Sprite data){
        if(renderPicture == null)
            return;

        renderPicture.sprite = data;
    }

    public void FadeingPicture(){
        if(renderPicture == null)
            return;

        if(currentTween != null)
            currentTween.Complete();
        renderPicture.color = new Color(1, 1, 1, 0.1f);
        currentTween = renderPicture.DOFade(FadeToAlpha, FadeDuration);
    }

    private void OnMouseDown() {
        if(currentTween != null)
            currentTween.Kill();
        currentTween = renderPicture.DOFade(TouchingTransparent, TouchingFadeDuration);
    }

    private void OnMouseUp() {
        if(currentTween != null)
            currentTween.Kill();
        currentTween = renderPicture.DOFade(FadeToAlpha, TouchingFadeDuration);
    }

    //public void Touch
}
