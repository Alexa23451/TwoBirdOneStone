using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseState : IState
{
    public event Action OnLoseGame;

    public LoseState()
    {

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
        SceneController.Instance.ReloadScene();
    }

    public void Exit()
    {
        UIManager.Instance.HidePanelWithDG(typeof(PlayAgainPanel));
    }

}
