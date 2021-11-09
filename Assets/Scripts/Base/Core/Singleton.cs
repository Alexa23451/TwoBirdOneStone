using UnityEngine;

/// <summary>
/// Singleton pattern implementation, just inherit your class from
/// this and once instance is found or created gameobject will be
/// marked as DontDestroyOnLoad
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Returns a singleton class instance
    /// If current instance is not assigned it will try to find an object of the instance type,
    /// in case instance already exists on a scene. If not, new instance will be created
    /// </summary>
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = GameObject.FindObjectOfType<T>();
            
            if (s_Instance == null)
                Instantiate();

           // Debug.Log($"SINGLETON = {s_Instance.GetType()}");

            return s_Instance;
        }
    }

    private static T s_Instance = null;
    
        
    protected static void Instantiate() 
    {
        string name = typeof(T).FullName;
        s_Instance = new GameObject(name).AddComponent<T>();
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }
    protected virtual void OnAwake(){ }

}
