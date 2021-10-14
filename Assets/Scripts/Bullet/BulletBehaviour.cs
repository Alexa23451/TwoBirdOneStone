using UnityEngine;

public interface IBulletInteract
{
    void OnEnter(GameObject onObject);
}

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    private IBulletInteract bulletInteract;
    private Vector2 _direction;
    private Rigidbody2D rig;
    public event System.Action OnBulletHit;

    [SerializeField] private bool isFly;
    [SerializeField] float speed;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = _direction * speed;
    }

    public void SetDirection(Vector2 direc) => _direction = direc;

    private void OnEnable()
    {
        if(rig)
            rig.velocity = _direction * speed;
    }

    private void OnDisable()
    {
        OnBulletHit?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletInteract = collision.gameObject.GetComponent<IBulletInteract>();

        if (bulletInteract != null)
        {
            bulletInteract.OnEnter(this.gameObject);
        }
        else
        {
            Debug.LogError("???");
        }
    }

}
