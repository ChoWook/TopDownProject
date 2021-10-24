using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.UpArrow))
        {
            {
                if (other.gameObject.tag.Equals("player"))
                    Debug.Log("Destroyed");
                var player = other.GetComponent<Player>();
                Player.HP = player.getHealth();
                Destroy(gameObject);
            }
        }
    }

}
