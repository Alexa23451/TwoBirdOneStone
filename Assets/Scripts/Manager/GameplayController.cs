using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameplayController : BaseManager<GameplayController>
{
    public Action OnLoseGame;
    public event Action OnWinGame;
    public event Action OnKillEnemy;
    private bool _inGame = false;


    [SerializeField] private List<IHealth> enemyHealth;
    [SerializeField] private int _numberOfEnemy;

    protected override void OnStart()
    {
        enemyHealth = new List<IHealth>();
        SceneManager.activeSceneChanged += OnLoadNewLevel;
        SceneManager.sceneUnloaded += OnUnLoadLevel;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnLoadNewLevel;
        SceneManager.sceneUnloaded -= OnUnLoadLevel;
    }

    private void OnLoadNewLevel(Scene fromScene, Scene toSceneMode)
    {
        FindAllEnemy();
        InitScene();
        
        _inGame = true;
        _numberOfEnemy = enemyHealth.Count;
    }

    private void InitScene()
    {
        foreach(var v in enemyHealth)
        {
            v.OnDie += OnEnemyDie;
        }
    }

    private void OnEnemyDie()
    {
        _numberOfEnemy--;

        if(_numberOfEnemy <= 0)
        {
            OnWinGame?.Invoke();
            Debug.Log("WIN GAME");
        }
    }

    private void FindAllEnemy()
    {
        var healths = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(var v in healths)
        {
            enemyHealth.Add(v.GetComponent<IHealth>());
        }
    }

    private void OnUnLoadLevel(Scene scene)
    {
        enemyHealth.Clear();
        _numberOfEnemy = 0;
        _inGame = false;
    }

    public override void Init()
    {

    }
}
