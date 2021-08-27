using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public GameObject Portal;
    public GameObject Player;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player = other.gameObject;
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return null;
        Player.transform.position = new Vector2 (Portal.transform.position.x, Portal.transform.position.y);
    }

}
