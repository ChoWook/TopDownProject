using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyCheck : MonoBehaviour
{
    public GameObject EndPoint;
    public int BossCnt;

    public void DisCountBossCnt()
    {
        BossCnt--;
        if(BossCnt <= 0)
        {
            EndPoint.SetActive(true);
        }
    }
}
