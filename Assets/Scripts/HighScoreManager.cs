using System.Collections.Generic;
using UnityEngine;

public static class HighScoreManager
{
    private const int MaxEntries      = 5;
    private const string ScoreKeyPrefix = "HS_Score_";
    private const string NameKeyPrefix  = "HS_Name_";

    private static bool _loaded = false;
    private static readonly List<int>    _scores = new();
    private static readonly List<string> _names  = new();

    public static IReadOnlyList<int> Scores  => _scores;
    public static IReadOnlyList<string> Names => _names;

    public static void Load()
    {
        if (_loaded) return;

        _scores.Clear();
        _names.Clear();

        for (int i = 0; i < MaxEntries; i++)
        {
            // -1 means "empty slot"
            int score = PlayerPrefs.GetInt(ScoreKeyPrefix + i, -1);
            string name = PlayerPrefs.GetString(NameKeyPrefix + i, "---");

            _scores.Add(score);
            _names.Add(name);
        }

        _loaded = true;
        Debug.Log("[HighScoreManager] Loaded entries: " + string.Join(", ", _scores));
    }

    public static void TryAddScore(int newScore, string playerName)
    {
        Load();

        if (string.IsNullOrWhiteSpace(playerName))
            playerName = "Player";

        Debug.Log($"[HighScoreManager] Trying to add score {newScore} for {playerName}");

        int insertIndex = -1;

        // First, check if it's bigger than something already there
        for (int i = 0; i < MaxEntries; i++)
        {
            if (newScore > _scores[i])
            {
                insertIndex = i;
                break;
            }
        }

        // If not bigger than any existing score,
        // see if there's an EMPTY slot (score < 0)
        if (insertIndex == -1)
        {
            for (int i = 0; i < MaxEntries; i++)
            {
                if (_scores[i] < 0)
                {
                    insertIndex = i;
                    break;
                }
            }
        }

        // Still -1 → table is full and new score is not high enough
        if (insertIndex == -1)
        {
            Debug.Log("[HighScoreManager] New score not high enough for table.");
            return;
        }

        _scores.Insert(insertIndex, newScore);
        _names.Insert(insertIndex, playerName);

        if (_scores.Count > MaxEntries) _scores.RemoveAt(MaxEntries);
        if (_names.Count > MaxEntries)  _names.RemoveAt(MaxEntries);

        Save();
        Debug.Log($"[HighScoreManager] Inserted at index {insertIndex}. Now: {string.Join(", ", _scores)}");
    }

    private static void Save()
    {
        for (int i = 0; i < MaxEntries; i++)
        {
            PlayerPrefs.SetInt(ScoreKeyPrefix + i, _scores[i]);
            PlayerPrefs.SetString(NameKeyPrefix + i, _names[i]);
        }

        PlayerPrefs.Save();
        Debug.Log("[HighScoreManager] Saved.");
    }
}