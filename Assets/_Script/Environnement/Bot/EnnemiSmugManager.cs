using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnnemiSmugManager : MonoBehaviour
{
    #region Variables
    public float speed;
    public float timeToFlip;
    public float timeStop;
    public Animator animator;
    public Transform[] waypoints;

    public BoxCollider2D colliderEnnemy;
    public SpriteRenderer SpriteRenderer;
    public GameObject slimeObject;
    private int life;
    private int maxLife = 2;
    private Transform target;
    private int destPoint;
    private bool running = true;
    #endregion

    #region Properties

    private void Start()
    {
        life = maxLife;
        target = waypoints[0];
        //StartCoroutine(Voyage());

    }

    private void Update()
    {
        if (running)
        {
            animator.SetBool("Run", true);
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
                SpriteRenderer.flipX = !SpriteRenderer.flipX;
                running = false;
                StartCoroutine(Paused());
            }
        }
    }
    #endregion

    #region Custom Methods
    IEnumerator Voyage()
    {
        animator.SetBool("Run", true);
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }
        yield return new WaitForSeconds(timeToFlip);
        StartCoroutine(Paused());
    }
    IEnumerator Paused()
    {
        animator.SetBool("Run", false);
        yield return new WaitForSeconds(timeStop);
        running = true;
        //StartCoroutine(Voyage());
    }
    IEnumerator dead()
    {
        running = false;
        colliderEnnemy.enabled = false;
        animator.Play("Slime_Death");
        
        yield return new WaitForSeconds(0.8f);

        Destroy(slimeObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") || collision.CompareTag("Cak"))
        {
            life--;
            animator.Play("Slime_Hurt");
            if (life == 0)
            {
                StartCoroutine(dead());
            }
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().life--;
        }
    }
    #endregion
}
