using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boss4 : MonoBehaviour
{
    public Orb[] Orbs;
    public Machine[] Machines;
    public Tilemap[] tilemaps;
    public Transform[] OrbTransform;
    public Transform PlayerStartTransform;
    public Orb OrbPrefab;

    Player player;
    bool[] isClearStates = new bool[4];

    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            tilemaps[i].gameObject.SetActive(false);
        }
        NextState();
    }

    public void ChangeState(int state)
    {
        if (state < 0 || state > 3)
        {
            return;
        }

        player = GameObject.FindObjectOfType<Player>();
        if (player != null)
        {
            player.gameObject.transform.position = PlayerStartTransform.position;
        }

        for (int i = 0; i < 4; i++)
        {
            if(Orbs[i] != null)
            {
                Destroy(Orbs[i].gameObject);
            }
        }

        for(int i = 0; i < 4; i++)
        {
            Orbs[i] = Instantiate(OrbPrefab, OrbTransform[i].position, OrbTransform[i].rotation);
            Orbs[i].boss = this;
            if (i == state)
            {
                tilemaps[i].gameObject.SetActive(true);
                Orbs[i].isCorrectOrb = true;
            }
            else
            {
                tilemaps[i].gameObject.SetActive(false);
                Orbs[i].isCorrectOrb = false;
            }
        }

        switch (state)
        {
            case 0: 
                Machines[0].ChangeState(1);
                Machines[1].ChangeState(1);
                break;
            case 1:
                Machines[0].ChangeState(1);
                Machines[1].ChangeState(2);
                break;
            case 2:
                Machines[0].ChangeState(2);
                Machines[1].ChangeState(1);
                break;
            case 3:
                Machines[0].ChangeState(2);
                Machines[1].ChangeState(2);
                break;
            default:
                break;
        }
    }

    public void ResetState()
    {
        isClearStates = new bool[4];
        ChangeState(GetRandomState());
    }

    public int GetRandomState()
    {
        int ret;

        if (CheckClearAllState())
        {
            return -1;
        }

        do { 
            ret = (int)(Random.value * 4);
        } while (isClearStates[ret]);

        return ret;
    }

    public bool CheckClearAllState()
    {
        for(int i = 0; i < 4; i++)
        {
            if (!isClearStates[i])
            {
                return false;
            }
        }
        return true;
    }

    public void NextState()
    {
        if (CheckClearAllState())
        {
            // 보스 클리어시 행할 행동들
        }
        else
        {
            ChangeState(GetRandomState());
        }
    }
}
