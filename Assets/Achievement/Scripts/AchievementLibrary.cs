using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Achievement.Data
{
    [CreateAssetMenu(menuName = "Data/Achievement/Library")]
    public class AchievementLibrary : ScriptableObject
    {
        public AchievementData[] CoreData;

        public AchievementData[] InitialData;

        public AchievementGroupData[] GroupData;

    }

}
