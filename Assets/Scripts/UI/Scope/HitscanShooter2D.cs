using UnityEngine;

public class ScopeHitScan2D : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Camera mainCamera;
    [SerializeField] RectTransform scopeUI; // el UI que está centrado en la X

    [Header("Hit Test")]
    [SerializeField] LayerMask hittableLayers; // Enemy + Props + Environment
    [SerializeField] float radiusWorld = 0.08f; // 0 = OverlapPoint, >0 = OverlapCircle

    private ScopeController scopeController;

    private void Start()
    {
        scopeController = GetComponent<ScopeController>();
    }
    public void Fire()
    {
        Vector2 aimWorld = scopeController.GetAimWorldPoint2D();

        Collider2D col = Physics2D.OverlapCircle(aimWorld, radiusWorld, hittableLayers);

        if (!col) return;

        GameObject go = col.gameObject;

        // 1) Info del objeto impactado
        Debug.Log($"Disparo a: {go.name} | Tag: {go.tag} | Layer: {LayerMask.LayerToName(go.layer)}");

        // 2) Versatilidad por interfaz (recomendado)
        //if (go.TryGetComponent<IDamageable>(out var dmg))
        //{
        //    dmg.TakeHit(1, aimWorld);
        //    return;
        //}

        // 3) O por tag (rápido)
        if (go.CompareTag("Enemy"))
        {
            Destroy(go); // ejemplo simple
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!mainCamera || !scopeUI) return;
        Vector2 aimWorld = scopeController.GetAimWorldPoint2D();
        Gizmos.DrawWireSphere(aimWorld, Mathf.Max(0.001f, radiusWorld));
    }
#endif
}
