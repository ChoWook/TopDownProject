using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectile : Projectile, TakableDamage
{
    public bool isDamaged = false;

    const int REFLECTED_PROJECTILE_LAYER = 15;
    Enemy enemy;

    public void Start()
    {
        Invoke("DestroySelf", LifeTime);
    }

    public int TakeDamage(int dmg)
    {
        // 보스방향으로 각도 수정
        if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>();
        }
        if (enemy)
        {
            rotation = Mathf.Atan2(enemy.transform.position.y - transform.position.y, enemy.transform.position.x - transform.position.x);
            rotation = rotation * 180.0f / Mathf.PI;
            Speed *= 2;
            gameObject.layer = REFLECTED_PROJECTILE_LAYER;
            LifeTime = 20.0f;
            ChangeRotationPerUpdate = 0;
            CancelInvoke();
            Invoke("DestorySelf", LifeTime);
            isDamaged = true;
        }

        return 1;
    }

}
