using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
