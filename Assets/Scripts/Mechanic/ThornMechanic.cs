using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMechanic : MonoBehaviour, IBulletInteract
{
    public event System.Action OnThorn;

    public void OnEnter(GameObject onObject)
    {
        onObject.SetActive(false);
        OnThorn?.Invoke();

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.ENEMY_HIT);
    }
}
