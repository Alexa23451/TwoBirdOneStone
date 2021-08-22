using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Services
{
    static Services()
    {
        serviceLinkers = new Dictionary<System.Type, object>();
        services = new List<object>();
    }

    private static Dictionary<System.Type, object> serviceLinkers;
    private static List<object> services;
    public static void RegisterAs<T>(object obj)
    {
        var type = typeof(T);
        Assert.IsTrue(!serviceLinkers.ContainsKey(type));
        if (serviceLinkers.ContainsKey(type))
            return;

        serviceLinkers.Add(type, obj);
        if (!services.Contains(obj))
        {
            services.Add(obj);
        }
    }

    public static bool ContainsService<T>()
    {
        var type = typeof(T);
        return serviceLinkers.ContainsKey(type);
    }

    public static bool ContainsService(System.Type type)
    {
        return serviceLinkers.ContainsKey(type);
    }

    public static void Register(object obj)
    {
        var type = obj.GetType();
        Assert.IsTrue(!serviceLinkers.ContainsKey(type), $"The type {type.FullName} is registered before");
        if (serviceLinkers.ContainsKey(type))
            return;

        serviceLinkers.Add(type, obj);

        if (!services.Contains(obj))
        {
            services.Add(obj);
        }
    }

    public static void Unregister(object obj)
    {
        if (!serviceLinkers.ContainsValue(obj))
            return;

        List<System.Type> types = new List<System.Type>();
        foreach (var kv in serviceLinkers)
        {
            if (kv.Value.Equals(obj))
            {
                types.Add(kv.Key);
            }
        }

        for (int i = 0; i < types.Count; i++)
        {
            var t = types[i];
            serviceLinkers.Remove(t);
        }

        if (services.Contains(obj))
            services.Remove(obj);
    }

    public static bool Find<T>(out T service)
    {
        var type = typeof(T);
        Assert.IsTrue(serviceLinkers.ContainsKey(type));
        if (!serviceLinkers.ContainsKey(type))
        {
            service = default;
            return false;
        }

        service = (T)serviceLinkers[type];
        return true;
    }

    public static void GetAllServices<T>(List<T> cacheList)
    {
        for (int i = 0; i < services.Count; i++)
        {
            var s = services[i];
            if (s is T)
            {
                cacheList.Add((T)s);
            }
        }
    }
}

public abstract class DbService : MonoBehaviour
{
    private void Awake()
    {
        Services.Register(this);
        OnRegisterServices();
        OnAwake();
    }

    public void InitializeService()
    {
        OnInit();
    }

    private void OnDestroy()
    {
        Services.Unregister(this);
        OnObjectDestroyed();
    }

    protected virtual void OnRegisterServices() { }
    protected virtual void OnAwake() { }
    protected virtual void OnInit() { }
    protected virtual void OnObjectDestroyed() { }
}

public abstract class DbSingletonService : DbService
{
    private bool duplicated = false;
    private void Awake()
    {
        var type = GetType();
        Assert.IsTrue(!Services.ContainsService(type), "The Service already has this asset, please check!");

        if (Services.ContainsService(GetType()))
        {
            duplicated = true;
            Destroy(gameObject);
            return;
        }
        Services.Register(this);
        OnRegisterServices();
        OnAwake();
        DontDestroyOnLoad(this.gameObject);
        OnSetSingleton();
    }

    protected virtual void OnSetSingleton() { }

    private void OnDestroy()
    {
        if (duplicated)
            return;

        Services.Unregister(this);
        OnObjectDestroyed();
    }
}

public abstract class DbSceneService : DbService { }
