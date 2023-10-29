using UnityEngine;

public class Wave3 : IWave
{
    public void Activate()
    {
        Debug.Log("Wave 3 STARTED");
    }

    public void Deactivate()
    {
        Debug.Log("Wave 3 ENDED");
    }
}