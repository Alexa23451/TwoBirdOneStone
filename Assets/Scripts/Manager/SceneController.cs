using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public interface ISceneManagement
{
    void ChangeScene(int id, float timeWait =1f);
}

public class SceneController : SceneInvocation , ISceneManagement
{
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
        if(id > SceneManager.sceneCount)
        {
            LogSystem.LogError("SCENE LOAD OUT OF BOUND");
        }

        Services.Find(out FadeInFadeOut fadeInFadeOut);

        DOTween.Play(fadeInFadeOut.FadeIn(timeWait).OnComplete(() => SceneManager.LoadScene(id)));
        
    }

}