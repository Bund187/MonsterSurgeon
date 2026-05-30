using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] AK.Wwise.Event wwiseWalkSound;
    [SerializeField] float speedMovement;
    [SerializeField] GameObject inventoryCanvas;

    public enum FacingDirection { Up, Down, Left, Right }
    public GameObject attackAnimation;
    public Transform[] attackPositions;

    private PlayerInputReader input;
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool inventoryActive;
    private bool movementIsBlocked;
    private PlayerAnimationController animationController;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInputReader>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    void FixedUpdate()
    {
        if(!movementIsBlocked)
            Move();
    }
    private void Update()
    {
        HandleInventoryInput();
        HandleAttackInput();
    }
    FacingDirection GetDirection(Vector2 dir)
    {
        // Determina el eje dominante
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? FacingDirection.Right : FacingDirection.Left;
        else
            return dir.y > 0 ? FacingDirection.Up : FacingDirection.Down;
    }

    void Move()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Vector2 movement = input.Move;
        transform.Translate(movement * speedMovement * Time.deltaTime);

        animationController.UpdateAnimation(movement);
    }  
    
    public void BlockMovement()
    {
        movementIsBlocked=!movementIsBlocked;
    }

    //INVENTORY
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

    //MELEE ATTACK
    public void HandleAttackInput()
    {
        if (input.AttackPressedThisFrame())
            Attack();
    }

    public void Attack()
    {
        switch (GetDirection(animationController.FacingDirection))
        {
            case FacingDirection.Right:
                ShowAttackAnimation(0, false);
                break;
            case FacingDirection.Down:
                ShowAttackAnimation(1, true);
                break;
            case FacingDirection.Left:
                ShowAttackAnimation(2, true);
                break;
            case FacingDirection.Up:
                ShowAttackAnimation(3, false);
                break;
        }
    }

    public void ShowAttackAnimation(int attackIndex, bool flip)
    {
        attackAnimation.transform.position = attackPositions[attackIndex].transform.position;
        attackAnimation.transform.rotation = attackPositions[attackIndex].transform.rotation;
        attackAnimation.GetComponent<SpriteRenderer>().flipX = flip;
        attackAnimation.SetActive(true);
    }

}