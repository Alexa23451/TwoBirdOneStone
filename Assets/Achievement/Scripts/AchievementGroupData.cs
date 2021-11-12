using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Module.Achievement.Data
{
    [CreateAssetMenu(menuName = "Data/AchievementGroup")]
    public class AchievementGroupData : ScriptableObject
    {
        [IdRange(IsEditable = true, SupportType = typeof(AchievementData))]
        public int[] conditionIDs;
        [IdRange(IsEditable = true, SupportType = typeof(AchievementData))]
        public int[] unlockIDs;
        public AchievementGroup CreateAchievementGroup()
        {
            AchievementGroup group = new AchievementGroup()
            {
                conditionIDs = this.conditionIDs,
                unlockIDs = this.unlockIDs
            };
            return group;
        }
    }

}
