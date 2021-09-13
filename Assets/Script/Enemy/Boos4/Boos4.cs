using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boos4 : MonoBehaviour
{
    public GameObject[] Orbs;
    public GameObject[] Machines;
    public Tilemap[] tilemaps;

    Transform[] OrbTransform = new Transform[4];

    void Start()
    {
        for(int i = 0; i < Orbs.Length; i++)
        {
            OrbTransform[i] = Orbs[i].transform;
        }

        for(int i = 0; i < tilemaps.Length; i++)
        {
            tilemaps[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
