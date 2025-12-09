using UnityEngine;

public static class PlayerData
{
    private const string PlayerNameKey = "PlayerName";
    private const string VolumeKey     = "Volume";
    private const string DifficultyKey = "Difficulty";

    public static string PlayerName { get; private set; } = "Player";
    public static float Volume      { get; private set; } = 1f;
    public static int   Difficulty  { get; private set; } = 1; // 0=Easy,1=Normal,2=Hard

    private static bool _loaded = false;

    public static void Load()
    {
        if (_loaded) return;

        PlayerName = PlayerPrefs.GetString(PlayerNameKey, "Player");
        Volume     = PlayerPrefs.GetFloat(VolumeKey, 1f);
        Difficulty = PlayerPrefs.GetInt(DifficultyKey, 1);

        // Apply volume + difficulty to systems
        AudioListener.volume = Volume;
        DifficultyManager.SetDifficultyFromIndex(Difficulty);

        _loaded = true;
    }

    public static void SetPlayerName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            name = "Player";

        PlayerName = name;
        PlayerPrefs.SetString(PlayerNameKey, PlayerName);
        PlayerPrefs.Save();
    }

    public static void SetVolume(float v)
    {
        Volume = Mathf.Clamp01(v);
        AudioListener.volume = Volume;

        PlayerPrefs.SetFloat(VolumeKey, Volume);
        PlayerPrefs.Save();
    }

    public static void SetDifficulty(int index)
    {
        Difficulty = Mathf.Clamp(index, 0, 2);
        DifficultyManager.SetDifficultyFromIndex(Difficulty);

        PlayerPrefs.SetInt(DifficultyKey, Difficulty);
        PlayerPrefs.Save();
    }
}