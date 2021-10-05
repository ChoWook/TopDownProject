using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    public Transform CameraPosition;
    public Transform LeftPosition;
    public Transform RightPosition;

    bool isPlyerFound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlyerFound)
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
            player.mainCamera.transform.position = new Vector3(CameraPosition.position.x, CameraPosition.position.y, player.CameraZDistance);
            player.mainCamera.transform.SetParent(null);
            isPlyerFound = true;
        }
    }
}
