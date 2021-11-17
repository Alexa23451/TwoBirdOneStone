using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement;

public class AchievementLogic : MonoBehaviour
{
    void Start()
    {
        GameplayController.Instance.GetState<Win1GameState>().OnWinGame += OnWinGame;
    }

    private void OnWinGame(int lv)
    {
        Debug.LogError(lv);
        AchievementManager.Instance.LogActivity(
            new Activity
            {
                ID = ActivityID.WIN_LV,
                Value = lv,
                actType = Activity.VALUETYPE.REPLACE
            }
            );
    }
    
}
