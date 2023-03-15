using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
