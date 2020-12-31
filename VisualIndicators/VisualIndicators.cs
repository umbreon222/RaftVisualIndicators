using System.Diagnostics.CodeAnalysis;
using System.Timers;
using UnityEngine;

public class VisualIndicators : Mod
{
    const double CHECK_INTERVAL = 1000;
    public static VisualIndicators instance;
    readonly Timer timer = new Timer(CHECK_INTERVAL);
    bool modActive = true;
    AI_NetworkBehavior_Shark shark;
    Seagull seagull;

    public void Start()
    {
        Debug.Log("Mod VisualIndicators has been loaded!");
        instance = this;
        timer.Elapsed += Timer_Elapsed;
    }

    public void OnModUnload()
    {
        Debug.Log("Mod VisualIndicators has been unloaded!");
        timer.Stop();
    }

    public override void WorldEvent_WorldLoaded()
    {
        Debug.Log("The world is loaded!");
        // Find that pesky seagull
        seagull = FindObjectOfType<Seagull>();
        // Find that annoying shark
        shark = FindObjectOfType<AI_NetworkBehavior_Shark>();
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (!modActive || seagull == null || shark == null)
        {
            return;
        }
        if (seagull.currentState == SeagullState.Peck)
        {
            RAPI.BroadcastChatMessage("Seagull is eating plants!");
        }
        if (shark.stateMachineShark.biteRaftState.isBitingRaft)
        {
            RAPI.BroadcastChatMessage("Shark is attacking raft!");
        }
    }

    [ConsoleCommand(name: "visual_indicators", docs: "Turns visual indicators on or off.")]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required by API")]
    public static void ActivateVisualIndicators(string[] args)
    {
        if (instance != null)
        {
            // Flip the switch
            instance.modActive = !instance.modActive;
            Debug.Log(string.Format("Mod VisualIndicators is {0}!", instance.modActive ? "on" : "off"));
        }
    }

    public void Update() {; }
}