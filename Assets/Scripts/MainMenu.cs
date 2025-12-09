using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}