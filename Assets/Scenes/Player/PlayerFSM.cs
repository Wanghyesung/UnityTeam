using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

//검 들었을 때, 없을 때,
//구르기 X
//점프 (위쪽 속도 검사후 판정)
//방어 (공격오브젝트 방향으로)
//

public class PlayerFSM : MonoBehaviour
{ 
    private SortedDictionary<ePlayerState, PlayerState> m_mapState;
    private PlayerState m_pCurPlayerState = null;
    private Player m_pPlayer;

    private Animator m_pAnimator;
    private Transform m_pTransform;
    private Rigidbody2D m_pRigidbody2D;

    private Collider2D m_pPlayerCollider;

    private Vector3 m_vDir;
    private Vector3 m_vScaleDir; //방향전환용
    private Vector3 m_vInitScale;

    private float m_fHorizon;
    private float m_fVertical;
   
    public void SetPlayerState(ePlayerState _eState)
    {
        //if(m_mapState.ContainsKey(_eState))
        PlayerState pState = FindState(_eState);
        if(pState != null)
            Trace.Assert(1 < 0, "이미 있는 상태");

        m_pCurPlayerState = m_mapState[_eState];
    }

    public void Init()
    {
        m_vDir = new Vector3(1, 0, 0);
        m_mapState = new SortedDictionary<ePlayerState, PlayerState>();

        m_pAnimator = GetPlayer().GetComponent<Animator>();
        m_pTransform = GetPlayer().GetComponent<Transform>();
        m_pRigidbody2D = GetPlayer().GetComponent<Rigidbody2D>();
        m_pPlayerCollider = GetPlayer().GetComponent<Collider2D>();

        m_vInitScale = GetPlayer().GetComponent<Transform>().localScale;
        m_vScaleDir = m_vInitScale;
    }

    public void ChanageState(ePlayerState _eState)
    {
        m_pCurPlayerState.Exit();

        PlayerState pState = FindState(_eState);
        if(pState == null)
        {
            Trace.Assert(1 < 0, "해당 상태 없음");
            return;
        }
           
        m_pCurPlayerState = pState;

        m_pCurPlayerState.Enter();
    }

    public void FSMUpdate()
    {
        if(m_pCurPlayerState.GetState() != ePlayerState.Attack)
        {
            update_rigidbody();
            check_down();
        }

        m_pCurPlayerState.UpdateState();
    }

    public ePlayerState GetCurPlayerState() { return m_pCurPlayerState.GetState(); }

    public void AddState(PlayerState _pPlayerState)
    {
        ePlayerState eState = _pPlayerState.GetState();

        if(FindState(eState) == null)
        {
            _pPlayerState.SetOwner(this);
            _pPlayerState.Init();
            m_mapState.Add(eState, _pPlayerState);
        }
    }
    public PlayerState FindState(ePlayerState _ePlayerState)
    {

        if (m_mapState.ContainsKey(_ePlayerState))
            return m_mapState[_ePlayerState];

        return null;
    }


    private void update_rigidbody()
    {
        m_fHorizon = Input.GetAxisRaw("Horizontal");
        m_fVertical = Input.GetAxisRaw("Vertical"); 

        if (m_fHorizon > 0.0f)
        {
            m_vScaleDir.x = m_vInitScale.x;
            m_vDir = new Vector3(1, 0, 0);

            m_pTransform.localScale = m_vScaleDir;
        }

        else if (m_fHorizon < 0.0f)
        {
            m_vScaleDir.x = m_vInitScale.x * -1;
            m_vDir = new Vector3(-1, 0, 0);

            m_pTransform.localScale = m_vScaleDir;
        }

        else
        {
            m_vDir = new Vector3(0, 0, 0);
        }
    }

    private void check_down()
    {
        if(Input.GetButtonDown("Jump") && m_fVertical < 0.0f &&
            !m_pPlayer.IsLastGround())
        {
            m_pPlayer.SetGround(false);
            m_pRigidbody2D.gravityScale = 2.0f;
        }
    }

    public void SetPlayer(Player _pPlayer) { m_pPlayer = _pPlayer; }
    public Player GetPlayer() { return m_pPlayer; }

    public float GetHorizon() { return m_fHorizon; }
    public float GetVertical() { return m_fVertical; }
    public Vector3 GetDir() { return m_vDir; }
    public Vector3 GetScaleDir() { return m_vScaleDir; }

    public Animator GetAnimator() { return m_pAnimator; }
    public Transform GetTransform() { return m_pTransform; }

    public Rigidbody2D GetRigidbody2D() { return m_pRigidbody2D; }
    public Collider2D GetCollider2D() { return m_pPlayerCollider; }

    
   
}
