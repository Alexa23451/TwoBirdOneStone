using System;
using System.Collections.Generic;

public class Events
{
    public delegate void GenericDelegate<T>(T param);

    private static Dictionary<Type, Delegate> delegates;

    static Events()
    {
        delegates = new Dictionary<Type, Delegate>();
    }

    public static void AddListener<T>(GenericDelegate<T> del)
    {
        var type = typeof(T);
        if (delegates.TryGetValue(type, out Delegate tempDel))
        {
            GenericDelegate<T> d = tempDel as GenericDelegate<T>;

            d += del;
            delegates[type] = d;
        }
        else
        {
            delegates[type] = del;
        }
    }

    public static void RemoveListener<T>(GenericDelegate<T> del)
    {
        var type = typeof(T);
        if (!delegates.TryGetValue(type, out Delegate tempDel))
            return;

        GenericDelegate<T> d = tempDel as GenericDelegate<T>;

        d -= del;
        if (d != null)
        {
            delegates[type] = d;
        }
        else
        {
            delegates.Remove(type);
        }
    }

    public static void TriggerEvent<T>(T param)
    {
        var type = typeof(T);
        if (!delegates.TryGetValue(type, out Delegate tempDel))
            return;

        GenericDelegate<T> del = tempDel as GenericDelegate<T>;

        del?.Invoke(param);
    }

}