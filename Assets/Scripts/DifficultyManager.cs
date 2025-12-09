using UnityEngine;

public enum DifficultyLevel
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

public static class DifficultyManager
{
    public static DifficultyLevel Current { get; private set; } = DifficultyLevel.Normal;

    public static void SetDifficultyFromIndex(int index)
    {
        if (index < 0 || index > 2)
            index = 1; // default to Normal

        Current = (DifficultyLevel)index;
        Debug.Log($"[DifficultyManager] Difficulty set to: {Current}");
    }

    // How much faster/slower moving targets are
    public static float GetBalloonSpeedMultiplier()
    {
        switch (Current)
        {
            case DifficultyLevel.Easy:   return 0.8f; // slower movement/ easier
            case DifficultyLevel.Normal: return 1.0f; // default
            case DifficultyLevel.Hard:   return 1.3f; // faster movement/ harder
            default:                     return 1.0f;
        }
    }
}