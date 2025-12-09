using UnityEngine;

public class PinMovement : MonoBehaviour
{
    // How fast the pin moves (units per second)
    public float speed = 8f; 

    // The direction the pin should move in. 
    // By default, it moves straight upward (along the Y axis).
    private Vector3 direction = Vector3.up; 

    // Update is called once per frame.
    void Update()
    {
        // Move the pin in the set direction, scaled by speed and frame time.
        // Time.deltaTime ensures movement is smooth and consistent on all computers.
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the pin has moved too far up or down on the screen.
        // If it goes beyond ±10 on the Y-axis, destroy it to clean up the scene.
        if (Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject); // Remove the pin from the game.
        }
    }

    // This method lets other scripts set the direction of the pin.
    // For example, the player could shoot pins in different angles.
    public void SetDirection(Vector3 dir)
    {
        // Normalize ensures the direction vector always has a length of 1,
        // so speed remains consistent no matter what direction it moves.
        direction = dir.normalized;
    }
}
