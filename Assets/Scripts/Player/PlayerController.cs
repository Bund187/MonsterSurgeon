using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] AK.Wwise.Event wwiseWalkSound;
    [SerializeField] float speedMovement;
    [SerializeField] GameObject inventoryCanvas;

    public enum FacingDirection { Up, Down, Left, Right }
    public GameObject[] attackAnimations;

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
                attackAnimations[0].SetActive(true);
                DeactivateAnimations(0);
                break;
            case FacingDirection.Down:
                attackAnimations[1].SetActive(true);
                DeactivateAnimations(1);
                break;
            case FacingDirection.Left:
                attackAnimations[2].SetActive(true);
                DeactivateAnimations(2);
                break;
            case FacingDirection.Up:
                attackAnimations[3].SetActive(true);
                DeactivateAnimations(3);
                break;
        }
    }

    public void DeactivateAnimations(int noDeactivate)
    {
        for(int i=0; i< attackAnimations.Length; i++)
        {
            if (i != noDeactivate)
            {
                attackAnimations[i].SetActive(false);
            }
        }
    }
}