using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBlocking : EnnemiSmugManager
{
    float speedbase;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Cak"))
        {
            speedbase = speed;
            animator.Play("Slime_Hurt");
            animator.SetBool("hurt", true);
            speed = 0f;

            StartCoroutine(InStone());
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().life--;
        }
    }

    IEnumerator InStone()
    {
        
        yield return new WaitForSeconds(5f);

        gameObject.transform.position = waypoints[0].position;
        animator.SetBool("hurt", false);
        speed = speedbase;
    }
}
