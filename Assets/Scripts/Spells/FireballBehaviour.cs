using UnityEngine;
using UnityEngine.UIElements;

public class FireballBehaviour : MonoBehaviour
{

    private Vector2 direction;
    private float damage;
    private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float damage, float speed)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.speed = speed;
        rb.linearVelocity = this.direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
            target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
