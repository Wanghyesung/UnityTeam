using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerRun : PlayerState
{
    float m_fSpeed;
    
    // Start is called before the first frame update
    public PlayerRun() : base(ePlayerState.Run)
    {
        m_fSpeed = 3.0f;
    }

    public override void UpdateState()
    {
        float fX = GetOwner().GetHorizon();

        if (chanage_state(fX))
            return;

        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        Vector2 tVelocity = pRigidbody.velocity;
        tVelocity.x = fX * m_fSpeed;
        pRigidbody.velocity = tVelocity;
    }

    public override void Enter()
    {
        GetOwner().GetAnimator().SetBool("bRun", true);
    }
    public override void Exit()
    {
        Rigidbody2D pRigidbody = GetOwner().GetRigidbody2D();
        pRigidbody.velocity = Vector2.zero;

        GetOwner().GetAnimator().SetBool("bRun", false);
    }
    public override void Init()
    {
        
    }


    private bool chanage_state(float _fX)
    {
        if (_fX == 0.0f)
        {
            GetOwner().ChanageState(ePlayerState.Idle);
            return true;
        } 

        else if (Input.GetKeyDown(KeyCode.M))
        {
            GetOwner().ChanageState(ePlayerState.Attack);
            return true;
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            GetOwner().ChanageState(ePlayerState.Roll);
            return true;
        }

        else if (Input.GetButtonDown("Jump"))
        {
            GetOwner().ChanageState(ePlayerState.Jump);
            return true;
        }

        return false;
    }
}
