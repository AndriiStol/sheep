using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SheepControl : MonoBehaviour
{
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private bool isGrounded = true; 
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Animator animator; 
    private bool isAlive = true;
    public LayerMask notGroundLayer; 





    public float sheepSpeed = 3.2f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    public void Update()
    {
        if (!isAlive) return; 

        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

       
        rb.velocity = new Vector2(sheepSpeed, rb.velocity.y);

        
        bool isOnNotGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, notGroundLayer);

        if (isOnNotGround)
        {
           
            rb.velocity = new Vector2(sheepSpeed, rb.velocity.y);
        }
        else
        {
           
            if (isGrounded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
               
                if (Input.GetTouch(0).position.y < Screen.height / 2)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    animator.Play("Jump"); 
                }
            }

            
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

           
            if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
            {
                animator.Play("Walk"); 
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("trap"))
        {
            TakeDamage(); 
        }
    }

    public void TakeDamage()
    {
       
        if (isAlive)
        {
            isAlive = false;
            animator.SetTrigger("Die");

            Die();
        }
    }

    public void Die()
    {
        
    }



    public void RestartGame()
    {
        

        UpdateSheepSpeed();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateSheepSpeed()
    {
        sheepSpeed = 3.2f;
    }
}
