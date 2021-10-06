public interface ICustomUpdate
{
    /// <summary>
    /// Register this at Awake
    /// </summary>
    void Register();

    /// <summary>
    /// Unregister this after destory
    /// </summary>
    void Unregister();

    void CustomUpdate();

    float currentUpdateTime { get; set; }
    float updateInterval { get; }
}
