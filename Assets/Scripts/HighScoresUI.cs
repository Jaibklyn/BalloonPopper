using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoresUI : MonoBehaviour
{
    public TextMeshProUGUI[] lines;

    void Start()
    {
        HighScoreManager.Load();

        var scores = HighScoreManager.Scores;
        var names  = HighScoreManager.Names;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == null) continue;

            string name = (i < names.Count)  ? names[i]  : "---";
            int score   = (i < scores.Count) ? scores[i] : 0;

            lines[i].text = $"{i + 1}. {name} - {score}";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}