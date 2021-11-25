using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData")]
public class ShopData : SettingsSingleton<ShopData>
{
    [SerializeField] public ShopItem[] shopItems;
}
