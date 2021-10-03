using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUpObj : MonoBehaviour, IBulletInteract
{
    public void OnEnter(GameObject onObject)
    {
        var rig = onObject.GetComponent<Rigidbody2D>();
        rig.velocity *= -1;

        VFX();
    }

    private void VFX()
    {

    }

    
}
