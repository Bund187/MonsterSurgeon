using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
        
    private PlayerInputActions input;

    private void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Player.Enable();

        // Solo para ejes continuos, y aun así es opcional
        input.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => Move = Vector2.zero;

        input.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += _ => Look = Vector2.zero;
    }
    private void OnDisable()
    {
        input.Player.Disable();
    }

    // Botones: no te suscribes, los consultas cuando quieras
    //public bool JumpPressedThisFrame() => input.Player.Jump.WasPressedThisFrame();
    public bool InteractPressedThisFrame() => input.Player.Interact.WasPressedThisFrame();
    public bool InventoryPressedThisFrame() => input.Player.Inventory.WasPressedThisFrame();
    public bool AttackPressedThisFrame() => input.Player.Attack.WasPressedThisFrame();
    public bool SpellPressedThisFrame() => input.Player.Spell.WasPressedThisFrame();

    //public bool SprintHeld() => input.Player.Sprint.IsPressed();
    //public bool CrouchHeld() => input.Player.Crouch.IsPressed();
}
