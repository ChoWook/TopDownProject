using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGrid : MonoBehaviour
{
    public float FallingSpeed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, transform.position.y - FallingSpeed * Time.deltaTime, 0);
  
        if (transform.position.y < -36.0f)
        {
            transform.position = new Vector3(0, 36, 0);
        }
    }
}
