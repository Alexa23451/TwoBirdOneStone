using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GlobalSetting : SettingsSingleton<GlobalSetting>
{
    [Range(1,69)] public int totalLevel;

    public int[] moneyRewardOnLevel;
}
