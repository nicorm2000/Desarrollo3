using UnityEngine;

public class Wave2 : IWave
{
    public void Activate()
    {
        Debug.Log("Wave 2 STARTED");
    }

    public void Deactivate()
    {
        Debug.Log("Wave 2 ENDED");
    }
}