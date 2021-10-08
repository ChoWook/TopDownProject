using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss3 : MonoBehaviour
{
    enum Boss3State
    {
        Left,
        Right,
        Both
    }

    public Transform CameraPosition;
    public Transform LeftPosition;
    public Transform RightPosition;
    public int[] StateHp = {50, 50, 100};

    bool isPlyerFound = false;
    Boss3State state = Boss3State.Left;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlyerFound)
        {
            PlayerCameraFound();
        }
    }

    public void PlayerCameraFound()
    {
        CharacterController2D player = FindObjectOfType<CharacterController2D>();
        if (player)
        {
            player.isBossStage = true;
            player.mainCamera.transform.position = new Vector3(CameraPosition.position.x, CameraPosition.position.y, player.CameraZDistance);
            player.mainCamera.transform.SetParent(null);
            isPlyerFound = true;
        }
    }

    public void ChangeToRightPosition(bool isRight)
    {
        if (isRight)
        {
            transform.position = RightPosition.position;
        }
        else
        {
            transform.position = LeftPosition.position;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ReflectProjectile>() == null)
        {
            return;
        }

        if (collision.GetComponent<ReflectProjectile>().isDamaged)
        {
            switch (state)
            {
                case Boss3State.Left:
                    StateHp[0] -= 10;
                    if(StateHp[0] <= 0)
                    {
                        NextState();
                    }
                    break;
                case Boss3State.Right:
                    StateHp[1] -= 10;
                    if (StateHp[1] <= 0)
                    {
                        NextState();
                    }
                    break;
                case Boss3State.Both:
                    StateHp[2] -= 10;
                    if (StateHp[2] <= 0)
                    {
                        NextState();
                    }
                    break;
            }

            collision.gameObject.SetActive(false);
        }
    }

    public void NextState()
    {
        switch (state)
        {
            case Boss3State.Left:
                state = Boss3State.Right;
                ChangeToRightPosition(true);
                break;
            case Boss3State.Right:
                state = Boss3State.Both;
                ChangeToRightPosition(false);
                break;
            case Boss3State.Both:
                // 보스 클리어시 할 행동
                break;
        }
    }

    public void AttackPattern()
    {
        switch (state)
        {
            case Boss3State.Left:
                break;
            case Boss3State.Right:
                break;
            case Boss3State.Both:
                break;
        }
    }
}
