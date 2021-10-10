using System;
using UnityEngine;

public class LoseState : IState
{
    ISceneManagement sceneManagement;
    FadeInFadeOut fadeSystem;
    public event Action OnLoseGame;

    public LoseState()
    {
        Services.Find(out sceneManagement);
        Services.Find(out fadeSystem);
    }

    public void Enter()
    {
        if (fadeSystem.IsFade)
            return;

        Debug.Log("LOSE GAME");

        OnLoseGame?.Invoke();
        SoundManager.Instance.Play(Sounds.LOSE_LV);
        UIManager.Instance.HideAllPanel();
        sceneManagement.ReloadScene();
    }

    public void Exit()
    {

    }

}
