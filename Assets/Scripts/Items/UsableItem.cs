using UnityEngine;

public abstract class UsableItem : Item
{
    // Se llama al cerrar inventario si queda seleccionado
    public virtual void OnEquipped(PlayerManager player) { }

    // Se llama cuando el jugador pulsa el botón de usar
    public abstract void Use(PlayerManager player);
}
