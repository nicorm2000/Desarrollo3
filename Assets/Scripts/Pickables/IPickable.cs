using UnityEngine;

public interface IPickable
{
    float CooldownTime { get; }
    void ApplyEffect();
    void StartCooldown();
    void ModifyVisuals(Material material, bool icon);
    bool IsReadyForPickUp();
}