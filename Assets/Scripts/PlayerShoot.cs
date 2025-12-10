using UnityEngine;
using UnityEngine.InputSystem; // For Keyboard.current

public class PlayerShoot : MonoBehaviour
{
    public GameObject pinPrefab; // Prefab of the pin to shoot
    public float pinSpeed = 8f;  // Speed of the pin

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();  // Get the Animator from Player
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Fire when player presses Space or Ctrl
        if (keyboard.spaceKey.wasPressedThisFrame || keyboard.leftCtrlKey.wasPressedThisFrame)
        {
            ShootPin();
        }
    }

    void ShootPin()
    {
        // Trigger animation
        if (animator != null)
        {
            animator.SetTrigger("Shoot"); //Animation parameter "Shoot"
        }

        // Spawn the pin upright
        GameObject pin = Instantiate(pinPrefab, transform.position, Quaternion.identity);

        // Move it straight up
        var move = pin.GetComponent<PinMovement>();
        if (move != null)
        {
            move.SetDirection(Vector3.up);
            move.speed = pinSpeed;
        }
    }
}