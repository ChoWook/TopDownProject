using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float rotation;
    public float Speed;
    public float LifeTime = 20.0f;
    
    int dmg;
    Enemy enemy;
    Rigidbody2D r2d;
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
    }

    public void SetEnemy(Enemy other)
    {
        enemy = other;
        
    }

    private void Update()
    {
        if (!r2d)
        {
            r2d = GetComponent<Rigidbody2D>();
        }

        if (enemy)
        {
            rotation = Mathf.Atan2(enemy.transform.position.y - transform.position.y, enemy.transform.position.x - transform.position.x);
            rotation = rotation * 180.0f / Mathf.PI;
        }
        
        r2d.rotation = rotation;

        // 날아가는 동작
        var DelX = Mathf.Cos(rotation * Mathf.Deg2Rad) * Time.deltaTime * Speed;
        var DelY = Mathf.Sin(rotation * Mathf.Deg2Rad) * Time.deltaTime * Speed;
        r2d.position = new Vector2(r2d.position.x + DelX, r2d.position.y + DelY);
    }

    public void SetDmg(int dmg)
    {
        this.dmg = (int)(dmg * 0.5f);
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
