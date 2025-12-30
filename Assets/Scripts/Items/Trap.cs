using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usables/Trap")]
public class Trap : UsableItem
{
    public override void OnEquipped(PlayerManager player)
    {
        //Se activa la animación del player de disparar
        //player.ChangeAnimation("shoot");
        Debug.Log("Se equipa trampa");
    }

    public override void Use(PlayerManager player)
    {
        
    }
}
