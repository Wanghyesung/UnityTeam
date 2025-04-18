using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DefendEnter : PlayerState
{
    float m_fHitPower;
    float m_fHitDrag;
    public Player_DefendEnter() : base(ePlayerState.DefendEnter)
    {

    }
    public override void Init()
    {
        m_fHitPower = 4.0f;
        m_fHitDrag = 8.0f;
        
    }

    public override void UpdateState()
    {

    }

    public override void Enter()
    { 
        GetOwner().GetAnimator().SetTrigger("bDefendEnter");

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = m_fHitDrag;

        //땅에서 맞는지 위에서 맞는지 분기 처리
        Vector2 vDir = GetOwner().GetPlayer().GetHitDir();
        //땅에서 맞는지 위에서 맞는지 분기 처리
        float fDir = vDir.x > 0.0f ? 1 : -1;
      
        pRigidbody.velocity = Vector2.zero; // 기존 속도 초기화
        pRigidbody.AddForce(new Vector2(fDir * m_fHitPower, 0.0f), ForceMode2D.Impulse);
    }
    public override void Exit()
    {
        GetOwner().GetPlayer().SetHit(false);
        GetOwner().GetAnimator().SetBool("bDefend", false);

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = 0.0f;
        pRigidbody.velocity = Vector2.zero;

    }
}
