using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartLevelState : IState
{
    [SerializeField] private List<IHealth> enemyHealth;
    [SerializeField] private int _numberOfEnemy;

    public event Action OnKillEnemy;

    public StartLevelState()
    {
        enemyHealth = new List<IHealth>();
    }

    public void Enter()
    {
        OnLoadNewLevel();
    }

    public void Exit()
    {
        OnUnLoadLevel();
    }

    private void OnLoadNewLevel()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex < 2)
        {
            return;
        }

        FindAllEnemy();
        InitScene();

        _numberOfEnemy = enemyHealth.Count;
    }

    private void FindAllEnemy()
    {
        var healths = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var v in healths)
        {
            enemyHealth.Add(v.GetComponent<IHealth>());
        }
    }

    private void InitScene()
    {
        foreach (var v in enemyHealth)
        {
            v.OnDie += OnEnemyDie;
        }
    }

    private void OnEnemyDie()
    {
        _numberOfEnemy--;
        OnKillEnemy?.Invoke();

        if (_numberOfEnemy <= 0)
        {
            GameplayController.Instance.WinLevelState();
        }
    }

    private void OnUnLoadLevel()
    {
        enemyHealth.Clear();
        _numberOfEnemy = 0;
    }
}
