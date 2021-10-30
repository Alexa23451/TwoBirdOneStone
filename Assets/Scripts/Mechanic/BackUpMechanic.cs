using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUpMechanic : MonoBehaviour, IBulletInteract
{
    public ParticleSystem _dustParticle;

    public void OnEnter(GameObject onObject)
    {
        var rig = onObject.GetComponent<Rigidbody2D>();

        //calculate direction
        float objMag = rig.velocity.magnitude;
        rig.velocity = -transform.up * objMag;
        

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.BACKUP_MECHANIC);
        if (DataManager.Instance.VibrateOn)
            UnityAndroidVibrator.Instance.VibrateForGivenDuration();

        var vfx = Pooling.InstantiateObject<ParticleSystem>(_dustParticle.gameObject, transform.position, Quaternion.identity);

        vfx.Play();

        Pooling.DestroyObject(vfx.gameObject, 2f);
    }


}
