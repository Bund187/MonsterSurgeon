using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponentInParent<Inventory>();
            if (!inventory) return;

            inventory.Add(item);

            Destroy(this.gameObject);
        }
    }
}
