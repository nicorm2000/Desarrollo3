using System;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] private string playerLayer;

    public event EventHandler OnPlayerTriggerEnter;
    public event EventHandler OnPlayerTriggerExit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayer))
        {
            OnPlayerTriggerEnter?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayer))
        {
            OnPlayerTriggerExit?.Invoke(this, EventArgs.Empty);
        }
    }
}