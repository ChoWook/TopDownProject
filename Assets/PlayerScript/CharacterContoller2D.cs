﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller2D : MonoBehaviour
{
    const int PLAYER_LAYER = 10;
    const int PLAYER_INVINCIBLE_LAYER = 11;

    // Move player in 2D space
    public float maxSpeed = 6.0f;
    public float jumpHeight = 15f;
    public float gravityScale = 1.5f;
    public float evasionSpeed = 2.5f;
    public Camera mainCamera;
    public Canvas InventoryUI;

    // Attack var
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attack = 5;
    public LayerMask enemyLayer;
    public float invincibleTime = 1.0f;


    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    bool isBothInput = false;
    bool isEvasion = false;
    bool isAttack = false;
    bool isOpenInventory = false;

    Vector3 cameraPos;
    Rigidbody2D r2d;
    BoxCollider2D mainCollider;
    Transform t;
    Animator anim;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InventoryUI.gameObject.SetActive(true);
        InventoryUI.gameObject.SetActive(false);

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }


    // Update is called once per frame
    void Update()
    {
        ToggleInventoryUI();
        BehaviorCheck();
        Attack();
        MoveControl();
        ChangeDirection();
        WalkAnimation();
        Jump();
        JumpAnimation();
        Evasion();
        MoveVelocity();
        CameraFlow();
    }

    private void CameraFlow()
    {
        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y + 1f, cameraPos.z);
        }
    }

    private void MoveVelocity()
    {
        // Apply movement velocity
        if (isEvasion)
        {
            moveDirection = facingRight ? 1 : -1;
            r2d.velocity = new Vector2((moveDirection) * maxSpeed * evasionSpeed, r2d.velocity.y);
        }
        else if(isAttack)
        {
            r2d.velocity = new Vector2(0, r2d.velocity.y);
        }
        else
        {
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        }
    }

    private void Evasion()
    {
        // Evasion
        if (Input.GetButtonDown("Evasion") && isGrounded && !isEvasion  && !isAttack)
        {
            isEvasion = true;
            anim.SetTrigger("evasionTrigger");

            OnInvincible();
        }
    }

    private void JumpAnimation()
    {
        // Jumping animation
        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    private void Jump()
    {
        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !isEvasion && !isAttack)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
    }

    private void WalkAnimation()
    {
        // Walk Animation
        if (moveDirection == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    private void ChangeDirection()
    {
        // Change facing direction
        if (moveDirection != 0 && !isAttack)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_jump_attack"))  // 점프 공격중에는 애니메이션 방향을 바꾸지 않음 
            { 

                if (moveDirection > 0 && !facingRight)
                {
                    facingRight = true;
                    t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
                }
                if (moveDirection < 0 && facingRight)
                {
                    facingRight = false;
                    t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                }
            }
        }
    }

    private void MoveControl()
    {
        // Movement controls
        if (!isEvasion)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                isBothInput = false;
                moveDirection = (Input.GetAxisRaw("Horizontal") > 0) ? 1 : -1;
            }
            else
            {
                if (Input.GetButton("Horizontal"))
                {
                    if (!isBothInput)
                    {
                        moveDirection *= -1;
                        isBothInput = true;
                    }

                }
                else
                {
                    isBothInput = false;
                    moveDirection = 0;
                }

            }
        }
    }

    private void Attack()
    {
        // Attack
        if (Input.GetButtonDown("Fire1") && !isEvasion && !isAttack)
        {
            anim.SetTrigger("attackTrigger");
            isAttack = true;


            // Enemy를 찾아 범위 안에 있는 적들에게 대미지 입히기
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log(enemy.name);
                enemy.GetComponent<Mobs>().takeDamage(attack);
            }
        }
    }

    private void BehaviorCheck()
    {
        // Evasion Check
        if (isEvasion && !anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_dash"))
        {
            isEvasion = false;
            EndInvincible();
        }

        // Attack Check
        if (isAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack"))
        {
            isAttack = false;
        }
    }

    private void ToggleInventoryUI()
    {
        if (Input.GetButtonDown("InventoryToggle")){
            isOpenInventory = !isOpenInventory;
            InventoryUI.gameObject.SetActive(isOpenInventory);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (isOpenInventory)
            {
                InventoryUI.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }


        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            OnDamaged();
        }
        else if (other.gameObject.tag == "Item")
        {
            var groundItem = other.GetComponent<GroundItem>();
            if (groundItem)
            {
                Item _item = new Item(groundItem.item);
                var inventory = GetComponent<Player>().inventory;
                if (inventory.AddItem(_item, 1))
                {
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.gameObject.tag == "end")
        {
            other.enabled = false;
            SceneCnt.EndGame();
        }

    }

    public void OnDamaged()
    {
        OnInvincible();

        // 1초 뒤에 무적 해제
        Invoke("EndInvincible", invincibleTime);
    }

    void EndInvincible()
    {
        gameObject.layer = PLAYER_LAYER;

        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    void OnInvincible() // 무적
    {
        // Change layer
        gameObject.layer = PLAYER_INVINCIBLE_LAYER;

        // 살짝 밝게 변하기
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        
    }

    
}
