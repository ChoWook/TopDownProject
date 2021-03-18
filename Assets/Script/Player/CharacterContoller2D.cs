using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 6.0f;
    public float jumpHeight = 15f;
    public float gravityScale = 1.5f;
    public float evasionSpeed = 2.5f;
    public Camera mainCamera;

    // Attack var
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attack = 5;
    public LayerMask enemyLayer;


    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    bool isBothInput = false;
    bool isEvasion = false;
    bool isAttack = false;

    Vector3 cameraPos;
    Rigidbody2D r2d;
    BoxCollider2D mainCollider;
    Transform t;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

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
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
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
        if (Input.GetButtonDown("Fire1") && !isEvasion)
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
        }

        // Attack Check
        if (isAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack"))
        {
            isAttack = false;
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
}
