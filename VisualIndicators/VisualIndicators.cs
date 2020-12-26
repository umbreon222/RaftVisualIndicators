using UnityEngine;
using System;

public class VisualIndicators : Mod
{
    AI_NetworkBehavior_Shark shark;
    Seagull seagull;
    DateTime? nextCheckTime;
    readonly double timeout = 1000;

    public void Start()
    {
        Debug.Log("Mod VisualIndicators has been loaded!");
    }

    public void OnModUnload()
    {
        Debug.Log("Mod VisualIndicators has been unloaded!");
    }

    public void Update()
    {
        // Don't try to do anything until we're in the game
        if (Semih_Network.InMenuScene)
        {
            return;
        }

        if (nextCheckTime.HasValue && DateTime.Now < nextCheckTime)
        {
            return;
        }

        if (seagull == null)
        {
            seagull = FindObjectOfType<Seagull>();
        }

        // FindObjectOfType returns null during the loading screen so check for null
        if (seagull != null && !seagull.IsKilled && seagull.currentState == SeagullState.Peck)
        {
            RAPI.BroadcastChatMessage("Seagull is eating plants!");
        }

        if (shark == null)
        {
            shark = FindObjectOfType<AI_NetworkBehavior_Shark>();
        }

        // FindObjectOfType returns null during the loading screen so check for null
        if (shark != null && !shark.IsKilled && shark.stateMachineShark.biteRaftState.isBitingRaft == true)
        {
            RAPI.BroadcastChatMessage("Shark is attacking raft!");
        }

        nextCheckTime = DateTime.Now.AddMilliseconds(timeout);
    }
}