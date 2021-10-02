using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for different application States
/// </summary>
public interface IState
{
    /// <summary>
    /// Method will invoke when we get to this state (enabling current UI, Controllers etc)
    /// </summary>
    void Enter();
    
    /// <summary>
    /// Method will invoke when we get out of this state(disabling current UI, Controllers etc)
    /// </summary>
    void Exit();
}
