using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData")]
public class ShopData : SettingsSingleton<ShopData>
{
    public ShopItem[] shopItems;

    ShopData()
    {

    }
}
