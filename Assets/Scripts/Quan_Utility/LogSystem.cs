using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogSystem
{
    public static void LogByColor(string text, string color)
    {
        Debug.Log($"<color={color}>" + text + "</color>");
    }

    public static void LogWarning(string text)
    {
        LogByColor(text, "yellow");
    }

    public static void LogError(string text)
    {
        LogByColor(text, "red");
    }

    public static void LogSuccess(string text)
    {
        LogByColor(text, "green");
    }

    public static void ShowCallerInfo(string message,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.Log($"message: {message}");
        Debug.Log($"member name: {memberName}");
        Debug.Log($"source file path: {sourceFilePath}");
        Debug.Log($"source line number: {sourceLineNumber}");
    }

    public static void LogArray<T>(params T[] arr)
    {
        string strArray = "[ ";

        for (int i = 0; i < arr.Length; i++)
        {
            if (i == arr.Length - 1)
            {
                strArray += arr[i].ToString();
            }
            else
            {
                strArray += arr[i].ToString() + ", ";
            }
        }
        strArray += " ]";

        Debug.Log(strArray);
    }

    public static void LogList<T>(List<T> list)
    {
        string strList = "[ ";

        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1)
            {
                strList += list[i].ToString();
            }
            else
            {
                strList += list[i].ToString() + ", ";
            }
        }
        strList += " ]";

        Debug.Log(strList);
    }
}
