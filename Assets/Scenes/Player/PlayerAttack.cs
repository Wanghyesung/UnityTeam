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
        m_fComboTime = 0.2f; //1�� �ȿ� ������  
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
        //���߿� ���콺 ���� Ű�� ����
        if(Input.GetKeyDown(KeyCode.M))
        {
            m_bCombo = true;
            m_fCurComboTime = 0.0f;
        }
    }

    //�ִϸ��̼� �̺�Ʈ Ȱ�� (�Լ� ������)
    public void Attack()
    {
        //1. �浹ü Ȱ��
        //GetOwner().GetAttackObj().Coll().enable = true;
        //2. �浹ü ���� �ȿ��� OnCollEenter�� ���� ü�� or manager���� ó��
    }
   
    public bool IsCombo() { return m_bCombo; }
}
