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
        // TODO: 몬스터 충돌 시 기절 애니메이션 재생
        anim.SetBool("Stun", stun);
    }

    public void SetMoved(bool move)
    {
        // TODO: 플레이어가 이동할 때 이동 애니메이션 제어
        anim.SetBool("Move", move);
    }

    public void SetMap(bool map)
    {
        // TODO: 맵 연결 미니게임 애니메이션 재생
        anim.SetBool("Map", map);
    }

    public void SetClear(bool clear)
    {
        // TODO: 미니게임 클리어 시 클리어 애니메이션 재생
        anim.SetBool("Clear", clear);
    }

    public void SetFail(bool fail)
    {
        // TODO: 미니게임 실패 시 실패 애니메이션 재생
        anim.SetBool("Fail", fail);
    }

    public void SetWork(bool work)
    {
        // TODO: 미니게임 작업 시 작업 애니메이션 재생
        anim.SetBool("GameWork", work);
    }

    public void SetSitting(bool sitting)
    {
        // TODO: 미니게임 작업 시 작업 애니메이션 재생
        anim.SetBool("Sitting", sitting);
    }

    public void SetFight(bool fight)
    {
        // TODO: 미니게임 진상제압 전투 애니메이션 전환
        anim.SetBool("Fight", fight);
    }
}
