using UnityEngine;

public class ExportTransitionAnimation : MonoBehaviour
{
    public Animator animator;
    public bool canAnim;

    public void CanUseAnimation()
    {
        animator.SetTrigger("changeScene");
    }
}
