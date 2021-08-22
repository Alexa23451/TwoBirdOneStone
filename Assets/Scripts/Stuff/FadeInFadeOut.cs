using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(Image))]
public class FadeInFadeOut : DbService
{
    Image img;

    protected override void OnAwake()
    {
        img = GetComponent<Image>();
    }

    protected override void OnInit()
    {
        SetBGColor(Color.black);
        FadeOut();
    }

    public void SetBGColor(Color c) => img.color = c; 

    public Tweener Fade(float fadeInTime = 1f , float fadeOutTime = 1f)
    {
        return DOTween.To( () => img.color, (x) => img.color = x, Color.black, fadeInTime)
            .OnComplete( () => FadeOut(fadeOutTime));
    }

    public Tweener FadeIn(float fadeInTime = 1f)
    {
        return DOTween.To(() => img.color, (x) => img.color = x, Color.black, fadeInTime);
    }

    public void FadeOut(float fadeOutTime = 1f)
    {
        DOTween.To(() => img.color, (x) => img.color = x, Color.clear, fadeOutTime);
    }
}
