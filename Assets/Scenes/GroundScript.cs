using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GroundScript : MonoBehaviour
{
    public bool m_bBaseGround = false;
    private uint m_iPlayerCheck = 1;

    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Player"))
        {
            Player pPlayer = pOtherGameObj.GetComponent<Player>();

            pPlayer.SetGround(true);
            pPlayer.SetLastGround(true);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
       
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Player"))
        {
            Player pPlayer = pOtherGameObj.GetComponent<Player>();

            pPlayer.SetLastGround(false);
        }
    }
    private void excute(Player _pPlayer)
    {
       
        
    }
}
