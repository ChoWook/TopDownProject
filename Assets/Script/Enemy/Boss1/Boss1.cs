using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1: MonoBehaviour
{
    public GameObject Cast;

    Enemy enemy;
    Animator animator;
    CharacterController2D player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponentInChildren<Animator>();

        Invoke("SpellAttack", 3.0f);
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


        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemy.isAttach = false;
        }
    }
    
    public void SpellAttack()
    {
        enemy.isAttach = true;
        animator.SetTrigger("Spell");
        Instantiate(Cast, player.transform).transform.SetParent(null);
    }
    
}
