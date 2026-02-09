using UnityEngine;

public class Player : MonoBehaviour
{
    public float Movepeed = 5f;
    public float jumpforce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

  
    void Update()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(MoveInput * Movepeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
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
}
