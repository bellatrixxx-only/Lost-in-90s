using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Передаём скорость в Animator
        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Поворот персонажа
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; //  вправо
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;  //  влево
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
