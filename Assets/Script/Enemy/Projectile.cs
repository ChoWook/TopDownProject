using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float rotation;
    public float Speed;
    public float AnimationSpeed = 5;
    public float LifeTime = 20.0f;
    Rigidbody2D r2d;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Invoke("DestroySelf", LifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        // 실시간으로 방향과 속도를 바꾸고 싶으면 여기에 작성

        // 스프라이트 방향 바꾸기
        r2d.rotation = rotation;

        // 날아가는 동작
        var DelX = Mathf.Cos(rotation * Mathf.Deg2Rad) * Time.deltaTime * Speed;
        var DelY = Mathf.Sin(rotation * Mathf.Deg2Rad) * Time.deltaTime * Speed;
        r2d.position = new Vector2(r2d.position.x + DelX, r2d.position.y + DelY);

        // 속도에 맞춰 스프라이트 속도 조절
        animator.speed = Speed / AnimationSpeed;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
