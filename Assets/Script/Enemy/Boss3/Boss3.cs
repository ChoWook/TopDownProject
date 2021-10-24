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
    public Boss3Shooters LeftPatternShooters;
    public Boss3Shooters RightPatternShooters;
    public Boss3Shooters BothPatternShooters;
    public int[] StateHp = {50, 50, 100};
    public float AttackDelay = 1.8f;
    public GameObject parent;
    public GameObject EndPoint;
    public FallingGrid[] FallingGrids;
    public GameObject DmgGrid;

    bool isPlayerFound = false;
    Boss3State state = Boss3State.Left;
    Animator animator;
    AudioSource audioSource;
    CharacterController2D player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        AttackTrriger();

        LeftPatternShooters.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerFound)
        {
            PlayerCameraFound();
        }

        if (!player)
        {
            player = FindObjectOfType<CharacterController2D>();
        }

        if (player)
        {
            player.isBossStage = true;
        }
    }

    public void PlayerCameraFound()
    {
        CharacterController2D player = FindObjectOfType<CharacterController2D>();
        if (player)
        {
            player.isFixedBossCamera = true;
            player.mainCamera.transform.position = new Vector3(CameraPosition.position.x, CameraPosition.position.y, player.CameraZDistance);
            player.mainCamera.transform.SetParent(null);
            player.jumpHeight *= 0.9f;
            isPlayerFound = true;
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
            audioSource.Play();
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

            Destroy(collision.gameObject);
        }
    }

    public void NextState()
    {
        switch (state)
        {
            case Boss3State.Left:
                state = Boss3State.Right;
                ChangeToRightPosition(true);
                LeftPatternShooters.gameObject.SetActive(false);
                RightPatternShooters.gameObject.SetActive(true);
                break;
            case Boss3State.Right:
                state = Boss3State.Both;
                ChangeToRightPosition(false);
                RightPatternShooters.gameObject.SetActive(false);
                BothPatternShooters.gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().color = new Color(1, 0.6f, 0.6f, 1);
                break;
            case Boss3State.Both:
                // 보스 클리어시 할 행동
                foreach(var FallingGrid in FallingGrids)
                {
                    FallingGrid.FallingSpeed = 0;
                }
                EndPoint.SetActive(true);
                DmgGrid.SetActive(false);
                Destroy(parent);
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

    public void AttackTrriger()
    {
        animator.SetTrigger("Attack");

        Invoke("AttackTrriger", AttackDelay);
    }
}
