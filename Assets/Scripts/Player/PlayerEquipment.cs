using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    public void EquipItem(UsableItem item)
    {
        item.OnEquipped(playerManager);
    }

}
