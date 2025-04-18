using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonsterState
{
    private float m_fFindRange;
    private float m_fAttackRange;

    public MonsterIdle() : base(eMonsterState.Idle)
    {

    }
    public override void UpdateState()
    {
        Vector3 vDiff = GetOwner().GetMonster().GetDiff();
        float fDiff = Math.Abs(vDiff.x);
        if(fDiff <= m_fAttackRange)
        {
            GetOwner().ChanageState(eMonsterState.Attack);
            return;
        }
        else if(fDiff <= m_fFindRange)
        {
            GetOwner().ChanageState(eMonsterState.Run);
            return;
        }


    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
    public override void Init()
    {
        m_fFindRange = 3.0f;
        m_fAttackRange = 2.0f;
    }

}
