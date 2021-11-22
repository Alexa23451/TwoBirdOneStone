using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSerializable
{
    public string id = "hihidochoo";
    public bool isAds = true;
    public int level = 1;
    public int coin = 0;
}

[System.Serializable]
public class ItemBought
{
    public string id;
    public List<int> colors;

    public ItemBought()
    {
        colors = new List<int>();
    }

    public ItemBought(string _id, List<int> _colors)
    {
        id = _id;
        colors = _colors;
    }
}
