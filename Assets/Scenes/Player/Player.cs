using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerFSM m_pFSM;
    protected GameObject m_pPlayerAttack;
   
    protected Vector2 m_vHitDir;
   
    protected bool m_bGround;
    protected bool m_bLastGround;
    protected bool m_bHit;

    protected tPlayerInfo m_tPlayerInfo;
    protected HPBar m_pHP;

    public Action<Vector2, int> m_ActionOnDamage;
   

    public virtual void Awake()
    {
        m_bGround = false;
        m_bHit = false;

        m_tPlayerInfo.m_fMaxHP = 100;
        m_tPlayerInfo.m_fHP = 100;
        m_pHP = FindObjectOfType<HPBar>();
       
    }
   
    public virtual void Start()
    {
        m_pFSM = new PlayerFSM();
        m_pFSM.SetPlayer(this);
        m_pFSM.Init();

        m_pHP.Init(m_tPlayerInfo.m_fMaxHP);//HP Awake가 먼저 실행될때까지 ㄱㄷ
    }

    // Update is called once per frame
    public virtual void Update()
    { 
        m_pFSM.FSMUpdate();
    }

    public bool IsGround() { return m_bGround; }

    public void SetGround(bool _bGround) { m_bGround = _bGround; }

    public bool IsLastGround() { return m_bLastGround; }

    public void SetLastGround(bool _bGround) { m_bLastGround = _bGround; }

    public void SetHit(bool _bHit){  m_bHit = _bHit;}
    public bool IsHit() { return m_bHit; }

    public virtual void SetDamage(Vector2 _vHitPoint, int _iDamage) 
    {
        ePlayerState eState = m_pFSM.GetCurPlayerState();
       
        if (eState == ePlayerState.Hit || eState == ePlayerState.Roll)
            return;

        //hp -= _iDamage;
        Vector3 vWorldPos = GetComponent<Transform>().position;
        Vector2 vDir = new Vector2(vWorldPos.x, vWorldPos.y) - _vHitPoint ;
        m_vHitDir = vDir;

    }
    public Vector2 GetHitDir() { return m_vHitDir; }

  
    public void ChangeIdle()
    {
        m_pFSM.ChanageState(ePlayerState.Idle);
    }

    public virtual void CheckAttack()
    {
        
    }

    public virtual void ActiveAttack()
    {
     
    }
    public virtual void UnActive()
    {
      
    }

    protected void SetDead()
    {
        //이벤트 매니저(다시 처음으로) , 게임오브젝트 매니저에서 UI로드(다시하기, ..), 지금은 걍 비활성
        gameObject.SetActive(false);
    }

    protected void set_damage(int _iDamage)
    {
        m_bHit = true;

        //캐릭마다 데미지 다르게
        m_tPlayerInfo.m_fHP = Mathf.Max(m_tPlayerInfo.m_fHP - _iDamage, 0);
        m_pHP.SetHP(m_tPlayerInfo.m_fHP, m_tPlayerInfo.m_fMaxHP);

        if (m_tPlayerInfo.m_fHP <= 0)
            m_pFSM.ChanageState(ePlayerState.Dead);/////
        else
            m_pFSM.ChanageState(ePlayerState.Hit);
    }
}
