using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    [Header("Settings Player")]
    public int life;
    public int maxLife;
    public float speed;
    public float superSpeed;
    public float jumpHeight = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;
    [SerializeField] float cancelRate = 5;
    [SerializeField]public  Animator animator;
    public Transform targetCamera;
    public GameObject gameOver;

    [Header("Input Interact")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform transformBullet;
    [SerializeField] GameObject hitCorps;

    Rigidbody2D rb;
    bool Jumping = false;
    public bool canJump = true;
    public bool isMove = false;
    public bool canAttackDistance = false;
    public int countJump;
    float buttonPressedTime = 0;
    float buttonPressedWindow = 1;
    bool jumpCanceled = false;
    

    #endregion

    #region BaseFunction
    private void Start()
    {
        life = maxLife;
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (life <= 0)
        {
            gameOver.SetActive(true);
        }
        if (isMove)
        {
            animator.SetBool("Runing", true);
        }
        else
        {
            animator.SetBool("Runing", false);
        }
        InputTrigger();
    }
    
    private void FixedUpdate()
    {
        if (jumpCanceled && Jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }
    #endregion

    #region SpecificFunction 
    private void InputTrigger()
    {
        if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f)
        {
            animator.SetTrigger("isRun");
            isMove = true;
            transform.position = new Vector2(transform.position.x + Input.GetAxis("Horizontal") / speed, transform.position.y);
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                targetCamera.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                targetCamera.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (Input.GetAxis("Fire3") > 0.1f)
            {
                transform.position = new Vector2(transform.position.x + Input.GetAxis("Horizontal") / superSpeed, transform.position.y);
            }

        }
        
        if (Input.GetAxis("Horizontal") == 0f && !Jumping)
        {
            isMove = false;
            animator.SetTrigger("isIdle");
            animator.SetBool("Runing", false);
            
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (rb.velocity.y >= -0.5 &&canJump && countJump < 2)
            {
                JumpAction();
            }
            
        }
        
        if (Jumping)
        {
            buttonPressedTime += Time.deltaTime;
            

            if (buttonPressedTime < buttonPressedWindow && Input.GetButtonUp("Jump"))
            {
                jumpCanceled = true;
            }
            if (rb.velocity.y < 0)
            {
                animator.SetTrigger("isFalling");
                //animator.Play("Animation_Hero_Falling");
                rb.gravityScale = fallGravityScale;
                Jumping = false;
            }
        }

        //fire
        if (Input.GetButtonDown("Fire1") && canAttackDistance)
        {
            Instantiate(bullet, transformBullet);
            animator.Play("Animation_Hero_Attack_02");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            hitCorps.SetActive(true);
            hitCorps.GetComponent<Hitting>().StartCoroutine("AttackCoolDown");
            animator.Play("Animation_Hero_Attack");
        }
    }

    void JumpAction()
    {
        animator.Play("Animation_Hero_Jump");
        rb.gravityScale = gravityScale;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        countJump++;
        Jumping = true;
        buttonPressedTime = 0;
        jumpCanceled = false;
    }

    #endregion

}
