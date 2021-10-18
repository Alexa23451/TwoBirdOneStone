using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnlockableItemData : ScriptableObject
{
    public string itemName;
    public bool defaultIsUnlocked;
    public Sprite icon;
    public GameObject previewPrefab;
}
