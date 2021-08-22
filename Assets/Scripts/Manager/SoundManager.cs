using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : DbSingletonService
{
    AudioSource audioSource;

    public AudioClip bgm;

    protected override void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioClip) {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(audioClip);
    }

    public void StopAudioSlowly(float time)
    {
        if (!audioSource.isPlaying)
            return;

        DOTween.To(() => audioSource.volume, (x) => audioSource.volume = x, 0f, time);
    }
}
