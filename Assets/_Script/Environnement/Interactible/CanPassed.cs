using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPassed : MonoBehaviour
{
    public GameObject dialoguePassed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialoguePassed.SetActive(true);
            if (collision.GetComponent<PlayerManager>().canAttackDistance)
            {
                dialoguePassed.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialoguePassed.SetActive(false);
    }
}
