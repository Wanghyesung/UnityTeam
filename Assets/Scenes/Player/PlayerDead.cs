using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDead : PlayerState
{
    private Action pDeadAction;

    public PlayerDead() : base(ePlayerState.Dead)
    {

    }
    public override void Init()
    {
        
    }
    
    public void SetAction(Action _pDeadAction)
    {
        pDeadAction = _pDeadAction;
    }

    public override void UpdateState()
    {
        
    }
    
    public override void Enter()
    {
        GetOwner().GetAnimator().SetTrigger("bDead");
    }
    public override void Exit()
    {
        pDeadAction?.Invoke();
        //여기서 끝내든지
    }
}