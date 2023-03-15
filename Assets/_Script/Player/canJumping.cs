using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canJumping : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flore"))
        {
            playerManager.animator.SetTrigger("EndFalling");
            //playerManager.animator.Play("Animation_Hero_EndFall");
            playerManager.countJump = 0;
            playerManager.canJump = true;
            playerManager.animator.SetTrigger("isFalling");
            playerManager.animator.SetTrigger("EndFalling");
        }
    }
}
