using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public class LevelEditor : EditorWindow
{
    int map = 11;

    [MenuItem("Tools/Level Editor %g")]
    static void OpenWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("INIT :");
        if (GUILayout.Button("Click"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Init.unity", OpenSceneMode.Single);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("MainMenu :");
        if (GUILayout.Button("Click"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
        }
        EditorGUILayout.EndHorizontal();


        for (int i=0; i<map; i++)
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
