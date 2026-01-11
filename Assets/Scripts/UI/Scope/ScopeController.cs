using UnityEngine;
using UnityEngine.InputSystem;

public class ScopeController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] RectTransform scopeUI;

    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera scopeCamera;

    [Header("Stick")]
    [SerializeField] float stickSpeed = 800f;
    [SerializeField] float stickDeadzone = 0.15f;

    [Header("Clamp")]
    [SerializeField] Vector2 padding = new Vector2(20, 20);

    private PlayerInputActions actions;
    private Vector2 scopeScreenPos;

    private enum AimMode { Pointer, Stick }
    private AimMode mode = AimMode.Pointer;

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        actions.Enable();

        // Arranca donde esté el ratón (o el puntero)
        Vector2 startPos = actions.Player.Pointer.ReadValue<Vector2>();

        // Si por alguna razón viene (0,0) (raro, pero puede pasar al iniciar),
        // lo dejamos en el centro como fallback.
        if (startPos == Vector2.zero)
            startPos = new Vector2(Screen.width / 2f, Screen.height / 2f);

        scopeScreenPos = startPos;
        scopeUI.position = scopeScreenPos;
        MoveScopeCamera(scopeScreenPos);
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Update()
    {
        Vector2 pointerPos = actions.Player.Pointer.ReadValue<Vector2>();
        Vector2 look = actions.Player.Look.ReadValue<Vector2>();

        bool stickActive = look.sqrMagnitude > (stickDeadzone * stickDeadzone);

        // Si el stick se usa, cambiamos a modo stick
        if (stickActive)
            mode = AimMode.Stick;

        // Si no hay stick activo, por defecto usamos puntero SIEMPRE
        if (!stickActive)
            mode = AimMode.Pointer;

        if (mode == AimMode.Pointer)
        {
            scopeScreenPos = pointerPos;
        }
        else // Stick
        {
            scopeScreenPos += look * stickSpeed * Time.deltaTime;
        }

        // Clamp
        scopeScreenPos.x = Mathf.Clamp(scopeScreenPos.x, padding.x, Screen.width - padding.x);
        scopeScreenPos.y = Mathf.Clamp(scopeScreenPos.y, padding.y, Screen.height - padding.y);

        // Apply
        scopeUI.position = scopeScreenPos;
        MoveScopeCamera(scopeScreenPos);
    }

    private void MoveScopeCamera(Vector2 screenPos)
    {
        Vector3 world = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
        world.z = scopeCamera.transform.position.z;
        scopeCamera.transform.position = world;
    }
}
