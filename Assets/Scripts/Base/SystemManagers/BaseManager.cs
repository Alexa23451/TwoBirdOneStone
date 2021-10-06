using UnityEngine;

/// <summary>
/// Base class for managers (as Singleton)
/// </summary>
public abstract class BaseManager<T> : Singleton<T> where T: MonoBehaviour
{
    /// <summary>
    /// Method contain any preparatory actions needed
    /// by the manager
    /// </summary>
    public abstract void Init();
}
