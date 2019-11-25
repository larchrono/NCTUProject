using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPhoto : MonoBehaviour
{
    public SpriteRenderer renderPicture;

    public void SetPictureData(Sprite data){
        renderPicture.sprite = data;
    }
}
