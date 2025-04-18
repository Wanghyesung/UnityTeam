using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonsterState
{
    private float m_fHitPower;

    public MonsterHit() : base(eMonsterState.Hit)
    {

    }
    public override void UpdateState()
    {
        
    }
    public override void Enter()
    {
        GetOwner().GetAnimator().SetTrigger("bHit");

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = 3.0f;
        
        Vector2 vDir = GetOwner().GetMonster().GetHitDir();
        //������ �´��� ������ �´��� �б� ó��
        float fDir = vDir.x > 0.0f ? 1 : -1;

        pRigidbody.velocity = new Vector2(0f, 0f);  // ���� �ӵ� �ʱ�ȭ
        //pRigidbody.angularVelocity = 0f;
        pRigidbody.AddForce(new Vector2(fDir * m_fHitPower, 0.0f), ForceMode2D.Impulse);
    }
    public override void Exit()
    {
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = 1.0f;
    }
    public override void Init()
    {
        m_fHitPower = 3.0f;
    }
}
