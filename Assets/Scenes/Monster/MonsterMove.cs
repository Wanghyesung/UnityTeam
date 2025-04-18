using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonsterState
{
    private float m_fMaxRanage;
    private float m_fMinRanage;
    private float m_fSpeed;
    public MonsterMove() : base(eMonsterState.Run)
    {

    }
    public override void UpdateState()
    {
        Vector3 vDiff =GetOwner().GetMonster().GetDiff();
        float fDiff = Math.Abs(vDiff.x);
        if(fDiff >= m_fMaxRanage)
        {
            GetOwner().ChanageState(eMonsterState.Idle);
            return;
        }
        if (fDiff <= m_fMinRanage)
        {
            GetOwner().ChanageState(eMonsterState.Attack);
            return;
        }
        vDiff.Normalize();
        float fXVector = vDiff.x * m_fSpeed;
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.velocity = new Vector2(fXVector, 0.0f);
    }
    public override void Enter()
    {
        GetOwner().GetAnimator().SetBool("Move", true);
    }
    public override void Exit()
    {
        GetOwner().GetAnimator().SetBool("Move", false);
        GetOwner().GetRigidbody2D().velocity = new Vector2(0.0f, 0.0f);
    }
    public override void Init()
    {
        m_fMaxRanage = 3.0f;
        m_fMinRanage = 2.0f;
        m_fSpeed = 2.0f;
    }
}
