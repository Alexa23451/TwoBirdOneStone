using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMechanic : MonoBehaviour, IBulletInteract
{
    public event System.Action OnThorn;
    public ParticleSystem _thornParticle;


    public void OnEnter(GameObject onObject)
    {
        onObject.SetActive(false);
        OnThorn?.Invoke();


        VFX();
        TimerManager.Instance.AddTimer(0.5f, ()=> GameplayController.Instance.LoseLevelState());
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.ENEMY_HIT);

        var vfx = Pooling.InstantiateObject<ParticleSystem>(_thornParticle.gameObject, transform.position, Quaternion.identity);
        Handheld.Vibrate();
        vfx.Play();

        Pooling.DestroyObject(vfx.gameObject, 2f);
    }
}
