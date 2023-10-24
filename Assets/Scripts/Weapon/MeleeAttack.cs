using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Vector3 attackRange;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float maxTimeToAttack;
    private float timeToAttack;


    private RaycastHit hit;
    private Quaternion rotation;
    private Vector3 size;

    public WeaponData weaponData;

    private void Start()
    {
        timeToAttack = maxTimeToAttack;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Attack();
        }

        timeToAttack -= Time.deltaTime;
    }

    private void Attack() 
    {
        rotation = weapon.transform.rotation;
        size = attackRange;

        if (timeToAttack <= 0) 
        {
            if (Physics.BoxCast(weapon.transform.position, size * 0.5f, weapon.transform.forward, out hit, rotation, attackRange.y, attackLayer))
            {
                hit.collider.GetComponent<HealthSystem>().TakeDamage(weaponData.damage);
                timeToAttack = maxTimeToAttack;
            }

            timeToAttack = maxTimeToAttack;
            Debug.Log("Pego");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(weapon.transform.position, size);
    }
}
