using UnityEngine;

public interface IBulletInteract
{
    void OnEnter(GameObject onObject);
}

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    public event System.Action OnBulletHit;
    private Vector2 _direction;
    private Rigidbody2D rig;
    private BulletFirer bulletFirer;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        bulletFirer = GetComponent<BulletFirer>();
    }

    public void SetDirection(Vector2 direc, float speed)
    {
        transform.up = direc;
        _direction = direc;
        rig.velocity = _direction * speed;
    }

    private void OnDisable()
    {
        OnBulletHit?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bulletFirer.IsShot)
            return;

        if(collision.gameObject.TryGetComponent<IBulletInteract>(out var bulletInteract))
        {
            bulletInteract.OnEnter(this.gameObject);
        }
    }

}
