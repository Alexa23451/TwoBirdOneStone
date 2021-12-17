using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponItem
{
    public bool[] slingShotItem;
}

public class InventoryManager : BaseManager<InventoryManager>
{
    [SerializeField] private WeaponItem weaponItem = new WeaponItem();

    [SerializeField] private string saveString;

    public const string saveStringKey = "Inventory";


    public override void Init()
    {

    }

    void Start()
    {
        Load();
    }

    public bool IsBuyItem(int id)
    {
        if (id < 0 || id >= weaponItem.slingShotItem.Length)
        {
            Debug.LogError("???");
            return false;
        }

        return weaponItem.slingShotItem[id];
    }

    [ContextMenu("SAVE")]
    private void Save()
    {
        saveString = JsonUtility.ToJson(weaponItem);
        PlayerPrefs.SetString(saveStringKey, saveString);
    }

    [ContextMenu("LOAD")]
    private void Load()
    {
        saveString = PlayerPrefs.GetString(saveStringKey);

        if(string.IsNullOrEmpty(saveString))
        {
            weaponItem.slingShotItem = new bool[ShopData.Instance.shopItems.Length];
            //set default
            weaponItem.slingShotItem[0] = true;
            return;
        }

        weaponItem = JsonUtility.FromJson<WeaponItem>(saveString);

    }

    [ContextMenu("DELETE")]
    private void Delete()
    {
        PlayerPrefs.SetString(saveStringKey, null);
    }

    public void SetBuyItem(int id, bool val)
    {
        if (id < 0 || id >= weaponItem.slingShotItem.Length)
        {
            Debug.LogError("???");
            return;
        }

        weaponItem.slingShotItem[id] = val;
    }

    private void OnDestroy()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            Save();
    }
}
