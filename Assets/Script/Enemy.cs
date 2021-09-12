﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp_max = 10;
    public float DetectRange = 10.0f;
    public float AttackRange = 2.0f;
    public float Speed = 2.5f;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;
    public int AttackDmg = 1;
    public float AttackTiming = 0.5f;
    public bool isFlip = false;
    public bool isFlying = false;
    public bool isAttach = false;

    private bool isAttack = false;
    CharacterController2D player;
    Rigidbody2D r2d;
    Animator anim;
    SpriteRenderer spriteRenderer;
    int Hp;

    // Start is called before the first frame update
    void Awake()
    {
        Hp = Hp_max;
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        r2d.gravityScale = (isFlying) ? 0 : 1;
    }

    private void Update()
    {
        BehaviorCheck();
        FindPlayer();
    }

    // Update is called once per frame
    public int takeDamage(int dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            Hp = 0;
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
        return Hp;
    }


    private void FindPlayer()
    {
        player = GameObject.FindObjectOfType<CharacterController2D>();
        if(player == null)
        {
            return;
        }
        var distanceX = Vector2.Distance(transform.position, new Vector2(player.transform.position.x, transform.position.y));
        var distance = Vector2.Distance(transform.position, player.transform.position);
        var facingDirection = player.transform.position.x - transform.position.x;
        if (!isAttack)
        {
            spriteRenderer.flipX = (facingDirection >= 0.0f) ? false^isFlip : true^isFlip;
            if (isAttach)   // 고정형 몹은 움직이지 않게 return
            {
                return;
            }
            if (distance <= DetectRange && distance >= AttackRange)
            {
                anim.SetBool("Walk", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), Speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Walk", false);
                if (distance <= AttackRange)
                {
                    Invoke("AttackPlayer", AttackTiming);
                    isAttack = true;
                    anim.SetTrigger("Attack");
                }
            }
        }
    }

    private void WalkAnimation()
    {
        // Walk Animation
        if (r2d.velocity.x == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
        }
    }

    private void BehaviorCheck()
    {
        // Attack Check
        if (isAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttack = false;
        }
    }

    private void AttackPlayer()
    {
        // Enemy를 찾아 범위 안에 있는 적들에게 대미지 입히기
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<CharacterController2D>().OnDamaged(AttackDmg);
        }
    }
}
