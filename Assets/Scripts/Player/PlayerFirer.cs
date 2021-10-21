using UnityEngine;
using System;

public class PlayerFirer : MonoBehaviour
{
    public event Action OnPlayerShot;

    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab;

    private PlayerMechanic playerMechanic;
    private BulletBehaviour currentBullet;

    private bool _isShot = false;
    public bool IsShot => _isShot;

    private void Awake()
    {
        playerMechanic = GetComponent<PlayerMechanic>();
        playerMechanic.OnRecharge += OnRecharge;
        UIManager.Instance.GetPanel<GameplayPanel>().OnSpaceUpdate += OnShot;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds += OnRecharge;
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


    private void OnDestroy()
    {
        playerMechanic.OnRecharge -= OnRecharge;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds -= OnRecharge;
        UIManager.Instance.GetPanel<GameplayPanel>().OnSpaceUpdate -= OnShot;
    }
}
