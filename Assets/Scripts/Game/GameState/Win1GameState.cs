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
        Debug.Log("WIN GAME");

        OnWinGame?.Invoke();
        SoundManager.Instance.Play(Sounds.WIN_LV);
        UIManager.Instance.HideAllPanel();
        sceneManagement.NextScene();
    }

    public void Exit()
    {

    }
}
