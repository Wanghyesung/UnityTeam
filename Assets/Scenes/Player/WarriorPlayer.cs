using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPlayer : Player
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        m_ActionOnDamage = SetDamage;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        m_pPlayerAttack = FindObjectOfType<PlayerAttackObj>().gameObject;
        m_pPlayerAttack.SetActive(false);

        base.Start();
        {
            PlayerState pIdle = new PlayerIdle();
            m_pFSM.AddState(pIdle);

            PlayerState pRun = new PlayerRun();
            m_pFSM.AddState(pRun);

            PlayerJump pJump = new PlayerJump();
            m_pFSM.AddState(pJump);

            PlayerAttack pAttack = new PlayerAttack();
            m_pFSM.AddState(pAttack);

            PlayerHit pHit = new PlayerHit();
            m_pFSM.AddState(pHit);

            PlayerRoll pRoll = new PlayerRoll();
            m_pFSM.AddState(pRoll);

            PlayerDefend pDefend = new PlayerDefend();
            m_pFSM.AddState(pDefend);

            Player_DefendEnter pDFEnter = new Player_DefendEnter();
            m_pFSM.AddState(pDFEnter);

            PlayerDead pDead = new PlayerDead();
            pDead.SetAction(SetDead);
            m_pFSM.AddState(pDead);

            m_pFSM.SetPlayerState(ePlayerState.Idle);

        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public override void SetDamage(Vector2 _vHitPoint, int _iDamage)
    {
        if (m_tPlayerInfo.m_fHP <= 0)
            return;

        base.SetDamage(_vHitPoint, _iDamage);

        ePlayerState eState = m_pFSM.GetCurPlayerState();

        if (eState == ePlayerState.Defend || eState == ePlayerState.DefendEnter)
        {
            m_pFSM.ChanageState(ePlayerState.DefendEnter);
            return;
        }

        set_damage(_iDamage);
    }

    public override void CheckAttack()
    {
        //나중에 템플릿으로 변경하기
        PlayerAttack pAttack = (PlayerAttack)m_pFSM.FindState(ePlayerState.Attack);

        if (!pAttack.IsCombo())
            m_pFSM.ChanageState(ePlayerState.Idle);
    }

    public override void ActiveAttack()
    {
        Transform pTr = GetComponent<Transform>();
        Vector3 vPosition = pTr.position;

        m_pPlayerAttack.SetActive(true);
        Transform pAttackTr = m_pPlayerAttack.GetComponent<Transform>();
        float fDir = pTr.localScale.x > 0 ? 1.0f : -1.0f;

        float fOffst = vPosition.x + fDir;
        pAttackTr.localPosition = new Vector3(fOffst, vPosition.y, vPosition.z);

    }
    public override void UnActive()
    {
        m_pPlayerAttack.SetActive(false);
        m_pPlayerAttack.GetComponent<Transform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
