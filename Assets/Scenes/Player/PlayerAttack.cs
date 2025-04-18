using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    bool m_bCombo;
    float m_fComboTime;
    float m_fCurComboTime;
    public PlayerAttack() : base(ePlayerState.Attack)
    {

    }

    public override void UpdateState()
    {
        check_combo_key();

        check_combo_time();
    }

    public override void Enter()
    {
        m_bCombo = true;
        m_fCurComboTime = 0.0f;

        GetOwner().GetAnimator().SetBool("bCombo", m_bCombo);
    }
    public override void Exit()
    {
        m_bCombo = false;
        GetOwner().GetAnimator().SetBool("bCombo", m_bCombo);
    }
    public override void Init()
    {
        m_fComboTime = 0.2f; //1초 안에 누르기  
    }

    private void check_combo_time()
    {
        m_fCurComboTime += Time.deltaTime;
        if(m_fComboTime < m_fCurComboTime)
        {
            m_bCombo = false;
        }
    }

    private void check_combo_key()
    {
        //나중에 마우스 왼쪽 키로 변경
        if(Input.GetKeyDown(KeyCode.M))
        {
            m_bCombo = true;
            m_fCurComboTime = 0.0f;
        }
    }

    //애니메이션 이벤트 활용 (함수 포인터)
    public void Attack()
    {
        //1. 충돌체 활성
        //GetOwner().GetAttackObj().Coll().enable = true;
        //2. 충돌체 로직 안에서 OnCollEenter로 몬스터 체력 or manager에서 처리
    }
   
    public bool IsCombo() { return m_bCombo; }
}
