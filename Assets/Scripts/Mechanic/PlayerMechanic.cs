using System;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour , IBulletInteract
{
    public event Action OnRecharge;

    private void Start()
    {
        OnRecharge += UIManager.Instance.GetPanel<GameplayPanel>().OnRecharge;
    }


    public void OnEnter(GameObject onObject)
    {
        onObject.SetActive(false);
        OnRecharge?.Invoke();

        VFX();
    }

    private void VFX()
    {
        SoundManager.Instance.Play(Sounds.TAKE_BULLET_BACK);
    }
}
