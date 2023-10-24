using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private GameObject weapon;


    RaycastHit hit;
    Quaternion rotation;
    Vector3 size;

    public WeaponData weaponData;

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Attack();
        }
    }

    private void Attack() 
    {
        rotation = weapon.transform.rotation;
        size = new Vector3(attackRange, attackRange, 0.5f);

        if (Physics.BoxCast(weapon.transform.position, size * 0.5f, weapon.transform.forward, out hit, rotation, attackRange, attackLayer))
        {
            hit.collider.GetComponent<HealthSystem>().TakeDamage(weaponData.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(weapon.transform.position, size);
    }
}
