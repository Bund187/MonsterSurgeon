using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usables/Dart Gun")]
public class DartGun : UsableItem
{
    public override void OnEquipped(PlayerManager player)
    {
        //Se activa la animación del player de disparar
        //player.ChangeAnimation("shoot");
    }

    public override void Use(PlayerManager player)
    {
        player.GetComponent<DartShooter>().Shoot();
    }
}
