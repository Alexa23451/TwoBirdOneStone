using System.Collections.Generic;
using UnityEngine;
using System;

public static class HexStringRandomEncoder 
{

    private static List<string> generatedHexList = new List<string>();
    
    public static Dictionary<string, string> GenerateRandomHex(string[] readableId)
    {
        var resultDictionary = new Dictionary<string, string>();
        for (int i = 0; i < readableId.Length; i++)
        {
            int randomNum = UnityEngine.Random.Range(0, 65535);
            string hex = Convert.ToString(randomNum, 16);
            while(generatedHexList.Contains(hex))
            {
                randomNum = UnityEngine.Random.Range(0, 65535);
                hex = Convert.ToString(randomNum, 16);
                if (!generatedHexList.Contains(hex))
                {
                    break;
                }
            }
            generatedHexList.Add(hex);
            resultDictionary.Add(hex, readableId[i]);
        }
        return resultDictionary;
    }
}
