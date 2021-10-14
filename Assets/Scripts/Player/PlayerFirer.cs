using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlayer
{
    GameObject GetPlayer();

    public bool IsShot { get; }
    event Action OnPlayerShot;
}

public class PlayerFirer : DbService, IPlayer
{
    private IPlayerInput playerInput;
    public event Action OnPlayerShot;

    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab;

    private PlayerMechanic playerMechanic;
    private BulletBehaviour currentBullet;

    private bool _isShot = false;
    public bool IsShot => _isShot;

    protected override void OnRegisterServices()
    {
        Services.RegisterAs<IPlayer>(this);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Services.Find(out playerInput);
        playerMechanic = GetComponent<PlayerMechanic>();

        playerMechanic.OnRecharge += OnRecharge;
        playerInput.OnSpaceUpdate += OnShot;
    }

    private void OnRecharge()
    {
        _isShot = false;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnShot();
        }
#endif
    }

    private void OnShot()
    {
        if (!_isShot)
        {
            if (!currentBullet)
            {
                currentBullet = Pooling.InstantiateObject(bulletPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
            }
            else
            {
                currentBullet.gameObject.SetActive(true);
                currentBullet.gameObject.transform.position = bulletPoint.transform.position;
                currentBullet.gameObject.transform.rotation = bulletPoint.transform.rotation;
            }

            SoundManager.Instance.Play(Sounds.SHOT);
            _isShot = true;
            currentBullet.OnBulletHit += OnBulletGone;
            currentBullet.SetDirection(transform.up);
            OnPlayerShot?.Invoke();
        }
    }

    private void OnBulletGone()
    {
        _isShot = false;
        currentBullet.OnBulletHit -= OnBulletGone;
    }


    public GameObject GetPlayer()
    {
        return this.gameObject;
    }

    protected override void OnObjectDestroyed()
    {
        playerMechanic.OnRecharge -= OnRecharge;
        playerInput.OnSpaceUpdate -= OnShot;

        Services.Unregister(this);
    }
}
