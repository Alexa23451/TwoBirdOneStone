using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseManager<SoundManager>
{
    private const string CONFIG_PATH = "Sounds/SoundManagerSettings";
    private const string DATA_PATH = "Sounds/SoundStorage";
    private const ushort CLEANING_DELAY = 5;
    private const bool REMOVE_IF_STOP = true;
    private const float FADE_RATIO = 0.4f;

    public bool GLOBAL_ON = true;

    private Dictionary<Sounds, FFSound> _loadedSoundsDictionary = new Dictionary<Sounds, FFSound>();    // Preload sounds from soundStorage 
    private List<FFSound> _sounds = new List<FFSound>();                                                // The sounds that were created

    private uint _currentId;

    private GameObject _soundPool;   // destroyable on load sound pool

    public override void Init()
    {
        _loadedSoundsDictionary = ToDictionary(Resources.Load<SoundStorage>(DATA_PATH)?.GetLoadedSounds());

        if (REMOVE_IF_STOP)
        {
            StartCoroutine(Clear());
        }
    }

    private Dictionary<Sounds, FFSound> ToDictionary(FFSound[] loadedSounds)
    {
        Dictionary<Sounds, FFSound> result = new Dictionary<Sounds, FFSound>();
        foreach (FFSound sound in loadedSounds)
            result.Add(sound.soundKey, sound);

        return result;
    }

    /// <summary>
    /// Play sound by key.
    /// </summary>
    /// <param name="soundKey"> Sounds enum.</param>
    /// <param name="isLoop">   If true - Audiosource loop is enable.</param>
    /// <param name="isDeleteOnStopPlay">   Destroy Audiosource object on audiosource stop play.</param>
    public void Play(Sounds soundKey, bool isLoop, bool isDestroyOnLoad, bool IsFade, bool is3D, Transform parent)
    {
        if (!GLOBAL_ON)
            return;

        FFSound ffSound = null;

        try
        {
            ffSound = (FFSound)_loadedSoundsDictionary[soundKey].Clone();
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError($"Key {soundKey} not fount!");
        }

        _currentId++;
        AudioSource audioSource = new GameObject($"Sound: {ffSound.audioClip.name} {_currentId}").AddComponent<AudioSource>();


        if (!parent)
        {
            if (!_soundPool)
                _soundPool = new GameObject("Sound Pool");
            parent = _soundPool.transform;
        }

        audioSource.transform.parent = parent;
        audioSource.transform.localPosition = Vector3.zero;
        audioSource.clip = ffSound.audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = ffSound.soundVolume;

        if (is3D)
        {
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.spatialBlend = 1.0f;
            audioSource.maxDistance = 60f;
            audioSource.minDistance = 15f;
        }

        audioSource.Play();
        if (IsFade)
            StartCoroutine(PlaySmoothCoroutine(audioSource));

        ffSound.audioSource = audioSource;

        _sounds.Add(ffSound);

        if (isDestroyOnLoad)
        {
            audioSource.transform.parent = null;
            DontDestroyOnLoad(audioSource.gameObject);
        }

        return;
    }

    /// <summary>
    /// Play sound by key. After end play, sound-gameobject destroy.
    /// </summary>
    /// <param name="soundKey"></param>
    /// <returns></returns>
    public void Play(Sounds soundKey) => Play(soundKey, false, false, false, false, null);

    /// <summary>
    /// Play sound loop. After end play, sound-gameobject destroy.
    /// </summary>
    /// <param name="soundKey"></param>
    /// <returns></returns>
    public void PlayLoop(Sounds soundKey, bool isDestroyOnLoad = false) => Play(soundKey, true, isDestroyOnLoad, false, false, null);

    /// <summary>
    /// Play only if this sound not playing on scene.
    /// </summary>
    /// <param name="soundKey"></param>
    /// <param name="isLoop"></param>
    /// <param name="isDestroyOnStopPlay"></param>
    public void PlayOneSound(Sounds soundKey, bool isLoop = false, bool isDestroyOnStopPlay = false)
    {
        if (!GLOBAL_ON)
            return;

        foreach (var sound in _sounds)
            if (sound.soundKey == soundKey)
            {
                if (!sound.audioSource.isPlaying)
                {
                    sound.audioSource.Play();
                    sound.audioSource.volume = sound.soundVolume;
                }

                return;
            }

        Play(soundKey, isLoop, isDestroyOnStopPlay, false, false, null);
    }

    public void PlayOneSoundOnParent(Sounds soundKey, bool isLoop, bool isFade, bool is3D, Transform parent)
    {
        StopAll(parent, isFade);
        Play(soundKey, isLoop, false, isFade, is3D, parent);
    }

    /// <summary>
    /// Stop all sounds
    /// </summary>
    public void StopAll(bool isFade = false)
    {
        foreach (var sound in _sounds)
            if (sound.audioSource)
                StopAll( sound.soundKey , isFade);
    }

    public void StopAll(Transform parent, bool isFade)
    {
        AudioSource[] audioSources = parent.GetComponentsInChildren<AudioSource>();

        if (audioSources.Length > 1)
        {
            foreach (var audioSource in audioSources)
                audioSource.Stop();
        }
        else if (audioSources.Length > 0)
        {
            if (isFade)
                StartCoroutine(StopSmoothCoroutine(audioSources[0]));
            else
                audioSources[0].Stop();
        }
    }

    /// <summary>
    /// Stop all sounds wich contains key
    /// </summary>
    /// <param name="soundKey"></param>
    public void StopAll(Sounds soundKey) => StopAll(soundKey, false);

    public void StopAll(Sounds soundKey, bool isFade)
    {
        foreach (var sound in _sounds)
            if (sound.soundKey == soundKey)
            {
                if (sound.audioSource == null)
                    continue;

                if (!isFade)
                    sound.audioSource.Stop();
                else
                    StartCoroutine(StopSmoothCoroutine(sound.audioSource));
            }
    }

    /// <summary>
    /// Stop <b>first</b> sound which contains key
    /// </summary>
    /// <param name="soundKey"></param>
    public void Stop(Sounds soundKey)
    {
        foreach (var sound in _sounds)
            if (sound.soundKey == soundKey && sound.audioSource.isPlaying)
            {
                sound.audioSource.Stop();
                return;
            }
    }

    /// <summary>
    /// Return FFSound by ID. If this id not exist - return null.
    /// </summary>
    /// <param name="id">   sound id.</param>
    /// <returns></returns>
    public FFSound GetSound(uint id)
    {
        foreach (var sound in _sounds)
            if (sound.id == id)
                return sound;

        return null;
    }

    //Playing sound if it not be play
    public void PlaySoundIfNotPlay(Sounds soundKey, bool isFade = false, bool isDestroyOnLoad = false, bool isLoop = false)
    {
        foreach (var sound in _sounds)
            if (sound.soundKey == soundKey)
            {
                if (sound.audioSource.isPlaying)
                {
                    return;
                }
            }

        Play(soundKey, isLoop , isDestroyOnLoad, isFade, false, null);
    }

    private IEnumerator Clear()
    {
        while (true)
        {
            for (int i = 0; i < _sounds.Count;)
            {
                if (_sounds[i].audioSource == null)
                {
                    _sounds.RemoveAt(i);
                    continue;
                }
                else if (!_sounds[i].audioSource.isPlaying)
                {
                    GameObject.Destroy(_sounds[i].audioSource.gameObject);
                    _sounds.RemoveAt(i);
                    continue;
                }
                i++;
            }
            yield return new WaitForSeconds(CLEANING_DELAY);
        }
    }

    private IEnumerator StopSmoothCoroutine(AudioSource audioSource)
    {
        float currentVolume = audioSource.volume;
        while (currentVolume > 0.05f)
        {
            if (audioSource == null)
                yield break;

            currentVolume -= FADE_RATIO * Time.deltaTime;
            audioSource.volume = currentVolume;
            yield return null;
        }

        if (audioSource != null)
            audioSource.Stop();
        yield break;
    }

    private IEnumerator PlaySmoothCoroutine(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;
        float currentVolume = 0;
        audioSource.volume = 0;

        while (currentVolume < startVolume)
        {
            if (audioSource == null)
                yield break;

            currentVolume += FADE_RATIO * Time.deltaTime;
            audioSource.volume = currentVolume;

            yield return null;
        }
        yield break;
    }

    private void OnDestroy()
    {

    }
}