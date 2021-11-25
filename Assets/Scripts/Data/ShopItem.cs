using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem")]
public class ShopItem : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] int goldCost;
    [SerializeField] Sprite imgItem;

    [HideInInspector] public string ItemName => itemName;
    [HideInInspector] public int GoldCost => goldCost;
    [HideInInspector] public Sprite ImgItem => imgItem;
}
