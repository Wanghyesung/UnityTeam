using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : PlayerState
{
    private float m_fRollPower = 7.0f;
    private float m_fDrag = 4.0f;
    public PlayerRoll() : base(ePlayerState.Roll)
    {

    }

    public override void UpdateState()
    {
        
    }

    public override void Enter()
    {
        GetOwner().GetAnimator().SetBool("bRoll", true);

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = m_fDrag;  
            
        //������ �´��� ������ �´��� �б� ó��
        float fDir = GetOwner().GetHorizon();
        //pRigidbody.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
        pRigidbody.AddForce(new Vector2(fDir * m_fRollPower,0.0f), ForceMode2D.Impulse);
    }
    public override void Exit()
    {
        GetOwner().GetAnimator().SetBool("bRoll", false);
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.drag = 0.0f;
        pRigidbody.velocity = Vector2.zero;
    }
    public override void Init()
    {

    }

}
