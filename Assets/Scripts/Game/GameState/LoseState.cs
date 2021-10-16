using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("LOSE GAME");

        OnLoseGame?.Invoke();
        SoundManager.Instance.Play(Sounds.LOSE_LV);
        UIManager.Instance.ShowPanelWithDG(typeof(PlayAgainPanel));
        UIManager.Instance.GetPanel<PlayAgainPanel>().SetLvText("Level " + (SceneManager.GetActiveScene().buildIndex - 1).ToString());
    }

    public void OnPlayAgain()
    {
        sceneManagement.ReloadScene();
    }

    public void Exit()
    {
        UIManager.Instance.HidePanelWithDG(typeof(PlayAgainPanel));
    }

}
