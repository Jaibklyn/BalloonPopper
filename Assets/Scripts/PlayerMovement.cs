using UnityEngine;
using UnityEngine.InputSystem;

// Ensure the GameObject has a Rigidbody2D and SpriteRenderer
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    // Speed of the player
    public float speed = 5f;

    // References to components
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Stores the current movement input
    private Vector2 moveInput;

    void Awake()
    {
        // Get references to Rigidbody2D and SpriteRenderer components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Reset movement each frame
        moveInput = Vector2.zero;

        // Get the current keyboard
        var keyboard = Keyboard.current;
        if (keyboard == null) return; // Safety check

        // Check which keys are pressed and adjust movement vector
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            moveInput.y += 1; // Move up
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            moveInput.y -= 1; // Move down
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            moveInput.x -= 1; // Move left
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            moveInput.x += 1; // Move right

        // Normalize to prevent faster diagonal movement
        moveInput = moveInput.normalized;

        // Flip the sprite when moving left/right for a visual effect
        if (moveInput.x > 0.01f) sr.flipX = false;
        else if (moveInput.x < -0.01f) sr.flipX = true;
    }

    void FixedUpdate()
    {
        // Calculate new position based on input and speed
        Vector2 newPos = rb.position + moveInput * speed * Time.fixedDeltaTime;

        // Get camera boundaries to prevent player from leaving the screen
        Camera cam = Camera.main;
        if (cam != null)
        {
            float vertExtent = cam.orthographicSize;           // Half of vertical view size
            float horzExtent = vertExtent * cam.aspect;        // Half of horizontal view size

            // Define screen bounds
            float leftBound = -horzExtent;
            float rightBound = horzExtent;
            float bottomBound = -vertExtent;
            float topBound = vertExtent;

            // Clamp player position inside screen bounds
            newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);
            newPos.y = Mathf.Clamp(newPos.y, bottomBound, topBound);
        }

        // Move the Rigidbody2D to the new position
        rb.MovePosition(newPos);
    }
}
