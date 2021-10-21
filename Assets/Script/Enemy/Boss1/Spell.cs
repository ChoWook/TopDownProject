using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Invoke("EnableColider", 0.35f);

        Destroy(gameObject, 1.18f);
    }


    void EnableColider()
    {
        boxCollider.enabled = true;
    }

}
