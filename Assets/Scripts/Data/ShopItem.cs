using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem")]
public class ShopItem : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] int goldCost;
    [SerializeField] Sprite imgShopItem;
    [SerializeField] Sprite imgGameItem;

    [HideInInspector] public string ItemName => itemName;
    [HideInInspector] public int GoldCost => goldCost;
    [HideInInspector] public Sprite ImgShop => imgShopItem;
    [HideInInspector] public Sprite ImgGame => imgGameItem;
}
