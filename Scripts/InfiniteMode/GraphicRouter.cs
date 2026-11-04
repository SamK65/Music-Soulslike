
using UnityEngine;

public class GraphicRouter : MonoBehaviour
{
    public static GraphicRouter Instance { get; private set; }
    
    // START OF TEMPORARY GUI VARIABLES
    [SerializeField]
    private bool showPlaceholderGUI = true;

    private bool bossGesticulating;
    private string currentAction = "Waiting";
    private string detailMessage = "";
    private float actionDisplayTime;
    private int lastParryDegree;
    // END OF TEMPORARY GUI VARIABLES

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

        public void PlayBossGesticulateStart()
    {
        bossGesticulating = true;
        ShowAction("BOSS GESTICULATING", "Boss melody playing");
    }

    public void PlayBossGesticulateStop()
    {
        bossGesticulating = false;
        ShowAction("BOSS GESTICULATION STOPPED", "Get ready to respond");
    }

    public void PlayClick()
    {
        ShowAction("COUNT-IN", "Click");
    }

    public void PlayBossHit()
    {
        ShowAction("BOSS HIT!");
    }

    public void PlayChallengeVictory()
    {
        ShowAction("CHALLENGE CLEARED", "You survived the melody!");
    }

    public void PlayPlayerDamage()
    {
        ShowAction("PLAYER DAMAGED", "You missed the timing window");
    }

    public void PlayPlayerDefeat()
    {
        ShowAction("GAME OVER", "Your health reached zero");
    }

    public void PlayParry(int degree)
    {
        lastParryDegree = degree;
        ShowAction("PARRY!", $"Correct scale degree: {degree}");
    }

    public void PlayEarly()
    {
        ShowAction("TOO EARLY!", "Wait for the response window");
    }

    private void ShowAction(string action, string detail)
    {
        currentAction = action;
        detailMessage = detail;
        actionDisplayTime = Time.unscaledTime;
    }

    // TEMPORARY GUI INTERFACE
    private void OnGUI()
    {
        if (!showPlaceholderGUI)
            return;

        // Top-left: current visual state.
        GUIStyle stateStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(12, 12, 8, 8)
        };

        GUI.Box(
            new Rect(20, 20, 300, 75),
            "GRAPHIC ROUTER\nBoss gesticulation: " +
            (bossGesticulating ? "ACTIVE" : "INACTIVE"),
            stateStyle);

        // Center: most recent visual event.
        GUIStyle actionStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 28,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true
        };

        GUIStyle detailStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true
        };

        float centerY = Screen.height * 0.35f;

        GUI.Box(
            new Rect(
                Screen.width * 0.2f,
                centerY,
                Screen.width * 0.6f,
                100),
            currentAction,
            actionStyle);

        GUI.Label(
            new Rect(
                Screen.width * 0.2f,
                centerY + 105,
                Screen.width * 0.6f,
                35),
            detailMessage,
            detailStyle);

        // Bottom-left: latest parry degree.
        GUIStyle smallStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleLeft
        };

        GUI.Box(
            new Rect(20, Screen.height - 60, 260, 35),
            "Last parry degree: " + lastParryDegree,
            smallStyle);
    }
}