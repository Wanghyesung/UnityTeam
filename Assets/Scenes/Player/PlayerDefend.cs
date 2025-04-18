using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : PlayerState
{
    public PlayerDefend() : base(ePlayerState.Defend)
    {

    }
    public override void Init()
    {

    }

    public override void UpdateState()
    {
        if(!Input.GetKey(KeyCode.N))
        {
            GetOwner().ChanageState(ePlayerState.Idle);
        }
    }

    public override void Enter()
    {
        GetOwner().GetAnimator().SetBool("bDefend", true);

        //todo : 방어력을 올린다
    }
    public override void Exit()
    {
        if(!GetOwner().GetPlayer().IsHit())
            GetOwner().GetAnimator().SetBool("bDefend", false);

        //다시 복구
    }
}
