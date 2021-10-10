using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starage for preloaded sounds
/// </summary>
[CreateAssetMenu(fileName = "SoundStorage", menuName = "Sound storage", order = 50)]
public class SoundStorage : ScriptableObject
{
    [SerializeField]
    private FFSound[] _loadedSounds;

    public FFSound[] GetLoadedSounds() => _loadedSounds;
}