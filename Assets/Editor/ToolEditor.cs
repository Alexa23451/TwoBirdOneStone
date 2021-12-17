using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ToolEditor : EditorWindow
{
    [MenuItem("Tools/Clear Save File %h")]
    static void ClearSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData");
        if (File.Exists(path))
        {
            File.Delete(path);
            PlayerPrefs.DeleteAll();
            Debug.Log("DELETE SAVE FILE");
        }
        
    }
}
