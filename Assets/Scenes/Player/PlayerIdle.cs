using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    
    public PlayerIdle() : base(ePlayerState.Idle)
    {
        
    }

    public override void UpdateState()
    {
        float fX = GetOwner().GetHorizon();

        PlayerFSM pFSM = GetOwner();

        //이동 감지
        if (fX != 0.0f /*|| fY != 0.0f*/)
            pFSM.ChanageState(ePlayerState.Run);

        //임시
        else if (Input.GetButtonDown("Jump") && pFSM.GetPlayer().IsGround())
        {
            pFSM.ChanageState(ePlayerState.Jump);
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            pFSM.ChanageState(ePlayerState.Attack);
        }

        else if(Input.GetKey(KeyCode.N))
        {
            pFSM.ChanageState(ePlayerState.Defend);
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

    }

   

}
