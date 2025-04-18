using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    tAttack m_tAttackInfo;

    private void Awake()
    {
        m_tAttackInfo.iDamage = 40;
    }

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Player"))
        {
            Player player = pOtherGameObj.GetComponent<Player>();
          
            if (player != null)
            {
                BattleManager.GBattleManager.ActiveDamage(player.SetDamage, transform.position, m_tAttackInfo.iDamage);
            }
        }
    }
}
