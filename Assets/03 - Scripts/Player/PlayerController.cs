using UnityEngine;
using UnityEngine.InputSystem;

// The bare minimum to run and jump: the session is about the Observer,
// not about character control.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 11f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.75f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        float direction = 0f;
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed) direction -= 1f;
        if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed) direction += 1f;

        body.linearVelocity = new Vector2(direction * speed, body.linearVelocity.y);
        if (direction != 0f) spriteRenderer.flipX = direction < 0f;

        bool jumpAsked = keyboard.spaceKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame;
        if (jumpAsked && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }
}
