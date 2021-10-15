using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Transform AttackPoint;
    public Transform[] TurningPoints;
    public GameObject Wave;
    public GameObject GroundWave;
    public GameObject SpreadSpark;
    public float Speed = 3f;

    protected int target = 0;
    protected Enemy enemy;

    Animator anim;
    Rigidbody2D r2d;
    RaycastHit2D Hit;
    bool isCircleRotation = true;
    bool isAttack = false;

    public void OnParentStart()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        r2d = GetComponent<Rigidbody2D>();
        anim.SetBool("Walk", true);

        Invoke("Attack", 7.0f);
    }

    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isAttack = false;
        }
        if (!isAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(TurningPoints[target].transform.position.x, TurningPoints[target].transform.position.y), Speed * Time.deltaTime);
        }
    }

    public void NextTarget()
    {
        if (isCircleRotation)
        {
            target = (target + 1) % 4;
        }
        else
        {
            target = target - 1;
            if (target == -1)
            {
                target = 3;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss2_TurningPoint")
        {
            if (isCircleRotation)
            {
                transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            }
            else
            {
                transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            transform.position = collision.transform.position;
            NextTarget();
        }
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        isAttack = true;

        Invoke("GroundWaveAttack", 1.1f);
    }

    public void WaveAttack()
    {
        Hit = Physics2D.Raycast(AttackPoint.position, transform.up, 30f, LayerMask.GetMask("Platform"));

        var wave = Instantiate(Wave, AttackPoint);
        wave.transform.rotation = new Quaternion(0, 0, r2d.rotation, 0);
        wave.transform.position = new Vector3((AttackPoint.position.x - Hit.point.x) / 2.0f,
                                    ((AttackPoint.position.y - Hit.point.y) / 2.0f), 0);
        wave.transform.localScale = new Vector3(Hit.distance * 1.1f, 1, 1);

        wave.transform.SetParent(null);
        Destroy(wave, 2.0f);
    }

    public void GroundWaveAttack()
    {
        GroundWave.transform.rotation = new Quaternion(0, 0, r2d.rotation, 0);
        GroundWave.SetActive(true);
        Invoke("EndGroundWaveAttack", 0.5f);
    }

    public void SpreadSparkAttack()
    {
        SpreadSpark.transform.rotation = new Quaternion(0, 0, r2d.rotation, 0);
        SpreadSpark.SetActive(true);
        Invoke("EndSpreadSparkAttack", 0.5f);
    }

    public void EndGroundWaveAttack()
    {
        GroundWave.SetActive(false);
    }

    public void EndSpreadSparkAttack()
    {
        SpreadSpark.SetActive(false);
    }

    public void TurnCircleRotation()
    {
        isCircleRotation = !isCircleRotation;
        NextTarget();
    }

    
}
