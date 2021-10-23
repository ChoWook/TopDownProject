using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1: MonoBehaviour, TakableDamage
{
    public GameObject Spell;
    public GameObject CounterParticle;
    public GameObject SpriteObject;
    public GameObject EndPoint;
    public GameObject Parent;
    public int Hp = 500;
    public float CounterTime = 1.3f;
    public float Speed = 1.5f;
    public LayerMask PlayerLayer;

    bool isAttack = false;
    bool isCountable = false;
    bool isHurt = false;
    float AttackRange = 0;
    float facingDirection = 0;
    float AttackTime = 0;
    Animator animator;
    CharacterController2D player;
    SpriteRenderer spriteRenderer;
    AudioSource hitSound;
    AudioSource DeadSound;

    string[] SpellAttackName = {"SpellOnPlayer", "MultiSpellAttack"};
    string[] AttackName = { "SpellAttack", "MultiSpellAttack", "BaseAttack", "CountableAttack" };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Invoke("Attack", 3.0f);

        hitSound = GameObject.Find("hitSound").GetComponent<AudioSource>();
        DeadSound = GameObject.Find("DeadSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            player = FindObjectOfType<CharacterController2D>();
            if (player)
            {
                player.CameraUpperDistance = 2.0f;
            }
        }

        
    }
    
    public void SpellAttack()
    {
        isAttack = true;
        AttackTime = Time.time;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Spell");

        Invoke(SpellAttackName[Random.Range(0, SpellAttackName.Length)], 1.4f);
    }
    
    void SpellOnPlayer()
    {
        var spell = Instantiate(Spell, player.transform);
        spell.transform.position = new Vector3(player.transform.position.x, -6f, 0);
        spell.transform.SetParent(null);
    }

    public void MultiSpellAttack()
    {
        for(int i = -5; i < 5; i++)
        {
            if(i == 0)
            {
                continue;
            }
            var spell = Instantiate(Spell, player.transform);
            spell.transform.position = new Vector3(transform.position.x + i * 4.5f, -6f, 0);
            spell.transform.SetParent(null);
        }
    }

    public void BaseAttack()
    {
        isAttack = true;
        AttackTime = Time.time;
        AttackRange = 5.0f;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");

        Invoke("DamagePlayer", 0.85f);
    }

    public void CountableAttack()
    {
        AttackRange = 7.0f;
        isAttack = true;
        isCountable = true;
        AttackTime = Time.time;
        spriteRenderer.color = new Color(1.0f, 0.5f, 0.5f);
        animator.SetBool("Walk", false);
        animator.SetTrigger("CountableAttack");
        Destroy(Instantiate(CounterParticle, transform), CounterTime) ;

        Invoke("DamagePlayer", CounterTime);
    }

    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);
        }
        else
        {
            isHurt = false;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && Time.time - AttackTime > 0.2f)
        {
            isAttack = false;
            AttackRange = 2.0f;
            spriteRenderer.color = new Color(1f, 1f, 1f);
        }

        Walk();
    }

    public void DamagePlayer()
    {
        // Enemy를 찾아 범위 안에 있는 적들에게 대미지 입히기
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);

        isCountable = false;
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<CharacterController2D>().OnDamaged(2);
        }
    }

    public int TakeDamage(int dmg)
    {
        Hp -= dmg;
        if (isCountable)
        {
            // 카운터 패턴에 맞았을 때
            isCountable = false;
            isHurt = true;
            CancelInvoke();
            Invoke("Attack", CounterTime + 1.0f);
            animator.SetTrigger("Countered");
        }

        if(Hp < 0)
        {
            // 보스가 죽었을 때 연출
            EndPoint.SetActive(true);
            EndPoint.transform.SetParent(null);
            DeadSound.Play();
            Destroy(Parent);
        }
        else
        {
            hitSound.Play();
        }
        
        return Hp;
    }

    public void Walk()
    {
        if (!isAttack && !isHurt)
        {
            var player = FindObjectOfType<CharacterController2D>();

            facingDirection = player.transform.position.x - transform.position.x;

            var lagacy = spriteRenderer.flipX;

            spriteRenderer.flipX = facingDirection >= 0.0f;

            if (lagacy != spriteRenderer.flipX)
            {
                SpriteObject.transform.localPosition = new Vector3(-SpriteObject.transform.localPosition.x, 0, 0);
            }

            if(Mathf.Abs(facingDirection) > 1.0f)
            {
                animator.SetBool("Walk", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), Speed * Time.deltaTime);
            }
        }
    }

    public void Attack()
    {
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 5)
        {
            Invoke(AttackName[Random.Range(0,AttackName.Length)], 0f);
        }
        else
        {
            SpellAttack();
        }

        Invoke("Attack", 3.5f);
    }
}
