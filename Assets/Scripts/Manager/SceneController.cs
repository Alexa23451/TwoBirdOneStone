using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : BaseManager<SceneController>
{
    private int _currentScene = 0;

    public override void Init()
    {
        SceneManager.activeSceneChanged += OnChangeScene;
    }

    void OnChangeScene(Scene cur, Scene next)
    {

    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnChangeScene;
    }

    public void ChangeScene(int id, float timeWait =1f)
    {
        if(id >= SceneManager.sceneCountInBuildSettings)
        {
            LogSystem.LogError("SCENE LOAD OUT OF BOUND");
            return;
        }


        _currentScene = id;
        DOTween.Play(FadeInFadeOut.Instance.Fade(timeWait, () => SceneManager.LoadScene(id), timeWait));
    }

    public void NextScene(float timeWait = 1)
    {
        if (_currentScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LogSystem.LogError("SCENE LOAD OUT OF BOUND");
            return;
        }


        _currentScene++;
        DOTween.Play(FadeInFadeOut.Instance.Fade(timeWait, () => SceneManager.LoadScene(_currentScene), timeWait));
    }

    public void ReloadScene(float timeWait = 1)
    {
        DOTween.Play(FadeInFadeOut.Instance.Fade(timeWait, () => SceneManager.LoadScene(_currentScene), timeWait));
    }
}