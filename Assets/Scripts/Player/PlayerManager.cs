using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator anim;

    public PlayerEquipment Equipment { get; private set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        Equipment = GetComponent<PlayerEquipment>();
    }

    public void ChangeAnimation(string animation)
    {
        anim.Play(animation);
    }
}
