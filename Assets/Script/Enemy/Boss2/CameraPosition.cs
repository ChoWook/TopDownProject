using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    bool isPlayerFound = false;
    void Update()
    {
        if (!isPlayerFound)
        {
            PlayerCameraFound();
        }
    }

    public void PlayerCameraFound()
    {
        CharacterController2D player = FindObjectOfType<CharacterController2D>();
        if (player)
        {
            player.isBossStage = true;
            player.mainCamera.transform.position = transform.position;
            player.mainCamera.transform.SetParent(null);
            isPlayerFound = true;
        }
    }
}
