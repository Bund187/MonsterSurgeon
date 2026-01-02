using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator anim;

    public PlayerEquipment Equipment { get; private set; }
    public PlayerInputController InputController { get; private set; }
    private void Start()
    {
        anim = GetComponent<Animator>();
        Equipment = GetComponent<PlayerEquipment>();
        InputController = GetComponent<PlayerInputController>();
    }

    public void ChangeAnimation(string animation)
    {
        anim.Play(animation);
    }
}
