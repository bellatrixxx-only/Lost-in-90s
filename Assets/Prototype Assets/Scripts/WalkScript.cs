using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool movementBlocked;  
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

 
    public void SetBlocked(bool blocked)
    {
        movementBlocked = blocked;

        if (blocked)
        {
            rb.linearVelocity = Vector2.zero;
            if (animator != null)
                animator.SetFloat("Speed", 0f);
        }
    }

    private void Update()
    {
        if (movementBlocked) return;   

        moveInput = Input.GetAxisRaw("Horizontal");

        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;
    }

    private void FixedUpdate()
    {
        if (movementBlocked) return;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
