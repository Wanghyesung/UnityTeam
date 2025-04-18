using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : PlayerState
{
    private float m_fHitPower;
    public PlayerHit() : base(ePlayerState.Hit)
    {

    }

    public override void UpdateState()
    {
       
    }

    public override void Enter()
    {
        GetOwner().GetAnimator().SetTrigger("bHit");

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.velocity = new Vector2(0.0f, 0.0f);
        pRigidbody.drag = 4.0f;
        m_fHitPower = 7.0f;

        Vector2 vDir = GetOwner().GetPlayer().GetHitDir();
        //땅에서 맞는지 위에서 맞는지 분기 처리
        float fDir = vDir.x > 0.0f ? 1 : -1;

        pRigidbody.velocity = new Vector2(0.0f,0.0f);// 기존 속도 초기화
        pRigidbody.AddForce(new Vector2(fDir * m_fHitPower, 0.0f), ForceMode2D.Impulse);
    }
    public override void Exit()
    {
        GetOwner().GetPlayer().SetHit(false);
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = 0.0f;
        pRigidbody.velocity = new Vector2(0.0f, 0.0f);

    }
    public override void Init()
    {

    }

    public void SetHitPower(float _fHitPower) { m_fHitPower = _fHitPower; }
}
