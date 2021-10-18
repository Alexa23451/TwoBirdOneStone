using UnityEngine;

public class SettingsSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T s_Instance = null;

    public static T Instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = Resources.Load($"Settings/{typeof(T).Name}") as T;

#if UNITY_EDITOR
            if(s_Instance == null)
                Instantiate();
#endif

            return s_Instance;
        }
    }

#if UNITY_EDITOR
    protected static void Instantiate()
    {
        s_Instance = ScriptableObject.CreateInstance<T>();
        UnityEditor.AssetDatabase.CreateAsset(s_Instance, $"Assets/Resources/Settings/{typeof(T).Name}.asset");
    }
#endif
}