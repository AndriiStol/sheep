using UnityEngine;
using System.Collections;


public class AnimationPlayer : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        
        if (animator != null)
        {
            animator.Play("tap");
        }

        
        StartCoroutine(HideAfterAnimation());
    }

    IEnumerator HideAfterAnimation()
    {
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        
        gameObject.SetActive(false);
    }
}
