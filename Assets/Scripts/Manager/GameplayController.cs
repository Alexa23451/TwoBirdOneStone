using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameplayController : BaseManager<GameplayController>
{
    public event Action OnLoseGame;
    public event Action OnWinGame;
    public event Action OnKillEnemy;

    [SerializeField] private List<IHealth> enemyHealth;
    [SerializeField] private int _numberOfEnemy;

    protected override void OnStart()
    {
        enemyHealth = new List<IHealth>();
        SceneManager.sceneLoaded += OnLoadNewLevel;
        SceneManager.sceneUnloaded += OnUnLoadLevel;
    }

    private void OnLoadNewLevel(Scene scene, LoadSceneMode loadSceneMode)
    {
        FindAllEnemy();
        InitScene();

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
    }

    public override void Init()
    {

    }
}
