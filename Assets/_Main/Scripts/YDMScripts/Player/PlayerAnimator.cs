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
        // TODO: ���� �浹 �� ���� �ִϸ��̼� ���
        anim.SetBool("Stun", stun);
    }

    public void SetMoved(bool move)
    {
        // TODO: �÷��̾ �̵��� �� �̵� �ִϸ��̼� ����
        anim.SetBool("Move", move);
    }

    public void SetMap(bool map)
    {
        // TODO: �� ���� �̴ϰ��� �ִϸ��̼� ���
        anim.SetBool("Map", map);
    }

    public void SetClear(bool clear)
    {
        // TODO: �̴ϰ��� Ŭ���� �� Ŭ���� �ִϸ��̼� ���
        anim.SetBool("Clear", clear);
    }

    public void SetFail(bool fail)
    {
        // TODO: �̴ϰ��� ���� �� ���� �ִϸ��̼� ���
        anim.SetBool("Fail", fail);
    }

    public void SetWork(bool work)
    {
        // TODO: �̴ϰ��� �۾� �� �۾� �ִϸ��̼� ���
        anim.SetBool("GameWork", work);
    }

    public void SetSitting(bool sitting)
    {
        // TODO: �̴ϰ��� �۾� �� �۾� �ִϸ��̼� ���
        anim.SetBool("Sitting", sitting);
    }

    public void SetFight(bool fight)
    {
        // TODO: �̴ϰ��� �������� ���� �ִϸ��̼� ��ȯ
        anim.SetBool("Fight", fight);
    }
}
