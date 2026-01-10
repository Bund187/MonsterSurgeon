using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputController : MonoBehaviour
{
   InputSystem_Profiles inputActions;

    void Awake()    
    {
        inputActions = new InputSystem_Profiles();
    }
    //void OnEnable()
    //{
    //    inputActions.Default.Enable(); // siempre activo
    //}
    void OnDisable()
    {
        inputActions.Disable();
    }

    // ---------- DART GUN ----------
    public void EnableDartGun()
    {
        DisableAllItemMaps();
        inputActions.DartGun.Enable();

        inputActions.DartGun.Aim.started += OnAim;
        inputActions.DartGun.Aim.canceled += OnReleaseAim;
        inputActions.DartGun.Shoot.performed += OnShoot;
        print("enable dartgun");
    }
    public void DisableDartGun()
    {
        inputActions.DartGun.Aim.performed -= OnAim; 
        inputActions.DartGun.Aim.performed -= OnReleaseAim;
        inputActions.DartGun.Shoot.performed -= OnShoot;
        inputActions.DartGun.Disable();
    }

    // ---------- BEAR TRAP ----------
    public void EnableTrap()
    {
        DisableAllItemMaps();
        inputActions.Enable();

        inputActions.Trap.SetTrap.performed += OnSetTrap;
    }

    public void DisableTrap()
    {
        inputActions.Trap.SetTrap.performed -= OnSetTrap;
        inputActions.Trap.Disable();
    }
    void DisableAllItemMaps()
    {
        DisableDartGun();
        DisableTrap();
    }

    // ---------- CALLBACKS ----------
    void OnAim(InputAction.CallbackContext ctx)
    {
        GetComponent<DartShooter>()?.Aim();
    }
    void OnReleaseAim(InputAction.CallbackContext ctx)
    {
        GetComponent<DartShooter>()?.ReleaseAim();
    }

    void OnShoot(InputAction.CallbackContext ctx)
    {
        GetComponent<DartShooter>()?.Shoot();
    }

    void OnSetTrap(InputAction.CallbackContext ctx)
    {
       // GetComponent<TrapPlacer>()?.EnterPlacementPreview();
    }
}
