using UnityEngine;

public class Wave1 : IWave
{
    public void Activate()
    {
        Debug.Log("Wave 1 STARTED");
    }

    public void Deactivate()
    {
        Debug.Log("Wave 1 ENDED");
    }
}