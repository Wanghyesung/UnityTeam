using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpEnd : PlayerState
{
   
    public PlayerJumpEnd() : base(ePlayerState.Jump_End)
    {

    }

    public override void UpdateState()
    {
        
    }
    public override void Init()
    {
   
    }

    public override void Enter()
    {
        GetOwner().GetAnimator().SetTrigger("bJumpEnd");
    }
    public override void Exit()
    {
      
    }

}