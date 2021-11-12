using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Module.Achievement.Data;
using UnityEngine;


public class AchievementEditor : Editor
{
    [MenuItem("Tools/Achievement/Create Achievement Data")]
    static void CreateAchievementData()
    {
        var asset = ScriptableObject.CreateInstance<AchievementData>();
        asset.Init();

        AssetDatabase.CreateAsset(asset, "Assets/AchievementData/Achievement/newAchievementData.asset");
        AssetDatabase.Refresh();

        Selection.activeObject = asset;
    }
}
