using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;

    // Global score for the current run (persists across levels)
    public static int Score { get; private set; } = 0;

    void Start()
    {
        UpdateScoreText();
    }

    // Adds points based on balloon size.
    // Smaller balloon = more points.
    public void AddScore(float balloonSize)
    {
        // Safety so we don't divide by zero or tiny negatives
        float safeSize = Mathf.Max(Mathf.Abs(balloonSize), 0.1f);

        // Smaller size = bigger reward
        int points = Mathf.RoundToInt(100f / safeSize);

        // If balloonSize was negative (kites/distractors),
        // flip the sign so we subtract points instead.
        if (balloonSize < 0f)
            points = -points;

        Score += points;
        UpdateScoreText();
    }

    // Hard reset
    public static void ResetScore()
    {
        Score = 0;
    }

    public void RestartLevel()
    {
        // If balloon gets too big, level restarts
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Go to the next level, keep the global Score
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            // No more levels -> run finished -> save to high scores
            PlayerData.Load(); // make sure name is loaded
            HighScoreManager.TryAddScore(Score, PlayerData.PlayerName);

            // Go to the HighScores scene
            SceneManager.LoadScene("HighScores");
        }
    }

    //  LEVEL PROGRESSION LOGIC

    // Called every time a balloon is popped
    public void OnBalloonPopped()
    {
        // Tiny delay so the Destroy() on the balloon actually happens
        Invoke(nameof(AdvanceIfNoBalloonsRemain), 0.05f);
    }

    private void AdvanceIfNoBalloonsRemain()
    {
        // Look for any remaining BalloonGrowth objects in the scene
        var balloons = Object.FindObjectsByType<BalloonGrowth>(FindObjectsSortMode.None);

        // If no balloons left, advance to the next level
        if (balloons == null || balloons.Length == 0)
        {
            // Small delay so the final pop sound can finish before changing scenes
            Invoke(nameof(LoadNextLevel), 0.3f);
        }
        // else: still balloons in this scene -> stay on this level
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Score;
    }
}