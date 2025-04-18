using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : PlayerState
{
    private float m_fAttackDamage;
    private float m_fAttackSpeed;

    private float m_fMaxAttackDamage;
    private float m_fMaxAttackSpeed;

    private bool m_bReady;
    public PlayerBowAttack() : base(ePlayerState.Attack)
    {

    }

    public override void UpdateState()
    {
        if (m_bReady)
        {
            if (Input.GetKeyUp(KeyCode.M))
            {
                //다시 속도 복귀
                GetOwner().GetAnimator().speed = 1.0f;
                m_bReady = false;
            }

            charge();
        }
    }

    public override void Init()
    {
        m_fMaxAttackDamage = 10.0f;
        m_fMaxAttackSpeed = 15.0f;

        m_fAttackDamage = 5.0f;
        m_fAttackSpeed = 8.0f;
    }

    public override void Enter()
    {
        m_bReady = false;
        m_fAttackDamage = 5.0f;
        m_fAttackSpeed = 8.0f;

        GetOwner().GetAnimator().SetBool("bAttack", true);
    }
    public override void Exit()
    {
        GetOwner().GetAnimator().SetBool("bAttack", false);
    }

    private void charge()
    {
        float fDT = Time.deltaTime * 10.0f;
        m_fAttackDamage += fDT;
        m_fAttackSpeed += fDT;

        if (m_fAttackDamage >= m_fMaxAttackDamage)
            m_fAttackDamage = m_fMaxAttackDamage;

        if (m_fAttackSpeed >= m_fMaxAttackSpeed)
            m_fAttackSpeed = m_fMaxAttackSpeed;   
    }

    public void SetReady(bool _bReady) { m_bReady = _bReady; }

    public float GetArrowSpeed() { return m_fAttackSpeed; }
    public float GetArrowDamage() { return m_fAttackDamage; }

}
