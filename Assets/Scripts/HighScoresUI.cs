using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoresUI : MonoBehaviour
{
    private const int MaxEntries = 5;
    private const int EmptyScore = -1;

    public TextMeshProUGUI[] lines;

    void Start()
    {
        Debug.Log($"[HighScoresUI] lines.Length = {lines?.Length ?? 0}");

        for (int i = 0; i < MaxEntries; i++)
        {
            if (lines == null || i >= lines.Length || lines[i] == null)
                continue;

            int score = PlayerPrefs.GetInt("HS_Score_" + i, EmptyScore);
            string name = PlayerPrefs.GetString("HS_Name_" + i, "---");

            if (score < 0)
            {
                // empty slot
                lines[i].text = $"{i + 1}. ---";
            }
            else
            {
                lines[i].text = $"{i + 1}. {name} - {score}";
            }

            Debug.Log($"[HighScoresUI] Slot {i}: name={name}, score={score}");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}