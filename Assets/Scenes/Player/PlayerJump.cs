using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerState
{
    float m_fHorPower;
    float m_fVerPower;
    public PlayerJump() : base(ePlayerState.Jump)
    {

    }

    public override void UpdateState()
    {
        //여기서 체크
        if (GetOwner().GetPlayer().IsGround())
        {
            PlayerState pPlayerJumpEnd = GetOwner().FindState(ePlayerState.Jump_End);
            if (pPlayerJumpEnd == null)
                GetOwner().ChanageState(ePlayerState.Idle);
            else
                GetOwner().ChanageState(ePlayerState.Jump_End);
        }
          
    }
    public override void Init()
    {
        m_fHorPower = 10.0f;
        m_fVerPower = 2.0f;
       
    } 

    public override void Enter()
    {
       
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        Vector3 vDir = GetOwner().GetDir();
        vDir.x *= m_fVerPower;
        vDir.y = m_fHorPower;
        pRigidbody.velocity = Vector2.zero; // 기존 속도 초기화
        pRigidbody.AddForce(vDir, ForceMode2D.Impulse);

        GetOwner().GetPlayer().SetGround(false);
        GetOwner().GetAnimator().SetBool("bJump", true);
    }
    public override void Exit()
    {
        GetOwner().GetAnimator().SetBool("bJump", false);
    }

}
