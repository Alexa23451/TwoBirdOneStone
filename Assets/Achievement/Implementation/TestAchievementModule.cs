using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement.Data;
using Module.Achievement;

public class TestAchievementModule : AchievementModule
{
    public AchievementLibrary Library;

    public const string saveStringKey = "Achievement";

    [SerializeField] private string dataString;

    public override void Initialize(AchievementLibrary data)
    {
        base.Initialize(data);
    }

    private void Start()
    {
        Initialize(Library);
        LoadData();
    }

    [ContextMenu("TEST")]
    public void Test()
    {
        this.LogActivity(
               new Activity()
               {
                   ID = ActivityID.WIN_LV,
                   Value = 1,
                   actType = Activity.VALUETYPE.REPLACE
               }
        );
    }


    [ContextMenu("RESET SAVE DATA")]
    public void ResetData()
    {
        PlayerPrefs.SetString(saveStringKey, null);
    }

    [ContextMenu("SAVE")]
    public void TestSave()
    {
        Save();
    }

    [ContextMenu("LOAD DATA")]
    public void LoadData()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(saveStringKey)))
        {
            return;
        }

        dataString = PlayerPrefs.GetString(saveStringKey);
        Load(dataString);
    }

    [ContextMenu("TEST BITMASK")]
    public void TestBitMask()
    {
        int bitCheck = 1 << 0;
        int field = 0;

        field.SetBit(bitCheck, true);

        Debug.Log(field);

        field.SetBit(bitCheck, false);

        Debug.Log(field);

    }

    private void SetBitMask(ref int mask, int bitSet, bool enable)
    {
        if (enable)
            mask |= bitSet;
        else
            mask &= ~bitSet;
    }

    public override void Save()
    {
        dataString = SaveData;
        PlayerPrefs.SetString(saveStringKey, dataString);
        Debug.Log(dataString);
    }


    private void OnGetContainer_Callback()
    {
        this.LogActivity(
            new Activity()
            {
                ID = ActivityID.MONEY_RICH,
                Value = 1,
                actType = Activity.VALUETYPE.ADD

            }
            );
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroy()
    {
        Save();
    }
}

