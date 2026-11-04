using UnityEngine;

public static class GameData
{
    // Current session variables
    public static int Score { get; set; } = 0;
    public static int Difficulty { get; set; } = 0;

    // Seed used to initialize Unity's random number generator
    public static int RandomSeed { get; private set; }

    // Initialize session variables
    public static bool IsInfiniteMode { get; private set; }
    public static void InitializeSession(bool infiniteMode = false)
    {
        Score = 0;
        Difficulty = 0;

        IsInfiniteMode = infiniteMode;

        // Generate a seed using clock
        RandomSeed = System.DateTime.UtcNow.Ticks.GetHashCode()
                 ^ System.Environment.TickCount;

        // Seed Unity's random number generator
        UnityEngine.Random.InitState(RandomSeed);

        UnityEngine.Debug.Log($"Session initialized. Seed: {RandomSeed}");
    }
}
