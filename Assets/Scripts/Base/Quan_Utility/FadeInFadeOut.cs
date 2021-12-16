using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FadeInFadeOut : BasePanel
{
    [SerializeField] List<Image> followFade;
    private bool _isFade = false;

    public bool IsFade => _isFade;


    public void Fade(float fadeInTime = 1f , Action eventBetween = null, float fadeOutTime = 1f)
    {
        if (_isFade)
        {
            Debug.LogWarning("FADE IN USE !");
            return;
        }
        SoundManager.Instance.Play(Sounds.FadeIn);

        _isFade = true;

        foreach(var img in followFade)
        {
            img.enabled = true;

            img.DOFade(1, fadeInTime).OnComplete(() => { 
                eventBetween?.Invoke();
                img.DOFade(0, fadeOutTime).OnComplete(() =>
                {
                    img.enabled = false;
                    _isFade = false;
                });
            });
        }
    }

    public override void OverrideText()
    {
        throw new NotImplementedException();
    }
}
