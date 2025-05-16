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
        //TODO : 몬스터 충돌 시 기절
        anim.SetBool("Stun", stun);
    }
    public void SetMoved(bool move)
    {
        //TODO : 몬스터 충돌 시 기절
        anim.SetBool("Move", move);
    }

}
