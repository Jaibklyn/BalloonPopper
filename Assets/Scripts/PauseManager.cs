using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For new Input System

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your PauseMenu panel here
    private bool isPaused = false;

    void Update()
    {
        // Check for Escape key press
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Freeze game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Unfreeze game
        isPaused = false;
    }

    public void GoToTitle()
    {
        Time.timeScale = 1f;  // Reset time before switching scenes
        SceneManager.LoadScene("MainMenu");
    }
}
