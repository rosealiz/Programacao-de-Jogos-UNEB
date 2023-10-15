using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private Weapon weapon;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    public void SetWeaponReference(Weapon weaponReference)
    {
        weapon = weaponReference;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(34);
        }
        Destroy(gameObject);
        if (weapon != null)
        {
            weapon.AddCount();
        }
    }
}
