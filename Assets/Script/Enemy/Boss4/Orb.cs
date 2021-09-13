using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Boss4 boss;
    public bool isCorrectOrb = false;
    public Enemy enemy;

    private void OnDestroy()
    {
        if(enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }

        if (!enemy.GetIsDead())
        {
            return;
        }

        if (isCorrectOrb)
        {
            boss.NextState();
        }
        else
        {
            boss.ResetState();
        }
    }
}
