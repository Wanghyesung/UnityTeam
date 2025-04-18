using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    private SortedDictionary<eMonsterState, MonsterState> m_mapState;
    MonsterState m_pCurMonsterState;

    private Animator m_pAnimator;
    private Transform m_pTransform;
    private Rigidbody2D m_pRigidbody2D;

    private Player m_pTarget;

    private MonsterScript m_pMonster;

    public void ChanageState(eMonsterState _eState)
    {
        m_pCurMonsterState.Exit();

        MonsterState pState = FindState(_eState);
        if (pState == null)
            Trace.Assert(1 < 0, "해당 상태 없음");

        m_pCurMonsterState = pState;

        m_pCurMonsterState.Enter();
    }

    public void FSMUpdate()
    {
        if (m_pCurMonsterState != null)
        {
            m_pCurMonsterState.UpdateState();
        }
    }

    public void SetMonsterState(eMonsterState _eState)
    {
        //if(m_mapState.ContainsKey(_eState))
        MonsterState pState = FindState(_eState);
        if (pState != null)
            Trace.Assert(1 < 0, "이미 있는 상태");

        m_pCurMonsterState = m_mapState[_eState];
    }

    public void Init()
    {
        m_mapState = new SortedDictionary<eMonsterState, MonsterState>();

        m_pAnimator = GetMonster().GetComponent<Animator>();
        m_pTransform = GetMonster().GetComponent<Transform>();
        m_pRigidbody2D = GetMonster().GetComponent<Rigidbody2D>();
      
        {
            MonsterState pIdle = new MonsterIdle();
            pIdle.SetOwner(this);
            pIdle.Init();

            MonsterState pRun = new MonsterMove();
            pRun.SetOwner(this);
            pRun.Init();

            MonsterState pAttack = new MonsterAttackState();
            pAttack.SetOwner(this);
            pAttack.Init();

            MonsterHit pHit = new MonsterHit();
            pHit.SetOwner(this);
            pHit.Init();

            m_mapState.Add(eMonsterState.Idle, pIdle);
            m_mapState.Add(eMonsterState.Run, pRun);
            m_mapState.Add(eMonsterState.Attack, pAttack);
            m_mapState.Add(eMonsterState.Hit, pHit);

            m_pCurMonsterState = pIdle;
        }
    }

    public MonsterState FindState(eMonsterState _eMonsterState)
    {
        if (m_mapState.ContainsKey(_eMonsterState))
            return m_mapState[_eMonsterState];

        return null;
    }

    public void SetMonster(MonsterScript _pMonsterScript) { m_pMonster = _pMonsterScript; }
    public MonsterScript GetMonster() { return m_pMonster; }

    public Player GetTarget() { return m_pTarget; }
    public void SetTarget(Player _pPlayer) { m_pTarget = _pPlayer; }
    public Animator GetAnimator() { return m_pAnimator; }
    public Transform GetTransform() { return m_pTransform; }
    public Rigidbody2D GetRigidbody2D() { return m_pRigidbody2D; }

    public eMonsterState GetCurState() { return m_pCurMonsterState.GetState(); }
}