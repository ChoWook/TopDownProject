using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp_max = 10;
    public float DetectRange = 10.0f;
    public float AttackRange = 2.0f;
    public float Speed = 2.5f;
    CharacterContoller2D player;
    Rigidbody2D r2d;
    Animator anim;
    int Hp;
    // Start is called before the first frame update
    void Awake()
    {
        Hp = Hp_max;
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
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
        var distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= DetectRange && distance >= AttackRange)
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), Speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
            if(distance <= AttackRange)
            {
                anim.SetTrigger("attackTrigger");
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
}
