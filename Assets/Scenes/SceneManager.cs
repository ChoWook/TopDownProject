using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;

    Vector3 startingPos;
    Quaternion startingRotate;
    bool isStarted = false;

    static int stageNum = 0;

    private void Awake()
    {
        Time.timeScale = 0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
