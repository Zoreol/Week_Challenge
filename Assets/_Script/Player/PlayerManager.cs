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
    [SerializeField] public float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;
    [SerializeField] float cancelRate = 5;
    [SerializeField]public  Animator animator;
    public Transform targetCamera;
    public GameObject gameOver;
    public int inGravite = 1;

    [Header("Input Interact")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform transformBullet;
    [SerializeField] GameObject hitCorps;
    [SerializeField] GameObject giveObject;
    [SerializeField] GameObject settings;

    public bool useObject;
    public bool usingObject;
    public Rigidbody2D rb;
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
            speed = 0;
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
            rb.AddForce(Vector2.down * cancelRate * inGravite);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            animator.Play("Animation_Hero_Hurt");
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
                if (inGravite == 1)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                    targetCamera.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                }
                if (inGravite == -1)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, -180f);
                    targetCamera.rotation = Quaternion.Euler(transform.rotation.x, 0f, -180f);
                }
            }
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                if (inGravite == 1)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                    targetCamera.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                }
                if (inGravite == -1)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, -180f);
                    targetCamera.rotation = Quaternion.Euler(transform.rotation.x, -180f, -180f);
                }

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
            if (useObject)
            {
                StartCoroutine(PlacementObject());

            }
        }
    }

    void JumpAction()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        inGravite = 1;
        animator.Play("Animation_Hero_Jump");
        //rb.gravityScale = gravityScale;
        rb.AddForce(Vector2.up * jumpForce * inGravite, ForceMode2D.Impulse) ;
        rb.gravityScale = gravityScale;
        countJump++;
        Jumping = true;
        buttonPressedTime = 0;
        jumpCanceled = false;
    }

    IEnumerator PlacementObject()
    {

        transform.GetChild(2).GetChild(0).transform.parent = giveObject.transform;
        giveObject.transform.GetChild(0).GetChild(0).position = giveObject.transform.position;
        usingObject = true;

            yield return new WaitForSeconds(0.1f);

        usingObject = false;
        giveObject.transform.GetChild(0).GetChild(0).transform.position = transform.GetChild(2).position;
        giveObject.transform.GetChild(0).parent = transform.GetChild(2).transform;
        
        useObject = true;

        
        //giveObject.transform.GetChild(2).GetChild(0).GetChild(0).position = transform.GetChild(2).position;
    }
    
    
    #endregion

}
