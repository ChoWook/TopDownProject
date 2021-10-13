using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Red : Boss2
{
    // Start is called before the first frame update
    void Start()
    {
        OnParentStart();
        GetComponent<SpriteRenderer>().color = new Color(1, 0.6f, 0.6f, 1);
        target = 2;
    }

}
