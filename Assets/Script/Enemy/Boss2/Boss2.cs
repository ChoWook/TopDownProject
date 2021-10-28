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
    public GameObject TargetDown;
    public BossDestroyCheck bossDestroyCheck;
    public float Speed = 3f;

    protected int target = 0;
    protected Enemy enemy;

    Animator anim;
    Rigidbody2D r2d;
    RaycastHit2D Hit;
    bool isCircleRotation = true;
    bool isAttack = false;
    int AttackWeight = 3;
    int TargetDownCnt = 0;
    CharacterController2D player;

    string[] AttackName = { "WaveAttack", "SpreadSparkAttack", "TargetDownAttack", "GroundWaveAttack", "GroundWaveAttack", "GroundWaveAttack" };

    public void OnParentStart()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        r2d = GetComponent<Rigidbody2D>();
        anim.SetBool("Walk", true);

        Invoke("Attack", 1.0f);
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

        if (Hit)
        {
            Debug.DrawLine(AttackPoint.position, Hit.point);
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

        Invoke(AttackName[Random.Range(0, AttackWeight)], 1.1f);
        Invoke("NextPattern", 4.0f);
    }

    public void WaveAttack()
    {
        Hit = Physics2D.Raycast(AttackPoint.position, transform.up, 30f, LayerMask.GetMask("Platform"));

        var wave = Instantiate(Wave, AttackPoint);
        var waveR2d = wave.GetComponent<Rigidbody2D>();
        wave.transform.position = new Vector2((Hit.point.x + AttackPoint.position.x) / 2.0f,
                                    ((Hit.point.y + AttackPoint.position.y) / 2.0f));
        waveR2d.rotation = r2d.rotation;
        wave.transform.localScale = new Vector3(Hit.distance * 1.25f, 3, 1);

        wave.transform.SetParent(null);
        Destroy(wave, 5.0f);
    }

    public void GroundWaveAttack()
    {
        GroundWave.GetComponent<Rigidbody2D>().rotation = r2d.rotation;
        GroundWave.SetActive(true);
        Invoke("EndGroundWaveAttack", 0.5f);
    }

    public void SpreadSparkAttack()
    {
        SpreadSpark.GetComponent<Rigidbody2D>().rotation = r2d.rotation;
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

    public void TargetDownAttack()
    {
        if (!player)
        {
            player = FindObjectOfType<CharacterController2D>();
        }

        Instantiate(TargetDown, player.GetComponent<Collider2D>().transform).transform.SetParent(null);

        if(TargetDownCnt < 3)
        {
            TargetDownCnt++;
            Invoke("TargetDownAttack", 0.3f);
        }
        else
        {
            TargetDownCnt = 0;
        }
    }

    public void TurnCircleRotation()
    {
        isCircleRotation = !isCircleRotation;
        NextTarget();

        Invoke("NextPattern", 2.5f);
    }

    public void NextPattern()
    {
        if (!player)
        {
            player = FindObjectOfType<CharacterController2D>();
        }

        player.isBossStage = true;
        AttackWeight = 4;                                           // 1:1:1:1 비율
        if(player.transform.position.y - transform.position.y < 3 && r2d.rotation != 90 && r2d.rotation != -90)
        {
            AttackWeight = 6;                                       // 1:1:1:3(GroundAttack) 비율 
        }

        if(Random.Range(0, 4) < 1)
        {
            TurnCircleRotation();
        }
        else
        {
            Attack();
        }
    }

    private void OnDestroy()
    {
        bossDestroyCheck.DisCountBossCnt();
    }
}
