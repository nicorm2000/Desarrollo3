using System;
using System.Collections;
using UnityEngine;

public class FunctionScheduler : MonoBehaviour
{
    private Coroutine coroutine;

    /// <summary>
    /// Start scheduling the function to be called repeatedly.
    /// </summary>
    /// <param name="function">The function to be called.</param>
    /// <param name="interval">The time interval between function calls.</param>
    public void StartScheduling(Action function, float interval)
    {
        coroutine = StartCoroutine(ScheduleFunction(function, interval));
    }

    /// <summary>
    /// Stop scheduling the function.
    /// </summary>
    public void StopScheduling()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator ScheduleFunction(Action function, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            function?.Invoke();
        }
    }
}