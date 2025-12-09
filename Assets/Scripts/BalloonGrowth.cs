using UnityEngine;

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize  = 3f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Grow the balloon every second
        InvokeRepeating(nameof(Grow), 1f, 1f);
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growRate;

        if (transform.localScale.x >= maxSize)
        {
            CancelInvoke(nameof(Grow));

            // When too big -> no points, restart level
            if (gameManager != null)
                gameManager.RestartLevel();
            else
                Debug.LogWarning("GameManager not found!");

            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        CancelInvoke(nameof(Grow));

        if (gameManager != null)
        {
            // Award points based on size
            gameManager.AddScore(transform.localScale.x);

            // Just notify that one balloon was popped.
            // GameManager will decide whether to advance.
            gameManager.OnBalloonPopped();
        }
        else
        {
            Debug.LogWarning("GameManager not found when popping balloon!");
        }

        Destroy(gameObject);
    }
}