using System;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement.Data;


namespace Module.Achievement
{
    public abstract class AchievementModule : BaseManager<AchievementModule> , IAchievementModule
    {
        //static 
        [SerializeField] protected Achievement[] achievements; // the array will load from updated data;

        //dynamic 
        [SerializeField] protected Activity[] activities; // the array will load from updated data.
        [SerializeField] protected AchievementProgress[] progress;
        [SerializeField] protected AchievementGroup[] groups;

        private Activity defaultActivity;
        private AchievementProgress defaultAchievementProgress;
        private ConditionProgress defaultConditionProgress;
        [SerializeField] private List<int> newUnlockedAchievements;
        private bool isUnlockedNew;

        public int[] GetNewUnlockAchievements()
        {
            if (isUnlockedNew)
            {
                isUnlockedNew = false;
                var arr = newUnlockedAchievements.ToArray();
                newUnlockedAchievements.Clear();
                return arr;
            }
            return null;
        }
        public string SaveData
        {
            get
            {
                ProgressWrapper wrapper = new ProgressWrapper()
                {
                    acts = new string[activities.Length],
                    achs = new string[progress.Length]
                };

                for (int i = 0; i < wrapper.acts.Length; i++)
                {
                    wrapper.acts[i] = Encode(activities[i].ID, activities[i].Value);
                }

                for (int i = 0; i < wrapper.achs.Length; i++)
                {
                    wrapper.achs[i] = Encode(progress[i].achievementID, progress[i].Status);
                }

                var data = JsonUtility.ToJson(wrapper);
                return data;
            }

        }


        //Inititialization
        public virtual void Initialize(AchievementLibrary data)
        {
            //create base achievement data 
            achievements = new Achievement[data.CoreData.Length];
            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = data.CoreData[i].CreateAchievement();
            }

            //create base activity
            var activityIDs = ActivityID.GetInt;
            activities = new Activity[activityIDs.Length];
            for (int i = 0; i < activityIDs.Length; i++)
            {
                activities[i] = new Activity()
                {
                    ID = activityIDs[i],
                    Value = 0
                };
            }

            //create base progress
            progress = new AchievementProgress[achievements.Length];
            for (int i = 0; i < progress.Length; i++)
            {

                var unlocked = false;
                AchievementCondition[] conditions = null;
                foreach (AchievementData achData in data.CoreData)
                {
                    if (achData.achievementID == achievements[i].ID)
                    {
                        unlocked = true;
                        conditions = achData.Conditions;
                        break;
                    }
                }
                progress[i] = new AchievementProgress()
                {
                    achievementID = achievements[i].ID,
                    Unlock = unlocked
                };

                if (conditions != null)
                {
                    progress[i].conditionProgresses = new ConditionProgress[conditions.Length];
                    for (int j = 0; j < progress[i].conditionProgresses.Length; j++)
                    {
                        progress[i].conditionProgresses[j] = new ConditionProgress()
                        {
                            conditionID = conditions[j].activityID,
                            completeStatus = 0
                        };

                    }
                }

            }

            //create base group 
            groups = new AchievementGroup[data.GroupData.Length];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = data.GroupData[i].CreateAchievementGroup();
            }

