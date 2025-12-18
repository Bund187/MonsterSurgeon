using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(string animation)
    {
        anim.Play(animation);
    }
}
