using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetStunned(bool stun)
    {
        //TODO : ���� �浹 �� ����
        anim.SetBool("stun", stun);
    }
}
