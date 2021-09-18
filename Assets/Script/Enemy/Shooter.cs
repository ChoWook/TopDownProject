using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] Projectiles;
    public int ProjectileType = 0;
    public float Rotation;
    public float Speed = 4.0f;
    public float ShootDelay = 1.0f;

    bool isDisalbed = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        isDisalbed = false;
        Shoot();
    }

    private void OnDisable()
    {
        isDisalbed = true;
    }

    void Shoot()
    {
        if (isDisalbed)
        {
            return;
        }

        var projectile = Instantiate(Projectiles[ProjectileType], transform).GetComponent<Projectile>();
        if (projectile == null)
        {
            Invoke("Shoot", ShootDelay);
            return;
        };

        projectile.rotation = Rotation;
        projectile.Speed = Speed;

        Invoke("Shoot", ShootDelay);
        return;
    }
}
