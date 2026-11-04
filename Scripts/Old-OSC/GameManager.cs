
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using extOSC;

public class GameManager : MonoBehaviour
{
	[Header("References")]
	public LaneObstacleSpawner spawner;
	public OSCTransmitter transmitter;
	public GameObject playerCharacter;

	[Header("UI")]
	public TMP_Text livesText;
	public TMP_Text scoreText;
	public GameObject startText;
	public GameObject gameOverPanel;

	private int lives = 3;
	private int score = 0;
	private bool gameStarted = false;
	private bool gameOver = false;

	void Awake()
	{
		// Initialize before the game begins.
		lives = 3;
		score = 0;

		if (playerCharacter != null)
			playerCharacter.SetActive(false);

		if (spawner != null)
			spawner.StopSpawning();

		if (startText != null)
			startText.SetActive(true);

		if (gameOverPanel != null)
			gameOverPanel.SetActive(false);

		UpdateUI();
	}

	void Update()
	{
		if (Keyboard.current == null ||
			!Keyboard.current.spaceKey.wasPressedThisFrame)
			return;

		if (!gameStarted && !gameOver)
		{
			StartGame();
		}
		else if (gameOver)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
			);
		}
	}

	void StartGame()
	{
		gameStarted = true;

		if (startText != null)
			startText.SetActive(false);

		if (playerCharacter != null)
			playerCharacter.SetActive(true);

		SendOSC("/gameStart");

		if (spawner != null)
			spawner.StartSpawning();

		Debug.Log("Game started");
	}

	public void RegisterSuccessfulPass()
	{
		if (!gameStarted || gameOver)
			return;

		score++;
		UpdateUI();

		SendOSC("/success");
		Debug.Log("Score: " + score);
	}

	public void PlayerHit()
	{
		if (!gameStarted || gameOver)
			return;

		lives = Mathf.Max(0, lives - 1);
		UpdateUI();

		SendOSC("/hit");
		Debug.Log("Lives: " + lives);

		if (lives <= 0)
			EndGame();
	}

	void EndGame()
	{
		gameOver = true;
		gameStarted = false;

		if (spawner != null)
			spawner.StopSpawning();

		// Hide the player without changing its position.
		if (playerCharacter != null)
			playerCharacter.SetActive(false);

		if (gameOverPanel != null)
			gameOverPanel.SetActive(true);

		SendOSC("/gameOver");
		Debug.Log("Game Over. Final score: " + score);
	}

	void UpdateUI()
	{
		if (livesText != null)
			livesText.text = "Lives = " + lives;

		if (scoreText != null)
			scoreText.text = "Score = " + score;
	}

	void SendOSC(string address)
	{
		if (transmitter == null)
		{
			Debug.LogWarning("OSC transmitter is not assigned.");
			return;
		}

		transmitter.Send(new OSCMessage(address));
		Debug.Log("Sent OSC: " + address);
	}
}