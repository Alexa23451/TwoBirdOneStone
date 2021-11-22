using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData")]
public class LevelData : SettingsSingleton<LevelData>
{
    [Range(1,69)] public int totalLevel;

    public int[] moneyRewardOnLevel;
}
