using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public interface ISceneManagement
{
    void ChangeScene(int id, float timeWait =1f);
    void NextScene(float timeWait = 1f);
    void ReloadScene(float timeWait = 1f);
}

public class SceneController : SceneInvocation , ISceneManagement
{
    private int _currentScene = 0;

    protected override void OnSceneInitialize()
    {
        SceneManager.activeSceneChanged += OnChangeScene;
        Services.RegisterAs<ISceneManagement>(this);

        //Custom don destroy on load
        DontDestroyOnLoad(gameObject);
    }

    void OnChangeScene(Scene cur, Scene next)
    {
        StartScene();
    }

    protected override void StartScene()
    {
        var allServices = new List<DbService>();

        Services.GetAllServices(allServices);

        foreach (var sv in allServices)
        {
            sv.InitializeService();
        }

        //Events.TriggerEvent(new GeneralEvent.AllServiceLoaded());
    }

    protected override void OnObjDestroy()
    {
        Services.Unregister(this);
        SceneManager.activeSceneChanged -= OnChangeScene;
    }

    public void ChangeScene(int id, float timeWait =1f)
    {
        if(id >= SceneManager.sceneCountInBuildSettings)
        {
            LogSystem.LogError("SCENE LOAD OUT OF BOUND");
            return;
        }

        Services.Find(out FadeInFadeOut fadeInFadeOut);

        _currentScene = id;
        DOTween.Play(fadeInFadeOut.Fade(timeWait, () => SceneManager.LoadScene(id), timeWait));
    }

    public void NextScene(float timeWait = 1)
    {
        if (_currentScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LogSystem.LogError("SCENE LOAD OUT OF BOUND");
            return;
        }

        Services.Find(out FadeInFadeOut fadeInFadeOut);

        _currentScene++;
        DOTween.Play(fadeInFadeOut.Fade(timeWait, () => SceneManager.LoadScene(_currentScene), timeWait));
    }

    public void ReloadScene(float timeWait = 1)
    {
        Services.Find(out FadeInFadeOut fadeInFadeOut);
        DOTween.Play(fadeInFadeOut.Fade(timeWait, () => SceneManager.LoadScene(_currentScene), timeWait));
    }
}