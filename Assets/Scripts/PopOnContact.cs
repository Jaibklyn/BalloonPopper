using UnityEngine;

public class PopOnContact : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip popSound;
    [Range(0f, 1f)] public float volume = 1f;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[PopOnContact] Hit: {other.name}, tag={other.tag}");

        // KITE / DISTRACTOR
        if (other.CompareTag("Distractor"))
        {
            if (gameManager != null)
            {
                float size = other.transform.localScale.x;
                // Negative score for kite hit (same formula, opposite sign)
                gameManager.AddScore(-size);
                Debug.Log($"[PopOnContact] Kite hit. Size={size}, negative score applied.");
            }
            else
            {
                Debug.LogWarning("[PopOnContact] GameManager not found for Distractor hit.");
            }

            Destroy(other.gameObject); // destroy kite
            Destroy(gameObject);       // destroy pin
            return;
        }

        // BALLOON
        if (other.CompareTag("Balloon"))
        {
            if (popSound != null)
            {
                Debug.Log("[PopOnContact] Balloon hit. Attempting to play pop sound.");

                AudioSource.PlayClipAtPoint(popSound, Vector3.zero, volume);
            }
            else
            {
                Debug.LogWarning("[PopOnContact] popSound is NOT assigned in the Inspector.");
            }

            // Let the balloon handle score + destroy
            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
            {
                balloon.Pop();
            }
            else
            {
                Debug.LogWarning("[PopOnContact] BalloonGrowth missing on Balloon. Destroying manually.");
                Destroy(other.gameObject);
            }

            // Destroy the pin
            Destroy(gameObject, 0.05f);
        }
    }
}