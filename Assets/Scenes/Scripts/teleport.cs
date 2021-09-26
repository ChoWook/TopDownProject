using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public GameObject Portal;
    public GameObject Player;


    public void OnTriggerStay2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Player")&& Input.GetKey(KeyCode.UpArrow))
            {
                {
                    Player = other.gameObject;
                    StartCoroutine(Teleport());
                }
            }        
    }


    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.2f);
        Player.transform.position = new Vector2 (Portal.transform.position.x, Portal.transform.position.y);
    }

}
