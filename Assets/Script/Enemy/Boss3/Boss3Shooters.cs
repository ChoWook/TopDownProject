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
                Shooters[i].SetActive(true);
            }
            else
            {
                Shooters[i].SetActive(false);
            }
        }
        Invoke("ChangePattern", 19.6f);
    }

}
