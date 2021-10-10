using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Shooters : MonoBehaviour
{
    public GameObject[] Shooters;

    int state = -1;

    public void OnEnable()
    {
        ChangePattern();
    }

    public void OnDisable()
    {
        //CancelInvoke();
    }

    public void ChangePattern()
    {
        int last = state;
        for(int i = 0; i < 100; i++)
        {
            if((state = Random.Range(0, Shooters.Length)) != last){
                break;
            }
        }

        for (int i = 0; i < Shooters.Length; i++)
        {
            if (i == state)
            {
                Shooters[i].SetActive(false);
                Shooters[i].SetActive(true);
                var boss3 = FindObjectOfType<Boss3>();
                boss3.ChangeToRightPosition(Shooters[i].tag == "Boss3_Right");
                boss3.AttackDelay = Shooters[i].GetComponentInChildren<Shooter>().ShootDelay;
            }
            else
            {
                Shooters[i].SetActive(false);
            }
        }
        Invoke("ChangePattern", 19.6f);
    }

}
