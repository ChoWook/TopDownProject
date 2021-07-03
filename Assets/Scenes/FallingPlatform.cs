using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    Rigidbody2D rb;
    float DelayFall = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Invoke("Fall", DelayFall);
            Debug.Log("Hit" + collision.gameObject.name);
        }
    }
    void Fall()
    {
        rb.isKinematic = false;

    }

}
