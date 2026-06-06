using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly float AttackCooldown = 0.2f;

    [SerializeField] float speedMovement;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] SpellBase spellBase;
    public enum FacingDirection { Right = 0, Down = 1, Left = 2, Up = 3 }
    public GameObject[] attackAnimation;
    public Transform[] attackPositions;
    public Transform spellAim;

    private PlayerInputReader input;
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool inventoryActive;
    private bool movementIsBlocked;
    private PlayerAnimationController animationController;
    private float lastAttackTime;
    public bool MovementIsBlocked { get => movementIsBlocked; set => movementIsBlocked = value; }

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
        HandleSpellInput();
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
        //Cooldown
        if (Time.time < lastAttackTime + AttackCooldown) return;

        movementIsBlocked = true;
        int index = (int)GetDirection(animationController.FacingDirection);

        SetActiveAttackAnimation(index);
        animationController.TriggerAttackAnimation();
        lastAttackTime = Time.time;
    }

    private void SetActiveAttackAnimation(int activeIndex)
    {
        for (int i = 0; i < attackAnimation.Length; i++)
        {
            attackAnimation[i].SetActive(i == activeIndex);
        }
    }

    //Spell Attack
    public void HandleSpellInput()
    {
        if (input.SpellPressedThisFrame())
            ThrowSpell();
    }

    public void ThrowSpell()
    {
        spellBase.CastSpell(spellAim);
        animationController.TriggerSpellAnimation();
    }
}