using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour , IBulletInteract
{
    public event System.Action OnRecharge;

    public void OnEnter(GameObject onObject)
    {
        onObject.SetActive(false);
        OnRecharge?.Invoke();

        VFX();
    }

    private void VFX()
    {

    }
}
