using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDeveloper.AdvancedObjectPool;
public interface IPlayer
{
    GameObject GetPlayer();

    public bool IsShot { get; }

}

public class PlayerMoveAndShot : DbService , IPlayer
{
    private IPlayerInput playerInput;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab;

    private PlayerMechanic playerMechanic;
    private BulletBehaviour currentBullet;

    private bool _isShot = false;

    bool IPlayer.IsShot
    {
        get => _isShot; 
    }

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
        playerInput.OnHorizontalUpdate += OnPlayerMove;
        playerInput.OnSpaceUpdate += OnPlayerShot;
    }

    private void OnPlayerMove(float horizontal)
    {
        transform.position += Vector3.right * horizontal * playerInput.Speed * Time.deltaTime;
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
    private void OnRecharge()
    {
        _isShot = false;
    }

    private void OnBulletGone()
    {
        _isShot = false;
        currentBullet.OnBulletHit -= OnBulletGone;
    }

    protected override void OnObjectDestroyed()
    {
        playerInput.OnHorizontalUpdate -= OnPlayerMove;
        playerMechanic.OnRecharge -= OnRecharge;

        Services.Unregister(this);
    }

    public GameObject GetPlayer()
    {
        return this.gameObject;
    }
}
