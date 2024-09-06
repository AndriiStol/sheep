using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SheepControl : MonoBehaviour
{
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private bool isGrounded = true; // Начально овца считается на земле.
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Animator animator; // Ссылка на компонент аниматора
    private bool isAlive = true;
    public LayerMask notGroundLayer; // Добавлен новый LayerMask для земли с тегом "notground".





    public float sheepSpeed = 3.2f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    public void Update()
    {
        if (!isAlive) return; // Если персонаж мертв, не обрабатываем ввод

        // Проверяем, находится ли овца на земле.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Движение вправо
        rb.velocity = new Vector2(sheepSpeed, rb.velocity.y);

        // Проверяем, находится ли овца на земле с тегом "notground".
        bool isOnNotGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, notGroundLayer);

        if (isOnNotGround)
        {
            // Движение вправо
            rb.velocity = new Vector2(sheepSpeed, rb.velocity.y);
        }
        else
        {
            // Проверяем наличие касания по экрану
            if (isGrounded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Проверяем, находится ли касание в нижней части экрана (половина ниже середины)
                if (Input.GetTouch(0).position.y < Screen.height / 2)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    animator.Play("Jump"); // Воспроизводим анимацию прыжка
                }
            }

            // Получаем информацию о текущем состоянии анимации
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Проверяем, завершилась ли анимация прыжка
            if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
            {
                animator.Play("Walk"); // Автоматически переключаемся в состояние "Walk"
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("trap"))
        {
            TakeDamage(); // Вызываем функцию смерти при прикосновении к объекту с тегом "trap"
        }
    }

    public void TakeDamage()
    {
        // Обработка урона от ловушки
        if (isAlive)
        {
            isAlive = false;
            animator.SetTrigger("Die");

            Die();
        }
    }

    public void Die()
    {
        // Реализация смерти
    }



    public void RestartGame()
    {
        // Сбрасываем скорость овцы на начальное значение.

        UpdateSheepSpeed();
        // Перезагружаем текущий уровень.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateSheepSpeed()
    {
        sheepSpeed = 3.2f;
    }
}
