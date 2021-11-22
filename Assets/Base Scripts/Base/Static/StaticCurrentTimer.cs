using System;
using System.Collections;
using UnityEngine;

public static class StaticCurrentTimer
{
    public static int CurrentTimeInSecond
    {
        get
        {
            return (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }

    public static int CurrentNextTimeInSecond(int minute)
    {
        DateTime today = DateTime.Now;
        return (int)(today.AddMinutes(minute) - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static int CurrentTimeDayInSecond
    {
        get
        {
            return (int)(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59) - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }

    public static int CurrenTimeInDay
    {
        get
        {
            return (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalDays;
        }
    }

    public static int CurrenTimeAddDay(int day)
    {
        DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
        return (int)(today.AddDays(day) - new DateTime(1970, 1, 1)).TotalDays;
    }

    public static int CurrentSecondInDay
    {
        get
        {
            return (int)(DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
        }
    }
}
