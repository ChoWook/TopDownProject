using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    Enemy enemy;
    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
        LifeTime = 4.0f;
        Speed = 0;
        Invoke("DestorySelf", LifeTime);

        Invoke("ShootEnemy", 0.7f);
    }

    void ShootEnemy()
    {
        Speed = 6.0f;
        ChangeRotationPerUpdate = 0;
    }

    public void SetEnemy(Enemy other)
    {
        enemy = other;
        rotation = Mathf.Atan2(enemy.transform.position.y - transform.position.y, enemy.transform.position.x - transform.position.x);
        rotation = rotation * 180.0f / Mathf.PI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
