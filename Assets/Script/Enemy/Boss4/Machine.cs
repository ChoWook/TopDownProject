using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Sprite[] sprites; //0: 없음, 1: 파랑, 2: 빨강
    public int state;
    SpriteRenderer spriteRenderer;

    public void ChangeState(int state)
    {
        this.state = state;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprites[state];
        Debug.Log(sprites[state] + " " + state);
    }
}
