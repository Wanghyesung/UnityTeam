using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState
{
    // Start is called before the first frame update

    private MonsterFSM m_pMonsterFSM;
    private eMonsterState m_eMonsterState;
    public MonsterState(eMonsterState _eState)
    {
        m_eMonsterState = _eState;
    }

    public abstract void UpdateState();
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Init();

    public void SetOwner(MonsterFSM _pFSM) { m_pMonsterFSM = _pFSM; }
    public MonsterFSM GetOwner() { return m_pMonsterFSM; }

    public eMonsterState GetState() { return m_eMonsterState; }

}
