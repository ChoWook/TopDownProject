using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] float fallTime = 0.5f, returnTime = 2f;
    Rigidbody2D rb;
    Vector2 startPos;
    bool isBack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        if (isBack)
            transform.position = Vector2.MoveTowards(transform.position, startPos, 20 * Time.deltaTime);

        if (transform.position.y == startPos.y)
            isBack = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player") && !isBack)
            Invoke("FallPlatform", fallTime);
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackPlatform", returnTime);
    }

    void BackPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        isBack = true;
    }
}