using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Rigidbody2D rbd;

    public float fallplat;

    private void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }


    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallplat);
        rbd.isKinematic = false;
        GetComponent<Collider2D>().isTrigger = true;
        yield return 0;
    }
}
