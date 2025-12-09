using UnityEngine;

public class PopOnContact : MonoBehaviour
{
    public AudioClip popSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // --- KITE / DISTRACTOR ---
        if (other.CompareTag("Distractor"))
        {
            if (gameManager != null)
            {
                float size = other.transform.localScale.x;

                // Using the same scoring formula as balloons,
                // but pass a negative size so points become negative.
                gameManager.AddScore(-size);
            }

            Destroy(other.gameObject); // destroy kite
            Destroy(gameObject);       // destroy pin
            return;
        }

        // --- BALLOON ---
        if (other.CompareTag("Balloon"))
        {
            // Play pop sound if available
            if (audioSource != null && popSound != null)
            {
                audioSource.PlayOneShot(popSound);
            }

            // Balloon handles its own pop logic
            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
                balloon.Pop();

            // Destroy the pin
            Destroy(gameObject, 0.05f);
        }
    }
}