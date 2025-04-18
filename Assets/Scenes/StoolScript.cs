using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolScript : MonoBehaviour
{
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Player"))
        {
            Player pPlayer = pOtherGameObj.GetComponent<Player>();
            if (pPlayer.IsGround())
                return;

            Vector2 tPlayerVel = pPlayer.GetComponent<Rigidbody2D>().velocity;

            if (tPlayerVel.y <= 0.0f)
            {
                pPlayer.SetGround(true);
                Rigidbody2D pRigidbody = pPlayer.GetComponent<Rigidbody2D>();
                pRigidbody.gravityScale = 0.0f;
                pRigidbody.velocity = Vector2.zero;

                // ��ġ ����
                Vector3 vPlayerPos = pPlayer.transform.position;

                Collider2D pPlayerColl = pPlayer.GetComponent<Collider2D>();
                float fPlayerYSize = pPlayerColl.bounds.extents.y;

                Collider2D pStoolColl = GetComponent<Collider2D>();
                float fStoolYSize = pStoolColl.bounds.extents.y;

                float fPlayerBottom = vPlayerPos.y - fPlayerYSize;
                float fStoolTop = pStoolColl.bounds.center.y + fStoolYSize;

                if (fPlayerBottom < fStoolTop)
                {
                    // ������ �÷��̾� Y = �� ��� + �÷��̾� ���� ��
                    float fCenter = fStoolTop + fPlayerYSize;
                    pPlayer.transform.position = new Vector3(vPlayerPos.x, fCenter, vPlayerPos.z);
                }

            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject pOtherGameObj = collision.gameObject;

        if (pOtherGameObj.CompareTag("Player"))
        {
            Player pPlayer = pOtherGameObj.GetComponent<Player>();

            pPlayer.SetGround(false);
            pPlayer.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        }
    }
    private void excute(Player _pPlayer)
    {
        

    }
}
