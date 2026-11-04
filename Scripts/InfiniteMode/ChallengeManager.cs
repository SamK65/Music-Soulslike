
using System.Collections;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public enum ChallengeState
    {
        Idle,
        CallPhase,
        ResponsePhase,
        WaitingForNextNote,
        ChallengeVictory,
        GameOver,
        BossDefeated
    }

    [Header("References")]
    [SerializeField] private MelodyGenerator melodyGenerator;
    [SerializeField] private VirtualKeyController virtualKeys;
    [SerializeField] private PdMessageRouter pdRouter;
    [SerializeField] private GraphicRouter graphicRouter;

    [Header("Challenge Settings")]
    [SerializeField, Min(1)] private int startingHealth = 10;
    [SerializeField, Min(1)] private int bossHitsRequired = 3;
    [SerializeField, Min(0f)] private float delayBetweenChallenges = 1f;

    public ChallengeState CurrentState { get; private set; }
        = ChallengeState.Idle;

    public int PlayerHealth { get; private set; }
    public int BossHitsRemaining { get; private set; }
    public MelodyData CurrentMelody { get; private set; }
    public int CurrentNoteIndex { get; private set; }

    private float challengeClock;
    private float noteStartTime;
    private float windowStartTime;
    private float allowance;
    private bool noteResolved;
    private bool noteSucceeded;
    private bool challengesStarted;

    private void Awake()
    {
        if (melodyGenerator == null)
            melodyGenerator = GetComponent<MelodyGenerator>();

        if (virtualKeys == null)
            virtualKeys = GetComponent<VirtualKeyController>();

        if (pdRouter == null)
            pdRouter = GetComponent<PdMessageRouter>();

        if (graphicRouter == null)
            graphicRouter = GetComponent<GraphicRouter>();
    }

    private void OnEnable()
    {
        if (virtualKeys != null)
            virtualKeys.OnNotePressed += HandlePlayerInput;
    }

    private void OnDisable()
    {
        if (virtualKeys != null)
            virtualKeys.OnNotePressed -= HandlePlayerInput;
    }

    public void StartChallenges()
    {
        // Wait for Challenge to be started
        if (challengesStarted)
            return;

        if (melodyGenerator == null)
        {
            Debug.LogError(
                "ChallengeManager requires MelodyGenerator");
            enabled = false;
            return;
        } 
        
        if (virtualKeys == null)
        {
            Debug.LogError(
                "ChallengeManager requires VirtualKeyController");
            enabled = false;
            return;
        }

        // Player Health
        PlayerHealth = startingHealth;
        // Boss Health
        BossHitsRemaining = bossHitsRequired;

        BeginChallenge();
    }

    public void BeginChallenge()
    {
        if (PlayerHealth <= 0 || BossHitsRemaining <= 0)
            return;

        StopAllCoroutines();

        // Generate Melody
        CurrentMelody = melodyGenerator.Generate();
        // Initialize:
        // Current Note
        CurrentNoteIndex = 0;
        // Clock
        challengeClock = 0f;
        // Current State of Challenge = Call Phase
        CurrentState = ChallengeState.CallPhase;
        // Remove any highlights
        virtualKeys.ClearHighlights();

        // Start the Challenge
        StartCoroutine(RunChallenge());
    }

    private IEnumerator RunChallenge()
    {
        // Mark starting note with highlight
        virtualKeys.SetHighlight(CurrentMelody.melody[0], true);
        
        // CALL: the boss plays the complete melody.
        for (int i = 0; i < CurrentMelody.melody.Count; i++)
        {
            int degree = CurrentMelody.melody[i];
            int midiNote = CurrentMelody.GetMidiNote(i);

            // Start note, tone = boss
            if (pdRouter != null)
                pdRouter.PlayMidiNote(midiNote);
            if (graphicRouter != null)
                graphicRouter.PlayBossGesticulateStart();

            yield return new WaitForSeconds(
                CurrentMelody.noteLength);

            // End note
            if (pdRouter != null)
                pdRouter.StopMidiNote(midiNote);
            if (graphicRouter != null)
                graphicRouter.PlayBossGesticulateStop();
        }

        // Count in start
        for (int i = 0; i < 3; i++)
        {
            if (pdRouter != null)
                pdRouter.PlayClick();
            if (graphicRouter != null)
                graphicRouter.PlayClick();

            yield return new WaitForSeconds(
                CurrentMelody.noteLength);
        }
        if (pdRouter != null)
            pdRouter.PlayClick();
        if (graphicRouter != null)
            graphicRouter.PlayClick();

        yield return new WaitForSeconds(
            CurrentMelody.noteLength * 0.5f);
        // Count in ends half a beat beat after last click
        // This accounts for the half beat before time X

        // Clear highlight from starting note
        virtualKeys.SetHighlight(CurrentMelody.melody[0], false);

        virtualKeys.ClearHighlights();

        // Start a fresh clock for the response.
        challengeClock = 0f;
        CurrentState = ChallengeState.ResponsePhase;

        // RESPONSE: the player reproduces the melody.
        for (int i = 0; i < CurrentMelody.melody.Count; i++)
        {
            CurrentNoteIndex = i;

            yield return StartCoroutine(
                RunResponseNote(i));

            if (CurrentState == ChallengeState.GameOver)
                yield break;
        }

        // Every note was resolved and the player survived.
        CurrentState = ChallengeState.ChallengeVictory;

        // TODO: Boss hit animation call

        BossHitsRemaining--;

        if (pdRouter != null)
        {
            pdRouter.PlayBossHit();
            pdRouter.PlayChallengeVictory();
        }
        if (graphicRouter != null)
        {
            graphicRouter.PlayBossHit();
            graphicRouter.PlayChallengeVictory();
        }

        Debug.Log(
            $"Challenge cleared! Boss hits remaining: " +
            $"{BossHitsRemaining}");

        if (BossHitsRemaining <= 0)
        {
            CurrentState = ChallengeState.BossDefeated;
            Debug.Log("Boss defeated!");
            yield break;
        }

        // Break time between challenges
        yield return new WaitForSeconds(delayBetweenChallenges);

        BeginChallenge();
    }

    // Coroutine handling clocks and windows in the background
    private IEnumerator RunResponseNote(int index)
    {
        CurrentState = ChallengeState.ResponsePhase;

        float length = CurrentMelody.noteLength;

        // clock reference of 0
        noteStartTime = challengeClock;
        // x (describes real timing, not just offset)
        windowStartTime = noteStartTime + length / 2f;
        // a
        allowance = length / 2f;

        // Marks regardless of failed or succeeded if the note is still answerable 
        noteResolved = false;
        // Marks if the note was answered correctly
        noteSucceeded = false;

        // A note can only be answered within its allotted length.
        while (challengeClock - noteStartTime < length)
        {
            challengeClock += Time.deltaTime;

            // The deadline shrinks with every mistake. (x + a)
            float deadline = windowStartTime + allowance;

            // if note not answered yet and clock > x + a, note failed -> player takes damage
            if (!noteResolved && challengeClock > deadline)
            {
                noteResolved = true;
                noteSucceeded = false;

                PlayerHealth--;

                Debug.Log(
                    $"Missed degree {CurrentMelody.melody[index]}. " +
                    $"Health: {PlayerHealth}");

                if (pdRouter != null)
                    pdRouter.PlayPlayerDamage();
                if (graphicRouter != null)
                    graphicRouter.PlayPlayerDamage();
                

                // if player is dead, game over
                if (PlayerHealth <= 0)
                {
                    PlayerHealth = 0;
                    CurrentState = ChallengeState.GameOver;

                    if (pdRouter != null)
                        pdRouter.PlayPlayerDefeat();
                    if (graphicRouter != null)
                        graphicRouter.PlayPlayerDefeat();

                    Debug.Log("Game over!");
                    yield break;
                }
            }

            yield return null;
        }

        // Both successful and failed notes occupy their full length.
        CurrentState = ChallengeState.WaitingForNextNote;
    }

    private void HandlePlayerInput(int degree)
    {
        if (CurrentState != ChallengeState.ResponsePhase)
            return;

        if (noteResolved)
            return;

        float length = CurrentMelody.noteLength;
        float deadline = windowStartTime + allowance;

        // The note window has expired; the coroutine will
        // register the miss and apply damage.
        if (challengeClock > deadline)
            return;

        bool correctDegree =
            degree == CurrentMelody.melody[CurrentNoteIndex];

        bool insideWindow =
            challengeClock >= windowStartTime;

        if (correctDegree && insideWindow)
        {
            noteResolved = true;
            noteSucceeded = true;

            GameData.Score += 10;

            int midiNote =
                CurrentMelody.GetMidiNote(CurrentNoteIndex);

            if (pdRouter != null)
            {
                pdRouter.PlayMidiNote(midiNote);
                pdRouter.PlayParry();
            }
            if (graphicRouter != null)
                graphicRouter.PlayParry(degree);

            Debug.Log(
                $"Correct! Degree {degree}. " +
                $"Score: {GameData.Score}");

            return;
        }

        // if Early, make early noise
        if (!insideWindow)
        {
            if (pdRouter != null)
                pdRouter.PlayEarly();
            if (graphicRouter != null)
                graphicRouter.PlayEarly();
            
        }

        // Wrong note or input before the valid window.
        allowance -= length / 8f;

        Debug.Log(
            $"Incorrect/early input: {degree}. " +
            $"Remaining allowance: {allowance:F3}s");
    }
}
