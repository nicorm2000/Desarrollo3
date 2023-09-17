using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(float damage) 
    {
        health -= damage;
    }
}
