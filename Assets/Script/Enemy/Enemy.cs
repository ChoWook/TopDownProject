using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, TakableDamage
{
    public int Hp_max = 10;
    public float DetectRange = 10.0f;
    public float AttackRange = 2.0f;
    public float Speed = 2.5f;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;
    public int AttackDmg = 1;
    public float AttackTiming = 0.5f;
    public float KnockBackSpeed = 4.0f;
    public float KnockBackTime = 0.3f;
    public bool isFlip = false;         // 스프라이트 X플립
    public bool isFlying = false;       // 날아다니기
    public bool isAttach = false;       // 범위 안에 들어오면 공격
    public bool isWatching = true;      // 캐릭터 위치 바라보기
    public bool isInvincible = false;   // 무적인 몹
    public bool isTouch = true;        // 몸빵딜 유무
    public bool isDamaged = false;

    bool isAttack = false;
    bool isDead = false;
    CharacterController2D player;
    Rigidbody2D r2d;
    Animator anim;
    SpriteRenderer spriteRenderer;
    AudioSource hitSound;
    AudioSource DeadSound;
    int Hp;
    int KnockBackDirection;
    float facingDirection;

    // Start is called before the first frame update
    void Awake()
    {
        Hp = Hp_max;
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        hitSound = GameObject.Find("hitSound").GetComponent<AudioSource>();
        DeadSound = GameObject.Find("DeadSound").GetComponent<AudioSource>();
        
        // audioSource.volume = 0.8f;
    }

    private void Start()
    {
        r2d.gravityScale = (isFlying) ? 0 : 1;
        if (isFlying)
        {
            var cols = GetComponents<BoxCollider2D>();
            foreach(var col in cols)
            {
                if (!col.isTrigger)
                {
                    col.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        BehaviorCheck();
        FindPlayer();
        KnockBack();
    }

    // Update is called once per frame
    public int TakeDamage(int dmg)
    {
        if (!isInvincible)
        {
            Hp -= dmg;
            if (Hp <= 0)
            {
                Hp = 0;
                Debug.Log("Destroyed");
                isDead = true;
                DeadSound.Play();
                Destroy(gameObject);
            }
            else
            {
                // 피격시 뒤로 밀려나기
                isDamaged = true;
                spriteRenderer.color = new Color(1, 1, 1, 0.6f);
               hitSound.Play();
                Invoke("ResetDamaged", KnockBackTime);
            }
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
        facingDirection = player.transform.position.x - transform.position.x;

        if (!isAttack)     
        {
            if (isWatching)
            {
                spriteRenderer.flipX = (facingDirection >= 0.0f) ? false ^ isFlip : true ^ isFlip;
            }

            if (isAttach)
            {
                return;         // 고정형 몹은 움직이지 않게 return
            }
            if (distance <= DetectRange && distance >= AttackRange && !isDamaged)
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

    public bool GetIsDead()
    {
        return isDead;
    }

    private void KnockBack()
    {
        if (isDamaged)
        {
            KnockBackDirection = (facingDirection >= 0.0f) ? -1 : 1;

            r2d.position = new Vector2(transform.position.x + KnockBackDirection * Time.deltaTime * KnockBackSpeed, transform.position.y);
        }
    }

    private void ResetDamaged()
    {
        isDamaged = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}
