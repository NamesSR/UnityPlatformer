using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public float Movepeed = 5f;
    public float jumpforce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;
    private SpriteRenderer spriterenderer;
    public int extraJumpsValue = 1;
    private int extraJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();    
        extraJumps = extraJumpsValue;
    }

  
    void Update()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(MoveInput * Movepeed, rb.linearVelocity.y);
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            }
            else if(extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
                extraJumps--;
            }
            
                
            
        }

        SetAnimation(MoveInput);
    }
    private void FixedUpdate()
    {
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void SetAnimation(float MoveInput)
    {
        if (isGrounded)
        {
            if (MoveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_run");
            }
        }
        else
        {
            if(rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_fall");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "damage")
        {
            Health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            StartCoroutine(blinkRed());

            if(Health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator blinkRed()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.white;
    }
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
