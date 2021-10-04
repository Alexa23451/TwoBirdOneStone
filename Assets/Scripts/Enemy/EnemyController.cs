using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IBulletInteract
{
    private IHealth health;
    [SerializeField] private int _damageTaken = -1;

    public event Action OnInteract;

    void Start()
    {
        health = GetComponent<IHealth>();
    }

    public void OnEnter(GameObject onObject)
    {
        OnInteract?.Invoke();
        gameObject.SetActive(false);
        health.TakeImpact(_damageTaken);

        VFX();
    }

    private void VFX()
    {

    }
}
