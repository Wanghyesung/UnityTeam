using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterState
{
    public MonsterAttackState() : base(eMonsterState.Attack)
    {

    }
    public override void UpdateState()
    {

    }
    public override void Enter()
    {
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        
        GetOwner().GetAnimator().SetBool("Attack", true);
    }
    public override void Exit()
    {
        GetOwner().GetAnimator().SetBool("Attack", false);

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
      
    }
    public override void Init()
    {

    }
}
