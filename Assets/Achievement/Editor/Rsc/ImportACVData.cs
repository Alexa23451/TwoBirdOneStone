using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ImportACVData : Editor
{
   
    [MenuItem("Tools/Import Activity Data")]
    static void ImportActivityData()
    {
        var obj = Selection.activeObject;
        if (obj == null)
            return;

        string[] lines = (obj as TextAsset).text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        var dict = HexStringRandomEncoder.GenerateRandomHex(lines);
        WriteFile(Path.Combine(Application.dataPath, "AutoGenerate") + "/AssetsActivityId.cs", dict);
        AssetDatabase.Refresh();
    }

    static void WriteFile(string path, Dictionary<string, string> dict)
    {
        StringBuilder sb = new StringBuilder();


        File.WriteAllText(path, "");

        sb.AppendLine("public static class ActivityID");
        sb.AppendLine("{");
        foreach(KeyValuePair<string, string> kvp in dict)
        {
            sb.AppendLine($"public const int {kvp.Value} =0x{kvp.Key} ; \n");
        }

        var count = dict.Count;
        sb.AppendLine($"public static readonly string[] GetDisplayString = new string[{dict.Count}]");
        sb.AppendLine("{");
        var index = 0;
        foreach(KeyValuePair<string, string> kvp in dict)
        {
            if(index == count-1)
            {
                sb.AppendLine($"\"{kvp.Value}\"");
            }
            else
            {
                sb.AppendLine($"\"{kvp.Value}\",");

            }
            index++;
        }
        sb.AppendLine("};");

        sb.AppendLine($"public static readonly int[] GetInt = new int[{dict.Count}]");
        sb.AppendLine("{");
        foreach(KeyValuePair<string, string> kvp in dict)
        {
            if(index == count - 1)
            {
                sb.AppendLine($"0x{kvp.Key}");
            }
            else
            {
                sb.AppendLine($"0x{kvp.Key},");
            }
        }
        sb.AppendLine("};");
        sb.AppendLine("}");


        File.WriteAllText(path, sb.ToString());

    }
}