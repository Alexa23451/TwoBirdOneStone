using System;
using UnityEngine;
using System.Collections.Generic;
using Module.Achievement.Data;

namespace Module.Achievement
{
    public interface IAchievementModule
    {
        void Initialize(AchievementLibrary data);
        void LogActivity(params Activity[] activities);
        string SaveData { get; }
        void Load(string data);
        void Save();
        int[] GetNewUnlockAchievements();

        DateTime GetCompleteTime(int id);
    }

    #region USED_FOR_LOG

    [System.Serializable]
    public struct Activity
    {
        [IdRange]
        public int ID;
        public int Value;
        [HideInInspector]public VALUETYPE actType;
        
        [System.Flags]
        public enum VALUETYPE
        {
            ADD,
            REPLACE
        }
    }
    #endregion

    #region RUNTIME
    [System.Serializable]
    public class Achievement
    {

        [IdRange(IsEditable = true, SupportType = typeof(AchievementData))]
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string desc;
        [SerializeField] private Condition[] conditions;

        public int ID => id;
        public string Name => name;
        public string Desc => desc;
        public Condition[] Conditions => conditions;
        public Achievement(int id, string name, string desc, Condition[] conditions)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.conditions = conditions;
        }
    }

    [System.Serializable]
    public class Condition
    {
        [System.Flags]
        public enum COMPARE
        {
            EQUAL = 1<<0,
            GREATER = 1<<1,
            LESS = 1<<2, 
            GREATER_AND_EQUAL = EQUAL|GREATER,
            LESS_AND_EQUAL = EQUAL| LESS
        }

        [IdRange]
        [SerializeField] private int id;
        [SerializeField] private COMPARE compareType;
        [SerializeField] private int value;

        public int Id => id;
        public COMPARE CompareType => compareType;
        public int Value => value;
        

        public Condition(int id, COMPARE compareType, int value, int progress)
        {
            this.id = id;
            this.compareType = compareType;
            this.value = value;
        }

    }

    public struct AchievementGroup
    {
        public int[] conditionIDs;
        public int[] unlockIDs;
    }

    [System.Serializable]
    public struct AchievementProgress
    {
        public int achievementID;

        public const int IS_UNLOCKED = 1 << 0;
        public const int IS_COMPLETED = 1 << 1;

        public ConditionProgress[] conditionProgresses;

        [SerializeField]private int status;
        [SerializeField]private long completeEpochTime;

        public int Status => status;
        public long CompleteTime => completeEpochTime;

        public bool Complete
        {
            get
            {
                return status.HasBit(IS_COMPLETED);
            }
            set
            {
                if (value)
                {
                    status.EnableBit(IS_COMPLETED);
                    completeEpochTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalMilliseconds;
                }
                else
                {
                    status.ClearBit(IS_COMPLETED);
                }
            }
        }
        public bool Unlock
        {
            get
            {
                return status.HasBit(IS_UNLOCKED);
            }
            set
            {
                if (value)
                    status.EnableBit(IS_UNLOCKED);
                else
                    status.ClearBit(IS_UNLOCKED);
            }
        }

        public void SetStatus(int status)
        {
            this.status = status;
        }
      
    }
    #endregion

    [System.Serializable]
    public struct ConditionProgress
    {
        public int conditionID;
        public int completeStatus; //0 is incomplete. 1 is complete
    }

    #region SAVE_LOAD
    [System.Serializable]
    public struct ProgressWrapper
    {
        public string[] acts;
        public string[] achs;

    }
    #endregion

    

    public static class BitMaskOpt
    {
        public static void SetBit(ref this int mask, int bitSet, bool enable)
        {
            if (enable)
                mask |= bitSet;
            else
                mask &= ~bitSet;
        }

        public static void EnableBit(ref this int mask, int bitset)
        {
            mask |= bitset;
        }

        public static void ClearBit(ref this int mask, int bitset)
        {
            mask &= ~bitset;
        }

        public static bool HasBit(ref this int mask, int bitset)
        {
            return (mask & bitset) == bitset;
        }
    }

}


