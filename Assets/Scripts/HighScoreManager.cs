using System.Collections.Generic;
using UnityEngine;

public static class HighScoreManager
{
    private const int MaxEntries = 5;
    private const string ScoreKeyPrefix = "HS_Score_";
    private const string NameKeyPrefix  = "HS_Name_";

    private static bool _loaded = false;
    private static readonly List<int> _scores  = new();
    private static readonly List<string> _names = new();

    public static IReadOnlyList<int> Scores  => _scores;
    public static IReadOnlyList<string> Names => _names;

    public static void Load()
    {
        if (_loaded) return;

        _scores.Clear();
        _names.Clear();

        for (int i = 0; i < MaxEntries; i++)
        {
            int score = PlayerPrefs.GetInt(ScoreKeyPrefix + i, 0);
            string name = PlayerPrefs.GetString(NameKeyPrefix + i, "---");

            _scores.Add(score);
            _names.Add(name);
        }

        _loaded = true;
    }

    public static void TryAddScore(int newScore, string playerName)
    {
        Load();

        if (string.IsNullOrWhiteSpace(playerName))
            playerName = "Player";

        // Insert new score in sorted order (descending)
        for (int i = 0; i < MaxEntries; i++)
        {
            if (newScore > _scores[i])
            {
                _scores.Insert(i, newScore);
                _names.Insert(i, playerName);

                if (_scores.Count > MaxEntries) _scores.RemoveAt(MaxEntries);
                if (_names.Count > MaxEntries)  _names.RemoveAt(MaxEntries);

                Save();
                return;
            }
        }

        // If not higher than any existing score, do nothing
    }

    private static void Save()
    {
        for (int i = 0; i < MaxEntries; i++)
        {
            PlayerPrefs.SetInt(ScoreKeyPrefix + i, _scores[i]);
            PlayerPrefs.SetString(NameKeyPrefix + i, _names[i]);
        }

        PlayerPrefs.Save();
    }
}