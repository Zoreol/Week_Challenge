using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Retry : MonoBehaviour
{
    [SerializeField] ExportTransitionAnimation transition;
    [SerializeField] Transform transformRetry;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //transition.CanUseAnimation();
            collision.transform.position = transformRetry.transform.position;
            collision.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            collision.GetComponent<PlayerManager>().rb.gravityScale = collision.GetComponent<PlayerManager>().gravityScale;
            collision.GetComponent<PlayerManager>().inGravite = 1;
        }
    }
}
