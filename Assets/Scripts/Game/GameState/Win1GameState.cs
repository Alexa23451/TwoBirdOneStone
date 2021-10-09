using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Win1GameState : IState
{
    public event Action OnWinGame;
    ISceneManagement sceneManagement;

    public Win1GameState()
    {
        Services.Find(out sceneManagement);
    }

    public void Enter()
    {
        OnWinGame?.Invoke();
        UIManager.Instance.HideAllPanel();
        sceneManagement.NextScene();
    }

    public void Exit()
    {

    }
}
