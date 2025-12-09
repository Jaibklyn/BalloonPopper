using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    // Speed at which the balloon moves
    public float speed = 3f;

    // Current direction of movement (starts moving right)
    private Vector3 direction = Vector3.right;

    // Reference to the SpriteRenderer for flipping the sprite
    private SpriteRenderer sr;

    // Left and right boundaries of the screen
    private float screenLeft, screenRight;

    void Start()
    {
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();

        // Get the main camera
        Camera cam = Camera.main;
        if (cam == null) return; // Safety check

        // Calculate the camera distance from the balloon on the z-axis
        float camDistance = Mathf.Abs(cam.transform.position.z - transform.position.z);

        // Convert viewport coordinates (0 to 1) to world coordinates for screen edges
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, camDistance));
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, camDistance));

        // Store x positions of left and right screen edges
        screenRight = rightEdge.x;
        screenLeft = leftEdge.x;
    }

    void Update()
    {
        // Move the balloon in the current direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the balloon has reached the right edge of the screen
        if (transform.position.x > screenRight)
        {
            direction = Vector3.left;       // Reverse direction to left
            if (sr != null) sr.flipX = true; // Flip the sprite horizontally
        }
        // Check if the balloon has reached the left edge of the screen
        else if (transform.position.x < screenLeft)
        {
            direction = Vector3.right;      // Reverse direction to right
            if (sr != null) sr.flipX = false; // Reset sprite flip
        }
    }
}