            newUnlockedAchievements = new List<int>();
        }


        //Log Activity
        public void LogActivity(params Activity[] activities)
        {
            for(int i = 0; i < activities.Length; i++)
            {
                var act = activities[i];

                ref var rAct = ref GetActivity(act.ID);

                switch (act.actType)
                {
                    case Activity.VALUETYPE.ADD:
                        rAct.Value += act.Value;
                        break;
                    case Activity.VALUETYPE.REPLACE:
                        rAct.Value = act.Value;
                        break;
                }
            }
            
            foreach (var achievement in achievements)
            {
                if (ConditionDone(activities, achievement))
                {
                    Debug.LogError("condition done");
                    CheckAchievementProgress(achievement.ID);
                }
            }
        }

        private bool ConditionDone(Activity[] activities, Achievement achievement)
        {
            bool conditionMeet = true;

           

            foreach (var condition in achievement.Conditions)
            {
                bool activityMeet = true;
                for (int i = 0; i < activities.Length; i++)
                {
                    var act = activities[i];
                    if (condition.Id == act.ID)
                    {
                        ref var rAct = ref GetActivity(act.ID);
                        ref var rConditionProgress = ref GetConditionProgress(act.ID, achievement.ID);
                        

                        if (condition.CompareType.HasFlag(Condition.COMPARE.EQUAL))
                        {
                            if (rAct.Value == condition.Value)
                            {
                                rConditionProgress.completeStatus = 1;                                
                            }
                        }

                        if (condition.CompareType.HasFlag(Condition.COMPARE.LESS))
                        {
                            if (rAct.Value < condition.Value)
                            {
                                rConditionProgress.completeStatus = 1;
                            }
                        }

                        if (condition.CompareType.HasFlag(Condition.COMPARE.GREATER))
                        {
                            if (rAct.Value > condition.Value)
                            {
                                rConditionProgress.completeStatus = 1;
                            }
                        }
                    }
                }

                ref var progress = ref GetAchievementProgress(achievement.ID);
                for(int i = 0; i < progress.conditionProgresses.Length; i++)
                {
                    ref var conditionP = ref GetConditionProgress(progress.conditionProgresses[i].conditionID, progress.achievementID);
                    if(conditionP.completeStatus == 0)
                    {
                        activityMeet = false;
                        break;
                    }
                }


                if (!activityMeet)
                {
                    conditionMeet = false;
                }

            }

            return conditionMeet;
        }

        private void CheckAchievementProgress(int achievementID)
        {
            ref var progress = ref GetAchievementProgress(achievementID);
            if (progress.Unlock)
            {
                progress.Complete = true;
                Debug.LogError(GetCompleteTime(achievementID));
                for (int i = 0; i < groups.Length; i++)
                {
                    CheckUnlockable(groups[i]);
                }
            }
        }

        private void CheckUnlockable(AchievementGroup group)
        {
            var complete = true;
            for (int i = 0; i < group.conditionIDs.Length; i++)
            {
                ref var progress = ref GetAchievementProgress(group.conditionIDs[i]);
                if (!progress.Complete)
                {
                    complete = false;
                    break;
                }
            }

            if (complete)
            {
                isUnlockedNew = true;

                for (int i = 0; i < group.unlockIDs.Length; i++)
                {
                    ref var progress = ref GetAchievementProgress(group.unlockIDs[i]);
                    progress.Unlock = true;
                    newUnlockedAchievements.Add(group.unlockIDs[i]);
                }
            }


        }

        private ref Activity GetActivity(int activityId)
        {
            for (int i = 0; i < this.activities.Length; i++)
            {
                ref var act = ref activities[i];
                if (act.ID == activityId)
                {
                    return ref act;
                }
            }

            return ref defaultActivity;
        }


        private ref ConditionProgress GetConditionProgress(int activityId, int achievementId)
        {
            ref var progress = ref GetAchievementProgress(achievementId);
            for (int i = 0; i < progress.conditionProgresses.Length; i++)
            {
                ref var conditionProgress = ref progress.conditionProgresses[i];
                if(conditionProgress.conditionID == activityId)
                {
                    return ref conditionProgress;
                }

            }
            return ref defaultConditionProgress;

        }

        private ref AchievementProgress GetAchievementProgress(int achievementID)
        {
            for (int i = 0; i < this.progress.Length; i++)
            {
                ref var progress = ref this.progress[i];
                if (progress.achievementID == achievementID)
                {
                    return ref progress;
                }
            }
            return ref defaultAchievementProgress;
        }




        //update load and save
        private string Encode(int id, int value)
        {
            return $"{Convert.ToString(id, 16)}-{Convert.ToString(value, 16)}";
        }

        private void UpdateProgress(ProgressWrapper wrapper)
        {
            for (int i = 0; i < wrapper.acts.Length; i++)
            {
                string[] elements = wrapper.acts[i].Split('-');
                int id = int.Parse(elements[0], System.Globalization.NumberStyles.HexNumber);
                int value = int.Parse(elements[1], System.Globalization.NumberStyles.HexNumber);
                ref var activity = ref GetActivity(id);
                activity.Value = value;

            }
            

            for (int i = 0; i < wrapper.achs.Length; i++)
            {
                string[] elements = wrapper.achs[i].Split('-');
                int id = int.Parse(elements[0], System.Globalization.NumberStyles.HexNumber);
                int value = int.Parse(elements[1], System.Globalization.NumberStyles.HexNumber);
                ref var achProgress = ref GetAchievementProgress(id);
                achProgress.SetStatus(value);

            }
        }

        public virtual void Save()
        {

        }

        public virtual void Load(string data)
        {
            if (data != null)
            {
                ProgressWrapper wrapper = JsonUtility.FromJson<ProgressWrapper>(data);
                UpdateProgress(wrapper);

            }

        }


        //ConvertTime
        public DateTime GetCompleteTime(int id)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

            ref var progress = ref GetAchievementProgress(id);

            return epoch.AddMilliseconds(progress.CompleteTime);
        }

        public void GetAchievementProgress(out AchievementProgress[] progressList)
        {
            progressList = (AchievementProgress[]) progress.Clone();
        }

        public void GetAchievementData(out Achievement[] achievementDatas)
        {
            achievementDatas = (Achievement[])achievements.Clone();
        }
    }

}
