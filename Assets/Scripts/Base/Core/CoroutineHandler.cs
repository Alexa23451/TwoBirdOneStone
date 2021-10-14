using UnityEngine;
using System.Collections;
using System;
using System.Threading.Tasks;

public enum GameState { Enter, Quit }

public class StaticVariable
{
    public static GameState GameState;
}

/// <summary>
/// This class allows us to start Coroutines from non-Monobehaviour scripts
/// Create a GameObject it will use to launch the coroutine on
/// </summary>
public class CoroutineHandler : MonoBehaviour
{
    static protected CoroutineHandler m_Instance;
    static public CoroutineHandler instance
    {
        get
        {
            if (m_Instance == null)
            {
                GameObject o = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(o);
                m_Instance = o.AddComponent<CoroutineHandler>();
            }

            return m_Instance;
        }
    }

    public void OnDisable()
    {
        if (m_Instance)
        {
            StopAllCoroutines();
            Destroy(m_Instance.gameObject);
        }
    }

    public static Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }

    public static void StopStaticCoroutine(Coroutine coroutine)
    {
        instance.StopCoroutine(coroutine);
    }
}



//////////////////////////////////////////////////////
public static class CoroutineUtils
{
    //public static IEnumerator Chain(params IEnumerator[] actions)
    //{
    //    foreach (IEnumerator action in actions)
    //    {
    //        yield return SomeSingletonGO.instance.StartCoroutine(action);
    //    }
    //}

    public static Coroutine PlayManyCoroutine(float timeDelay, float timeBetween, params Action[] actions)
    {
        return CoroutineHandler.StartStaticCoroutine(DelayManySeconds(timeDelay, timeBetween, actions));
    }

    private static IEnumerator DelayManySeconds(float timeDelay, float timeBetween, params Action[] actions)
    {
        yield return WaitForSecondCache.GetWFSCache(timeDelay);
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i]();

            yield return WaitForSecondCache.GetWFSCache(timeBetween);
        }
    }

    public static Coroutine PlayCoroutine(Action action, float delay)
    {
        return CoroutineHandler.StartStaticCoroutine(DelaySeconds(action, delay));
    }

    public static Coroutine PlayCoroutineRequire(Action action, float delay, Func<bool> req)
    {
        return CoroutineHandler.StartStaticCoroutine(DelaySecondsAndRequire(action, delay, req));
    }

    private static IEnumerator DelaySecondsAndRequire(Action action, float delay, Func<bool> req)
    {
        yield return WaitForSecondCache.GetWFSCache(delay);
        yield return new WaitUntil(req);
        action();
    }

    private static IEnumerator DelaySeconds(Action action, float delay)
    {
        yield return WaitForSecondCache.GetWFSCache(delay);
        action();
    }

    public static IEnumerator Do(Action action)
    {
        action();
        yield return null;
    }
}




//Threadings
public static class TaskUtils
{
    public static async void PlayTask(Task ActionTask)
    {
        await ActionTask;
    }
}

//TaskUtils.PlayTask(Task.Run(async () =>
//{
//    await Task.Delay(10000);
//    LogSystem.LogWarning("100");

//    await Task.Delay(2000);
//    LogSystem.LogWarning("200");

//    await Task.Delay(3000);
//    LogSystem.LogWarning("300");
//}));