using UnityEngine;
using System.Collections;   // <-- needed for IEnumerator / Coroutine

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize  = 3f;

    private GameManager gameManager;
    private Animator animator;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        animator    = GetComponent<Animator>();

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
        // Stop growth ticks
        CancelInvoke(nameof(Grow));

        // Trigger pop animation if we have one
        if (animator != null)
        {
            animator.SetTrigger("Pop");
        }

        // Award score immediately (before destroy)
        if (gameManager != null)
        {
            gameManager.AddScore(transform.localScale.x);
        }
        else
        {
            Debug.LogWarning("GameManager not found when popping balloon!");
        }

        StartCoroutine(PopSequence());
    }

    private IEnumerator PopSequence()
    {
        // Wait long enough for the pop animation to play
        yield return new WaitForSeconds(0.25f);   // match your clip length

        if (gameManager != null)
        {
            // Any balloons left check
            gameManager.OnBalloonPopped();
        }

        // Balloon is removed
        Destroy(gameObject);
    }
}