using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    List<ICustomUpdate> customUpdates = new List<ICustomUpdate>();
    List<ICustomUpdate> nullRef = new List<ICustomUpdate>();
    static bool isQuiting;

    
    static CustomUpdateManager _instance;
    public static CustomUpdateManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<CustomUpdateManager>();
            }

            return _instance;
        }
        private set { _instance = value; }
    }

    private void Awake()
    {
        instance = this;
        isQuiting = false;
        //Application.quitting += ApplicationQuitting;
    }

    private void OnDestroy()
    {
        //Application.quitting -= ApplicationQuitting;
    }

    private void ApplicationQuitting()
    {
        isQuiting = true;
    }

    public static void Register(ICustomUpdate sender)
    {
        instance.customUpdates.Add(sender);
    }

    public static void UnRegister(ICustomUpdate sender)
    {
        if(!isQuiting & instance)
            instance.customUpdates.Remove(sender);
    }

    private void Update()
    {
        foreach (var update in customUpdates)
        {
            if (update == null)
            {
                nullRef.Add(update);
                continue;
            }

            update.currentUpdateTime += Time.deltaTime;
            if (update.currentUpdateTime >= update.updateInterval)
            {
                update.currentUpdateTime = 0;
                update.CustomUpdate();
            }
        }

        if (nullRef.Count > 0)
        {
            foreach (var update in nullRef)
            {
                customUpdates.Remove(update);
            }

            nullRef.Clear();
        }
    }
}
