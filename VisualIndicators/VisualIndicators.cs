using UnityEngine;

public class VisualIndicators : Mod
{
    public void Start()
    {
        Debug.Log("Mod VisualIndicators has been loaded!");
    }

    public void OnModUnload()
    {
        Debug.Log("Mod VisualIndicators has been unloaded!");
    }
}