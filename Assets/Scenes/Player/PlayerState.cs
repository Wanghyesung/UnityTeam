using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//abstract
public abstract class PlayerState
{
    private PlayerFSM m_pPlayerFSM;
    private ePlayerState m_ePlayerState;
    public PlayerState(ePlayerState _eState) 
    {
        m_ePlayerState = _eState;
    }

    public abstract void UpdateState();
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Init();

    public void SetOwner(PlayerFSM _pFSM) { m_pPlayerFSM = _pFSM; }
    public PlayerFSM GetOwner() { return m_pPlayerFSM; }

    public ePlayerState GetState() { return m_ePlayerState; }

}
