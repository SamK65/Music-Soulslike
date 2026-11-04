
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ChallengeManager challengeManager;

    public enum GameState
    {
        StartScreen,
        TutorialMode,
        InfiniteMode
    }

    public GameState CurrentState { get; private set; }

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        // The game always boots to the start screen
        CurrentState = GameState.StartScreen;
        Time.timeScale = 1f;

        Debug.Log("Game booted. Waiting for player selection.");
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        // ESC toggles pause during gameplay.
        if (CurrentState != GameState.StartScreen &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }

        // Don't process mode selection while already in a game
        if (CurrentState != GameState.StartScreen)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame ||
            Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            StartGame(GameState.TutorialMode);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame ||
                 Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            StartGame(GameState.InfiniteMode);
        }
    }

    private void StartGame(GameState selectedMode)
    {
        // Initialize the session to to the selected mode
        GameData.InitializeSession(infiniteMode: selectedMode == GameState.InfiniteMode);

        // Change the current state
        CurrentState = selectedMode;

        // Dispatch to the selected mode
        switch (CurrentState)
        {
            case GameState.TutorialMode:
                Debug.Log("Tutorial Game Mode selected.");
                break;

            case GameState.InfiniteMode:
                Debug.Log("Infinite Game Mode selected.");

                if (challengeManager != null)
                {
                    challengeManager.StartChallenges();
                }
                else
                {
                    Debug.LogError(
                        "GameManager has no ChallengeManager assigned.");
                }

                break;
        }
    }

    private void TogglePause()
    {
        // Flip state ON -> OFF, OFF -> ON
        IsPaused = !IsPaused;

        Time.timeScale = IsPaused ? 0f : 1f;

        Debug.Log(IsPaused ? "Game paused." : "Game resumed.");
    }

    // Temporary GUI
    private void OnGUI()
    {
        // Temporary UI for testing the game flow
        // We can replace this with a proper Canvas later

        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 28,
            alignment = TextAnchor.MiddleCenter
        };

        Rect area = new Rect(
            0, Screen.height / 2 - 50,
            Screen.width, 250);

        if (IsPaused)
        {
            GUI.Label(area, "PAUSED\nPress ESC to resume", style);
            return;
        }


        switch (CurrentState)
        {
            case GameState.StartScreen:
                GUI.Label(
                    area,
                    "GAME START\nPress 1 for Tutorial\nPress 2 for Infinite",
                    style);
                break;

            case GameState.TutorialMode:
                GUI.Label(
                    area,
                    "TUTORIAL MODE\n(Coming Soon)",
                    style);
                break;
            /*
            case GameState.InfiniteMode:
                GUI.Label(
                    area,
                    "INFINITE MODE\n(Coming Soon)",
                    style);
                break;
            */
        }
    }

    // Elegant closing cleanup
    private void OnDestroy()
    {
        // Avoid leaving the editor or another scene paused.
        Time.timeScale = 1f;
    }
}

