using System.Collections;
using System;
using UnityEngine;

public interface IHealth
{
    int Healths { get;  }
    void TakeImpact(int damage);
    event Action OnDie;
    event Action OnTakeDamage;
    event Action OnHeal;
}

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected int maxHealth = 100;

    public int Healths => health;

    public event Action OnDie;
    public event Action OnTakeDamage;
    public event Action OnHeal;

    public void TakeImpact(int damage)
    {
        health = damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (damage > 0)
            OnTakeDamage?.Invoke();
        else
            OnHeal?.Invoke();

        if(health <= 0)
        {
            OnDie?.Invoke();
        }
    }
}
