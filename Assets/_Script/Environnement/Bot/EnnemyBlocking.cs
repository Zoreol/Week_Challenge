using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnnemyBlocking : EnnemiSmugManager
{
    public GameObject slimeObjectif;
    public bool dejaCAK;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cak"))
        {
            if (dejaCAK)
            {
                slimeObjectif.transform.position = collision.transform.position;
                Debug.Log("je prend");
                
                slimeObject.transform.parent = collision.GetComponent<Hitting>().player.gameObject.transform.GetChild(2).transform;
                
                //slimeObject.transform.position = new Vector2(0f, 0f);
                //slimeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f,0f));
                transform.position = collision.GetComponent<Hitting>().player.transform.GetChild(2).position;
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                collision.GetComponent<Hitting>().player.GetComponent<PlayerManager>().useObject = true;
                return;
            }

            Debug.Log("collision");
            speed = 0f;
            animator.Play("Slime_Hurt");
            animator.SetBool("hurt", true);
            dejaCAK = true;
        }
        
    }
}
