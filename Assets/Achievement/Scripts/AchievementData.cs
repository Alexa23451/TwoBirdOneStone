using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement;
using Module.Achievement.Tool;

namespace Module.Achievement.Data
{
    public class AchievementData : ScriptableObject
    {
        [IdRange(IsEditable = true, SupportType = typeof(AchievementData))]
        public int achievementID;
        public string achievevementName;
        public string achievementDescription;
        public AchievementCondition[] Conditions;


        public void Init()
        {
            var rand = new System.Random();
            achievementID = rand.Next();
            while (AchievementTool.isContain(achievementID))
            {
                achievementID = rand.Next();
                if (AchievementTool.isContain(achievementID))
                {
                    break;
                }
            }
            AchievementTool.WriteAchievementIdentifierRecord(achievementID);

        }

        public Achievement CreateAchievement()
        {
            var conditions = new Condition[Conditions.Length];
            for (int i = 0; i < conditions.Length; i++)
            {
                var cond = Conditions[i].CreateCondition();
                conditions[i] = cond;
            }

            Achievement achievement = new Achievement(achievementID, achievevementName, achievementDescription, conditions);
            
            return achievement;            
        }


    }


    [System.Serializable]
    public class AchievementCondition
    {
        [IdRange(IsEditable = true)]
        public int activityID;
        public Condition.COMPARE type;
        public int value;

        public Condition CreateCondition()
        {
            Condition cond = new Condition(activityID, type, value, 0);
            return cond;
        }
    }

}
