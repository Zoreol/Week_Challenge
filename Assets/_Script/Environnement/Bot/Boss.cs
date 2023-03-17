using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Boss : MonoBehaviour
{
    [Header("SettingBoss")]
    public float timeAnim;
    public float timeprepaAnim;
    public int life;
    public int maxLife = 50;
    public GameObject dragon;
    public Animator animator;

    private bool inAction;
    private bool inRandom;

    [Header("Sprite")]
    public SpriteRenderer spriteDragon;
    public Sprite spriteAttack1Dragon;
    public GameObject particul1;
    public ParticleSystem particulAttack;
    public Sprite spriteAttack2Dragon;
    public ParticleSystem particuleDeath;

    public GameObject endGame;

    private void Start()
    {
        StartCoroutine(AttackAnimPrepa());
        inAction = false;
        life = maxLife;
    }
    private void Update()
    {
        if (!inAction && !inRandom)
        {
            StartCoroutine(RandomInt());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inAction)
        {
            if (collision.CompareTag("bullet") || collision.CompareTag("Cak"))
            {
                StartCoroutine(animDegat());
                if (life <= 0)
                {
                    StartCoroutine(DeathAnimaton());
                }
            }
        }
        
    }

    IEnumerator RandomInt()
    {
        inRandom = true;
        int randomize = Random.Range(0, 15);
        if (randomize >= 7 && randomize <= 11)
        {
            StartCoroutine(AttackAnimPrepa2());
        }
        else if (randomize > 11)
        {
            StartCoroutine(AttackAnimPrepa());
        }

        yield return new WaitForSeconds(5f);
        inRandom = false;
    }
    IEnumerator animDegat()
    {
        life--;
        //animator.Play("Dragon_Hurt");
        animator.SetBool("Hurting", true);

        yield return new WaitForSeconds(timeAnim);

        animator.SetBool("Hurting", false);

    }

    IEnumerator DeathAnimaton()
    {
        spriteDragon.enabled = false;
        particuleDeath.Play();
        animator.SetBool("Hurting", true);
        yield return new WaitForSeconds(timeAnim);
        animator.SetBool("Hurting", false);
        endGame.SetActive(true);
        Destroy(dragon);
    }




    IEnumerator AttackAnimPrepa()
    {
        animator.SetBool("attack1", true);
        inAction = true;
        animator.StopPlayback();
        animator.Play("Drragon_Prepa_Attack01");

        spriteDragon.sprite = spriteAttack1Dragon;

        yield return new WaitForSeconds(timeprepaAnim);

        StartCoroutine(AttackAnim());
    }
    IEnumerator AttackAnim()
    {
        animator.Play("Drragon_Attack01");
        particul1.SetActive(true);
        particulAttack.Play();
        animator.SetBool("attack1", false);

        yield return new WaitForSeconds(timeAnim);

        particul1.SetActive(false);
        inAction = false;
        
    }





    IEnumerator AttackAnimPrepa2()
    {
        inAction = true;
        animator.StopPlayback();
        spriteDragon.sprite = spriteAttack2Dragon;

        yield return new WaitForSeconds(timeAnim);

        StartCoroutine(AttackAnim2());
    }
    IEnumerator AttackAnim2()
    {
        animator.Play("Drragon_Attack02");

        yield return new WaitForSeconds(timeAnim);

        particul1.SetActive(false);
        inAction = false;
    }
}
