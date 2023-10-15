using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bulletPrefab;
    private int bulletCount = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        BulletMove bulletMoveScript = bullet.GetComponent<BulletMove>();
        if (bulletMoveScript != null)
        {
            bulletMoveScript.SetWeaponReference(this);
        }
    }

    public void AddCount()
    {
        bulletCount++;
        Debug.Log("Bullet Count: " + bulletCount);
        if (bulletCount == 30)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
