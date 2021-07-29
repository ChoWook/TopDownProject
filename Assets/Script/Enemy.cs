using System.Collections;
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

    private bool isAttack = false;
    CharacterContoller2D player;
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
        player = GameObject.FindObjectOfType<CharacterContoller2D>();
        if(player == null)
        {
            return;
        }
        var distanceX = Vector2.Distance(transform.position, new Vector2(player.transform.position.x, transform.position.y));
        var distance = Vector2.Distance(transform.position, player.transform.position);
        var facingDirection = player.transform.position.x - transform.position.x;
        if (!isAttack)
        {
            spriteRenderer.flipX = (facingDirection >= 0.0f) ? false : true;

            if (distance <= DetectRange && distance >= AttackRange)
            {
                anim.SetBool("isWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), Speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking", false);
                if (distance <= AttackRange)
                {
                    Invoke("AttackPlayer", AttackTiming);
                    isAttack = true;
                    anim.SetTrigger("attackTrigger");
                }
            }
        }
    }

    private void WalkAnimation()
    {
        // Walk Animation
        if (r2d.velocity.x == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    private void BehaviorCheck()
    {
        // Attack Check
        if (isAttack && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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
            player.GetComponent<CharacterContoller2D>().OnDamaged(AttackDmg);
        }
    }
}
