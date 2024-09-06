using UnityEngine;
using System.Collections;


public class AnimationPlayer : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Воспроизводим анимацию с именем "tap" при запуске сцены.
        if (animator != null)
        {
            animator.Play("tap");
        }

        // Запускаем корутину для скрытия объекта после завершения анимации.
        StartCoroutine(HideAfterAnimation());
    }

    IEnumerator HideAfterAnimation()
    {
        // Ждем завершения анимации.
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Скрываем объект.
        gameObject.SetActive(false);
    }
}
