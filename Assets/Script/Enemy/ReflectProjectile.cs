using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectile : Projectile, TakableDamage
{
    public bool isDamaged = false;

    const int REFLECTED_PROJECTILE_LAYER = 15;
    Boss3 boss3;


    public void Start()
    {
    }

    public int TakeDamage(int dmg)
    {
        // 보스방향으로 각도 수정
        if (!boss3)
        {
            boss3 = FindObjectOfType<Boss3>();
        }
        if (boss3)
        {
            rotation = Mathf.Atan2(boss3.transform.position.y - transform.position.y, boss3.transform.position.x - transform.position.x);
            rotation = rotation * 180.0f / Mathf.PI;
            Speed *= 2;
            gameObject.layer = REFLECTED_PROJECTILE_LAYER;
            isDamaged = true;
        }

        return 1;
    }

}
