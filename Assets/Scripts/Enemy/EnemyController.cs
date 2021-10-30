using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IBulletInteract
{
    public ParticleSystem _explodeVFX;

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

        FX();
    }

    private void FX()
    {
        SoundManager.Instance.Play(Sounds.ENEMY_HIT);

        var vfx = Pooling.InstantiateObject<ParticleSystem>(_explodeVFX.gameObject, transform.position, Quaternion.identity);

        if(DataManager.Instance.VibrateOn)
            UnityAndroidVibrator.Instance.VibrateForGivenDuration();

        vfx.Play();

        Pooling.DestroyObject(vfx.gameObject, 2f);
    }
}
