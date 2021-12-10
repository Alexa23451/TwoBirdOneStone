using System;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour , IBulletInteract
{
    public event Action OnRecharge;
    public Transform bulletStartPoint;

    private void Start()
    {
        //OnRecharge += UIManager.Instance.GetPanel<GameplayPanel>().OnRecharge;
        

    }

    public void OnEnter(GameObject onObject)
    {
        OnRecharge?.Invoke();

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.TAKE_BULLET_BACK);
    }
}
