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

    [Header("Clamp")]
    [SerializeField] Vector2 padding = new Vector2(20, 20);

    private PlayerInputActions actions;

    private Vector2 scopeScreenPos;

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        actions.Enable();

        // Inicializa la mirilla en el centro
        scopeScreenPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Update()
    {
        Vector2 pointerPos = actions.Player.Pointer.ReadValue<Vector2>();
        Vector2 look = actions.Player.Look.ReadValue<Vector2>();

        bool usingMouse = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero;
        bool usingStick = look.sqrMagnitude > 0.001f;

        // 1) Decide quién manda
        if (usingMouse)
        {
            scopeScreenPos = pointerPos;
        }
        else if (usingStick)
        {
            scopeScreenPos += look * stickSpeed * Time.deltaTime;
        }

        // 2) Clamp a pantalla
        scopeScreenPos.x = Mathf.Clamp(scopeScreenPos.x, padding.x, Screen.width - padding.x);
        scopeScreenPos.y = Mathf.Clamp(scopeScreenPos.y, padding.y, Screen.height - padding.y);

        // 3) Mueve la UI
        scopeUI.position = scopeScreenPos;

        // 4) Mueve la cámara de la lupa
        Vector3 world = mainCamera.ScreenToWorldPoint(
            new Vector3(scopeScreenPos.x, scopeScreenPos.y, 0f)
        );
        world.z = scopeCamera.transform.position.z;
        scopeCamera.transform.position = world;
    }
}
