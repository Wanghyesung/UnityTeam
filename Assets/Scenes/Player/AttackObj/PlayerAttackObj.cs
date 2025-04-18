using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObj : MonoBehaviour
{
    private bool m_bDeleteObject = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Monster"))
        {
            MonsterScript pMonster = pOtherGameObj.GetComponent<MonsterScript>();
            if (pMonster != null)
            {
                pMonster.SetDamage();

                //Pool.GPool.ReleaseObject()
                if (m_bDeleteObject)
                    gameObject.SetActive(false);
            }
        }
    }

    public void SetDeleteObject(bool _bDelete) { m_bDeleteObject = _bDelete; }
    
}
