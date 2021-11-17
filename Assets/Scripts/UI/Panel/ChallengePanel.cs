using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Module.Achievement;

public class ChallengePanel : BasePanel
{
    public Transform missionTransform;
    public MissionElement prefabMission;

    public AchievementProgress[] achievementProgresses;
    public Achievement[] achievements;

    private List<MissionElement> listAchievements = new List<MissionElement>();

    private void Start()
    {
        AchievementManager.Instance.GetAchievementProgress(out achievementProgresses);
        AchievementManager.Instance.GetAchievementData(out achievements);

        for (int i=0; i< achievementProgresses.Length; i++)
        {
            if (!achievementProgresses[i].Complete)
            {
                //Create new achievement
                var newAchievement = GameObject.Instantiate(prefabMission, missionTransform);
                listAchievements.Add(newAchievement);

                newAchievement.Init(achievements[i].Name , achievements[i].Money);
                newAchievement.gameObject.SetActive(true);

            }
            else
            {
                var newAchievement = GameObject.Instantiate(prefabMission, missionTransform);
                listAchievements.Add(newAchievement);

                newAchievement.gameObject.SetActive(false);

            }
        }
    }

    private void OnEnable()
    {
        if(listAchievements.Count == 0)
        {
            return;
        }
        //check progress to change btn
        AchievementManager.Instance.GetAchievementProgress(out achievementProgresses);

        for (int i=0; i<achievementProgresses.Length; i++)
        {
            if (achievementProgresses[i].Complete)
            {
                listAchievements[i].SetCompleteMission();
            }
        }
    }

    public void CloseMenu()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        UIManager.Instance.HidePanelWithDG(typeof(ChallengePanel));
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    
}
