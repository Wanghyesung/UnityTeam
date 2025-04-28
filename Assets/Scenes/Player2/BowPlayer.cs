using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ref	참조로 전달 (내부에서 변경 가능)
out	함수에서 값을 반환받기 위한 출력용
var	자동 타입 추론 (컴파일 타임에 결정됨)
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
            pDead.SetAction(SetDead); //함수 포인터?
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
        
        //방향
        Vector3 vPlayerScale = pTr.localScale;
        float fDir = vPlayerScale.x > 0 ? 1.0f : -1.0f;

        PlayerBowAttack pAttack = (PlayerBowAttack)m_pFSM.FindState(ePlayerState.Attack);
        //속도
        float fSpeed = pAttack.GetArrowSpeed();
        Vector2 vVel = new Vector2(fDir * fSpeed, 0.0f);
        pArrow.GetComponent<Rigidbody2D>().velocity = vVel;


        //회전
        Vector3 vScale = pAttackTr.localScale;
        vScale.x *= -fDir;
        pAttackTr.localScale = vScale;
    }
    public override void UnActive()
    {
        
    }

}
