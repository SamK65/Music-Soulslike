
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MelodyData
{
    public int startNote;
    public List<int> melody = new List<int>();
    public float noteLength;
    public float bpm;

    // Natural minor scale: degrees 1 through 7
    private static readonly int[] NaturalMinorOffsets =
        { 0, 2, 3, 5, 7, 8, 10 };

    // Convert melody intervals to MIDI notes
    public int GetMidiNote(int index)
    {
        if (index < 0 || index >= melody.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        int degree = melody[index];

        if (degree < 1 || degree > 7)
            throw new InvalidOperationException(
                "Melody degrees must be between 1 and 7.");

        return startNote + NaturalMinorOffsets[degree - 1];
    }
}

public class MelodyGenerator : MonoBehaviour
{
    [Header("Melody Settings")]
    [SerializeField, Min(1)]
    private int melodyLength = 8;

    [SerializeField, Min(0.05f)]
    private float noteLength = 0.5f;

    [SerializeField, Range(0, 10)]
    private int lowestMidiOctave = 4;

    [SerializeField, Range(0, 10)]
    private int highestMidiOctave = 5;

    [SerializeField, Min(1f)]
    private float bpm = 120f;

    public MelodyData Generate()
    {
        int low = Mathf.Min(lowestMidiOctave, highestMidiOctave);
        int high = Mathf.Max(lowestMidiOctave, highestMidiOctave);

        // Select a tonic in the configured MIDI octave range
        int minimumMidi = low * 12;
        int maximumMidi = high * 12 + 11;

        MelodyData data = new MelodyData
        {
            startNote = UnityEngine.Random.Range(
                minimumMidi, maximumMidi + 1),
            noteLength = 60 / bpm,
            bpm = bpm,
            melody = new List<int>()
        };

        for (int i = 0; i < melodyLength; i++)
        {
            int degree = UnityEngine.Random.Range(1, 8);
            data.melody.Add(degree);
        }

        Debug.Log(
            $"Generated melody: tonic MIDI {data.startNote}, " +
            $"degrees [{string.Join(", ", data.melody)}]");

        return data;
    }
}
