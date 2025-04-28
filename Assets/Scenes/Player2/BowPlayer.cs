using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ref	������ ���� (���ο��� ���� ����)
out	�Լ����� ���� ��ȯ�ޱ� ���� ��¿�
var	�ڵ� Ÿ�� �߷� (������ Ÿ�ӿ� ������)
 */
public class BowPlayer : Player
{
   
    public override void Awake()
    {
        base.Awake();

        m_ActionOnDamage = SetDamage;
    }
    // Start is called before the first frame update
    public override void Start()
    {
       

        base.Start();
        {
            PlayerState pIdle = new PlayerIdle();
            m_pFSM.AddState(pIdle);

            PlayerState pRun = new PlayerRun();
            m_pFSM.AddState(pRun);

            PlayerJump pJump = new PlayerJump();
            m_pFSM.AddState(pJump);

            PlayerJumpEnd pJumpEnd = new PlayerJumpEnd();
            m_pFSM.AddState(pJumpEnd);

            PlayerHit pHit = new PlayerHit();
            m_pFSM.AddState(pHit);

            PlayerBowAttack pAttack = new PlayerBowAttack();
            m_pFSM.AddState(pAttack);

            PlayerDead pDead = new PlayerDead();
            pDead.SetAction(SetDead); //�Լ� ������?
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

        ePlayerState eState = m_pFSM.GetCurPlayerState();

        base.SetDamage(_vHitPoint, _iDamage);

        if (eState == ePlayerState.Hit || eState == ePlayerState.Roll)
            return;

        set_damage(_iDamage);

    }

    public override void CheckAttack()
    {
        if(Input.GetKey(KeyCode.M))
        {
            PlayerBowAttack pAttack = (PlayerBowAttack)m_pFSM.FindState(ePlayerState.Attack);

            pAttack.SetReady(true);
            GetComponent<Animator>().speed = 0f;
        }
    }

    public override void ActiveAttack()
    {
        GameObject pArrow = Pool.GPool.GetObject("Arrow", transform.position);
      
        pArrow.GetComponent<PlayerAttackObj>().SetDeleteObject(true);

        Transform pAttackTr = pArrow.GetComponent<Transform>();
        Transform pTr = GetComponent<Transform>();
        
        //����
        Vector3 vPlayerScale = pTr.localScale;
        float fDir = vPlayerScale.x > 0 ? 1.0f : -1.0f;

        PlayerBowAttack pAttack = (PlayerBowAttack)m_pFSM.FindState(ePlayerState.Attack);
        //�ӵ�
        float fSpeed = pAttack.GetArrowSpeed();
        Vector2 vVel = new Vector2(fDir * fSpeed, 0.0f);
        pArrow.GetComponent<Rigidbody2D>().velocity = vVel;


        //ȸ��
        Vector3 vScale = pAttackTr.localScale;
        vScale.x *= -fDir;
        pAttackTr.localScale = vScale;
    }
    public override void UnActive()
    {
        
    }

}
