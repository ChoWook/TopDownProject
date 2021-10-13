using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Transform[] TurningPoints;
    public float Speed = 3f;

    protected int target = 0;

    Animator anim;
    bool isCircleRotation = true;

    public void OnParentStart()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(TurningPoints[target].transform.position.x, TurningPoints[target].transform.position.y), Speed * Time.deltaTime);
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

    public void Turn()
    {
        // 트리거에 닿았을 때 방향 바꾸기
        
    }
}
