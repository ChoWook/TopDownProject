using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Sprite[] sprites; //0: 없음, 1: 파랑, 2: 빨강
    public int state;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeState(0);
    }

    public void ChangeState(int state)
    {
        this.state = state;
        spriteRenderer.sprite = sprites[state];
    }
}
