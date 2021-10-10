using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(Image))]
public class FadeInFadeOut : DbSingletonService
{
    private Image img;
    private bool _isFade = false;

    public bool IsFade => _isFade;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        img = GetComponent<Image>();
        SetBGColor(Color.black);
    }

    public void SetBGColor(Color c) => img.color = c; 

    public Tweener Fade(float fadeInTime = 1f , Action eventBetween = null, float fadeOutTime = 1f)
    {
        if (_isFade)
        {
            Debug.LogWarning("FADE IN USE !");
            return null;
        }

        img.enabled = true;
        _isFade = true;

        return FadeIn(fadeInTime).OnComplete(delegate {
                eventBetween?.Invoke();
                DOTween.Play(FadeOut());
            });
    }

    private Tweener FadeIn(float fadeInTime = 1f)
    {
        return DOTween.To(() => img.color, (x) => img.color = x, Color.black, fadeInTime);
    }

    private Tweener FadeOut(float fadeOutTime = 1f)
    {
        return DOTween.To(() => img.color, (x) => img.color = x, Color.clear, fadeOutTime).OnComplete(
            delegate { img.enabled = false; _isFade = false; });
    }
}
