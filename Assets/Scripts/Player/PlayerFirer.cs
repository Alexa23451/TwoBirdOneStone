using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    GameObject GetPlayer();

    public bool IsShot { get; }
}

public class PlayerFirer : DbService, IPlayer
{
    private IPlayerInput playerInput;

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
        playerInput = GetComponent<IPlayerInput>();
        playerMechanic = GetComponent<PlayerMechanic>();

        playerMechanic.OnRecharge += OnRecharge;
        playerInput.OnSpaceUpdate += OnPlayerShot;
    }

    private void OnRecharge()
    {
        _isShot = false;
    }

    private void OnPlayerShot()
    {
        if (!_isShot)
        {
            if (!currentBullet)
            {
                currentBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
            }
            else
            {
                currentBullet.gameObject.SetActive(true);
                currentBullet.gameObject.transform.position = bulletPoint.transform.position;
                currentBullet.gameObject.transform.rotation = bulletPoint.transform.rotation;
            }

            _isShot = true;
            currentBullet.OnBulletHit += OnBulletGone;
            currentBullet.SetDirection(transform.up);
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
        playerInput.OnSpaceUpdate -= OnPlayerShot;

        Services.Unregister(this);
    }
}
