using System;
using UnityEngine;

/// <summary>
/// It is a set of data storing a sound, its key, volume, etc. Used in the sound controller.
/// </summary>
[Serializable]
public class FFSound : ICloneable
{
    public AudioClip audioClip;
    public Sounds soundKey;
    [Range(0, 1f)] public float soundVolume;

    [HideInInspector] public uint id;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public bool isDestroyOnStopPlay;

    public object Clone() => new FFSound
    {
        audioClip = this.audioClip,
        soundKey = this.soundKey,
        soundVolume = this.soundVolume,
        id = this.id,
        audioSource = this.audioSource,
        isDestroyOnStopPlay = this.isDestroyOnStopPlay
    };
}