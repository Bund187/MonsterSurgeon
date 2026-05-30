using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float speed;

    public void TakeDamage(float playerAttack)
    {
        life -= playerAttack;
        print("Enemy life= " + life);
        if (life <= 0) Dead();
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

}
