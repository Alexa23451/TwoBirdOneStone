using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUpMechanic : MonoBehaviour, IBulletInteract
{
    public ParticleSystem _dustParticle;

    public void OnEnter(GameObject onObject)
    {
        var rig = onObject.GetComponent<Rigidbody2D>();
        rig.velocity *= -1;
        

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.BACKUP_MECHANIC);

        var vfx = Pooling.InstantiateObject<ParticleSystem>(_dustParticle.gameObject, transform.position, Quaternion.identity);
        Vibrator.Vibrate();
        vfx.Play();

        Pooling.DestroyObject(vfx.gameObject, 2f);
    }


}
