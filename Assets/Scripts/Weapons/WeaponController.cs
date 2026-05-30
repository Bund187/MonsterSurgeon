using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private int power;
    [SerializeField] private string type;

    private Collider2D hitCollider;

    private void Start()
    {
        hitCollider = GetComponent<Collider2D>();
    }

    public void EnableHitbox() => hitCollider.enabled = true;
    public void DisableHitbox()
    {
        hitCollider.enabled = false;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
            target.TakeDamage(attack);
    }  
}
