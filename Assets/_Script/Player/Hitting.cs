using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : MonoBehaviour
{
    public GameObject hitObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cube"))
        {
            collision.transform.parent = gameObject.transform;
            hitObject.SetActive(true);
        }
    }
    public IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
