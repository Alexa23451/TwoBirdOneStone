using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using UnityEditor;


namespace Module.Achievement.Tool
{
    public static class AchievementTool
    {

        private static string path = Path.Combine(Application.dataPath, "AutoGenerate") + "/AchievementID.txt";
        
        public static void WriteAchievementIdentifierRecord(int id)
        {
            var totalRecord = TotalAchievementRecord();

            var newLine = $"ACH_{totalRecord++} = {id}";
            File.AppendAllText(path, newLine + Environment.NewLine);

            //AssetDatabase.Refresh();

        }

        public static bool isContain(int id)
        {
            var list = GetIDs();
            if(list == null)
            {
                return false;
            }
            if(list.Contains(id))
            {
                return true;
            }
            return false;
        }

        static int TotalAchievementRecord()
        {
            if(File.Exists(path))
            {
                return File.ReadAllLines(path).Length;
            }else
            {
                return 0;
            }
        }

        public static List<int> GetIDs()
        {
            if(File.Exists(path))
            {
                var returnList = new List<int>();
                string[] lines = File.ReadAllLines(path);
                for(int i = 0; i < lines.Length; i++)
                {
                    var element = lines[i].Split('=');
                    returnList.Add(Int32.Parse(element[1]));
                }
                return returnList;
            }
            else
            {
                return null;
            }
        }

        public static List<string> GetDisplayNames()
        {
            if (File.Exists(path))
            {
                var returnList = new List<string>();
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    var element = lines[i].Split('=');
                    returnList.Add(element[0]);
                }
                return returnList;
            }
            else
            {
                return null;
            }
        }




    }
}
