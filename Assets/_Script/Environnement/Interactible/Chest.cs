using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject dialogueTrouver;

    private void Start()
    {
          animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("OpenChest", true);
            collision.GetComponent<PlayerManager>().canAttackDistance = true;
            dialogueTrouver.SetActive(true);
        }
    }
}
