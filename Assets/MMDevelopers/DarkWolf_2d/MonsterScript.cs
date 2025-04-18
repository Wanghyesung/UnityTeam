using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    Collider2D m_pCollider;
    Rigidbody2D m_pRigidbody;

    Player m_pTarget;
    private MonsterFSM m_pFSM;
    private GameObject m_pMonsterAttack;

    private Vector3 m_vDiff;
    private Vector3 m_vHitDir;

    bool m_bHit;
    private void Awake()
    {
        m_bHit = false;
    }
   
    private void Start()
    {
        m_pTarget = FindObjectOfType<Player>();
        m_pMonsterAttack = FindObjectOfType<MonsterAttack>().gameObject;
        m_pMonsterAttack.SetActive(false);

        m_pFSM = new MonsterFSM();
        m_pFSM.SetMonster(this);
        m_pFSM.Init();
    }

    // Update is called once per frame
    private void Update()
    {
        update_dir();

        m_pFSM.FSMUpdate();
    }

    
    private void update_dir()
    {
        eMonsterState eState = m_pFSM.GetCurState();
        if (eState == eMonsterState.Attack || eState == eMonsterState.Hit)
            return;

        Transform pTr = m_pFSM.GetTransform();
        m_vDiff = m_pTarget.GetComponent<Transform>().position - pTr.position;
        if(m_vDiff.x > 0.0f)
            pTr.localScale = new Vector3(-2.0f,2.0f,2.0f);
        else
            pTr.localScale = new Vector3(2.0f, 2.0f, 2.0f);
    }

    public Vector3 GetDiff() { return m_vDiff; }

    public void ChanageIdle()
    {
        m_pFSM.ChanageState(eMonsterState.Idle);
    }

    public void ActiveAttack()
    { 
        m_pMonsterAttack.SetActive(true);


        Transform pTr = GetComponent<Transform>();
        Vector3 vPosition = pTr.position;
        Transform pAttackTr = m_pMonsterAttack.GetComponent<Transform>();
        float fDir = pTr.localScale.x > 0 ? -1.0f : 1.0f;
        
        float fOffst = vPosition.x + fDir;
        pAttackTr.localPosition = new Vector3(fOffst, vPosition.y, vPosition.z);

    }
    public void UnActive() 
    {
        m_pMonsterAttack.SetActive(false);
        m_pMonsterAttack.GetComponent<Transform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetDamage()
    {
        eMonsterState eState = m_pFSM.GetCurState();

        Vector3 vWorldPos = GetComponent<Transform>().position;
       
        float fDir = GetComponent<Transform>().localScale.x > 0 ? 1.0f : -1.0f;
        m_vHitDir = new Vector2(fDir,0.0f);

        m_bHit = true;
       
        //todo : 나중에 데미지 들어오면 여기서 분기처리하든가 매니저에서 관리
        m_pFSM.ChanageState(eMonsterState.Hit);

    }


    public Vector3 GetHitDir() { return m_vHitDir; }
}
