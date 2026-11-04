
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class HighScoreManager
{
    private const string SaveKey = "Game_HighScores";
    private const int MaxHighScores = 10;

    [Serializable]

    public class HighScoreEntry
    {
        public string initials;
        public int score;

        public HighScoreEntry(string initials, int score)
        {
            this.initials = initials;
            this.score = score;
        }
    }

    [Serializable]
    private class HighScoreList
    {
        public List<HighScoreEntry> scores = new List<HighScoreEntry>();
    }

    private static List<HighScoreEntry> highScores;

    // Load scores when first requested
    private static void EnsureLoaded()
    {
        // Check if scores are already loaded
        if (highScores != null)
            return;

        highScores = new List<HighScoreEntry>();

        string json = PlayerPrefs.GetString(SaveKey, "");

        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                HighScoreList saved = JsonUtility.FromJson<HighScoreList>(json);

                if (saved != null && saved.scores != null)
                    highScores = saved.scores;
            }
            // If loading fails throw error
            catch (Exception e)
            {
                Debug.LogWarning(
                    $"Could not load high scores: {e.Message}");
            }
        }
        // Remove invalis entries and normalize initals.
        highScores.RemoveAll(entry => entry == null || entry.score < 0);

        foreach (HighScoreEntry entry in highScores)
        {
            entry.initials = NormalizeInitials(entry.initials);
        }

        SortAndTrim();

    }

    private static string NormalizeInitials(string initials)
    {
        // If no initials are provided, make it "AAA"
        if (string.IsNullOrWhiteSpace(initials))
            return"AAA";
        
        initials = initials.Trim().ToUpperInvariant();

        if (initials.Length > 3)
            initials = initials.Substring(0, 3);

        // Pad short initials to exactly three characters.
        return initials.PadRight(3, 'A');
    }

    private static void SortAndTrim()
    {
        highScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (highScores.Count > MaxHighScores)
        {
            highScores.RemoveRange(MaxHighScores, highScores.Count - MaxHighScores);
        }
    }

    public static IReadOnlyList<HighScoreEntry> GetHighScores()
        {
            EnsureLoaded();
            return highScores.AsReadOnly();
        }

        // Submit a score together with the player's initials.
        // Returns true if the score makes the top 10.
        public static bool SubmitScore(int score, string initials)
        {
            EnsureLoaded();

            if (score < 0)
                return false;

            if (highScores.Count >= MaxHighScores && score <= highScores[MaxHighScores - 1].score)
            {
                return false;
            }

            initials = NormalizeInitials(initials);

            highScores.Add(new HighScoreEntry(initials, score));

            SortAndTrim();
            SaveHighScores();

            return true;
        }

        private static void SaveHighScores()
        {
            HighScoreList data = new HighScoreList
            {
                scores = highScores
            };

            string json = JsonUtility.ToJson(data);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }