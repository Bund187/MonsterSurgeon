using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    // Hashes son más eficientes que strings
    private static readonly string MoveX = "Walk_X";
    private static readonly string MoveY = "Walk_Y";
    private static readonly string IsMoving = "isMoving";

    // Guarda la última dirección para el idle correcto
    private Vector2 facingDirection = Vector2.down;

    public Vector2 FacingDirection { get => facingDirection; set => facingDirection = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(Vector2 movement)
    {
        bool moving = movement.sqrMagnitude > 0.01f;
       
        animator.SetBool(IsMoving, moving);
        if (moving)
        {
            // Normalizar evita que las diagonales sean más rápidas visualmente
            facingDirection = movement.normalized;
            animator.SetInteger(MoveX, (int)facingDirection.x);
            animator.SetInteger(MoveY, (int)facingDirection.y);
        }
        else
        {
            // En idle, mantiene la última dirección mirando al frente
            animator.SetInteger(MoveX, (int)facingDirection.x);
            animator.SetInteger(MoveY, (int)facingDirection.y);
        }
    }
}