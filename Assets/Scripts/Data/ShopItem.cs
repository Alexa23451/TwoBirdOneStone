using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int goldCost;
    public Sprite imgImg;
}
