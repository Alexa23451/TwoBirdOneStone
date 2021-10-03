using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public class LevelEditor : EditorWindow
{
    int map = 11;

    [MenuItem("Tools/Level Editor %h")]
    static void OpenWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        for(int i=0; i<map; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level " + (i + 1) + " : ");
            if (GUILayout.Button("Click"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Level" + (i + 1)+ ".unity", OpenSceneMode.Single);
            }

            EditorGUILayout.EndHorizontal();
        }
    }


}
