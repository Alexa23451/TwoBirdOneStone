using System;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour , IBulletInteract
{
    public event Action OnRecharge;
    public Transform bulletStartPoint;

    private void Start()
    {
        OnRecharge += UIManager.Instance.GetPanel<GameplayPanel>().OnRecharge;
    }


    public void OnEnter(GameObject onObject)
    {
        if(onObject.TryGetComponent<BulletBehaviour>(out var bulletBehaviour))
        {
            onObject.transform.position = bulletStartPoint.position;
            onObject.transform.rotation = bulletStartPoint.rotation;

            bulletBehaviour.SetDirection(Vector2.zero, 0);
            bulletBehaviour.gameObject.transform.SetParent(this.gameObject.transform);

            OnRecharge?.Invoke();
        }

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.TAKE_BULLET_BACK);
    }
}
