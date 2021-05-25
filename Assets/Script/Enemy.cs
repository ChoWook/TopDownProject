using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp_max = 10;
    int Hp;
    // Start is called before the first frame update
    void Awake()
    {
        Hp = Hp_max;
    }

    // Update is called once per frame
    public int takeDamage(int dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            Hp = 0;
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
        return Hp;
    }
}
