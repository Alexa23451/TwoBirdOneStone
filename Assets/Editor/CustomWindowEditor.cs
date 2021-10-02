using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomWindowEditor : Editor
{
    [MenuItem("Tools/Init %g")]
    static void Init()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Init.unity", OpenSceneMode.Single);
        Debug.Log("OPEN INIT");
    }

    [MenuItem("Tools/Menu %t")]
    static void MainMenu()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
        Debug.Log("OPEN MENU");
    }



}
