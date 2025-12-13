using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    //[SerializeField] AK.Wwise.Event wwiseWalkSound;
    [SerializeField] float speedMovement;

    private PlayerInputReader input;
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private Vector2 moveInput;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInputReader>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Vector2 movement = input.Move;
        transform.Translate(movement * speedMovement * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}