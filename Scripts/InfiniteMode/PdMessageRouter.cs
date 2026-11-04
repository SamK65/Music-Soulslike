
using UnityEngine;

public class PdMessageRouter : MonoBehaviour
{
    public static PdMessageRouter Instance { get; private set; }

    [SerializeField]
    private LibPdInstance pdInstance;

    private void Awake()
    {
        if (pdInstance == null)
            pdInstance = GetComponent<LibPdInstance>();

        if (pdInstance == null)
        {
            Debug.LogError("PdMessageRouter requires a LibPdInstance.");
        }
    }

    // player: tone = 0, boss: tone = 1
    public void PlayMidiNote(int midiNote,int tone = 0 ,int velocity = 100)
    {
        Debug.Log($"[PD placeholder] Play note {midiNote}, tone {tone}, velocity {velocity}");

        if (pdInstance == null)
            return;

        pdInstance.SendFloat("midiNote", midiNote);

        Debug.Log($"Sent MIDI note number {midiNote} to Pd.");
    }

    public void StopMidiNote(int midiNote)
    {
        Debug.Log($"[PD placeholder] Stop note {midiNote}");
    }

    public void PlayBossHit()
    {
        Debug.Log("[PD placeholder] Boss hit event");
    }

    public void PlayPlayerDamage()
    {
        Debug.Log("[PD placeholder] Player damage event");
    }

    public void PlayParry()
    {
        Debug.Log("[PD placeholder] Successful parry event");
    }

    public void PlayChallengeVictory()
    {
        Debug.Log("[PD placeholder] Challenge victory event");
    }

    public void PlayPlayerDefeat()
    {
        Debug.Log("[PD placeholder] Player defeat event");
    }

    public void PlayClick()
    {
        Debug.Log("[PD placeholder] Click event");
    }

    public void PlayEarly()
    {
        Debug.Log("[PD placeholder] Early event");
    }
}
