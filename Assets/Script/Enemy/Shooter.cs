using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] Projectiles;
    public GameObject ReflectProjectile;
    public int ProjectileType = 0;
    public float Rotation;
    public float Speed = 4.0f;
    public float ShootDelay = 1.0f;
    public float ReflectProbability = 15.0f;
    public float ChangeRotationPerUpdate = 0;
    public float ProjectileLifeTime = 20.0f;
    public float EnableDelay = 0.0f;
    public int LimitShoot = 100;
    public bool isReflectAttack = false;
    public bool isGuided = false;

    bool isDisalbed = false;
    int ShootCnt = 0;
    Rigidbody2D parentR2d;
    // Start is called before the first frame update
    void OnEnable()
    {
        isDisalbed = false;
        parentR2d = GetComponentInParent<Rigidbody2D>();
        Invoke("Shoot", EnableDelay);
    }

    private void OnDisable()
    {
        isDisalbed = true;
        ShootCnt = 0;
        CancelInvoke();
    }

    void Shoot()
    {
        if (isDisalbed)
        {
            return;
        }

        Projectile projectile;

        var random = Random.Range(0, 100);
        if (isReflectAttack && random < ReflectProbability)
        {
            projectile = Instantiate(ReflectProjectile, transform).GetComponent<Projectile>();
        }
        else
        {
            projectile = Instantiate(Projectiles[ProjectileType], transform).GetComponent<Projectile>();
        }
        
        if (projectile == null)
        {
            Invoke("Shoot", ShootDelay);
            return;
        };

        if (isGuided)
        {
            var player = FindObjectOfType<Player>();
            if (player)
            {
                Rotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
                Rotation = Rotation * 180.0f / Mathf.PI;
            }
        }
        
        projectile.rotation = Rotation;
        if (parentR2d)
        {
            projectile.rotation += parentR2d.rotation;
        }

        projectile.Speed = Speed;
        projectile.ChangeRotationPerUpdate = ChangeRotationPerUpdate;
        projectile.LifeTime = ProjectileLifeTime;
        projectile.transform.SetParent(null);
        ShootCnt++;
        if(ShootCnt < LimitShoot)
        {
            Invoke("Shoot", ShootDelay);
        }
        return;
    }
}
