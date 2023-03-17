using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cak"))
        {
            StartCoroutine(animHitMan());
        }
    }

    IEnumerator animHitMan()
    {
        animator.Play("mannequinHit");
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("hit", false);
    }
}
