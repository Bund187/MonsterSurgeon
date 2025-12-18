using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] AK.Wwise.Event wwiseWalkSound;
    [SerializeField] float speedMovement;
    [SerializeField] GameObject inventoryCanvas;

    private PlayerInputReader input;
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool inventoryActive;
    private bool movementIsBlocked;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInputReader>();
    }

    void FixedUpdate()
    {
        if(!movementIsBlocked)
            Move();
    }
    private void Update()
    {
        HandleInventoryInput();
    }

    void Move()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Vector2 movement = input.Move;
        transform.Translate(movement * speedMovement * Time.deltaTime);
    }  
    
    public void BlockMovement()
    {
        movementIsBlocked=!movementIsBlocked;
    }

    public void HandleInventoryInput()
    {
        if (input.InventoryPressedThisFrame())
            ToggleInventory();
    }
    void ToggleInventory()
    {
        BlockMovement();
        inventoryActive = !inventoryActive;
        inventoryCanvas.SetActive(inventoryActive);
    }
}