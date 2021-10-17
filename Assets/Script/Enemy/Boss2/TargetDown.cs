using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDown : MonoBehaviour
{
    CircleCollider2D CircleCollider;
    Animator animator;
    void Start()
    {
        CircleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        Invoke("Boom", 1.0f);
    }

    void Boom()
    {
        CircleCollider.enabled = true;
        animator.enabled = true;
        Destroy(gameObject, 0.6f);
    }
}
