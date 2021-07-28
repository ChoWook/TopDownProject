﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCnt : MonoBehaviour
{
    public GameObject player;

    Vector3 startingPos;
    Quaternion startingRotate;
    

    static int stageNum = 0;

    private void Awake()
    {
      Time.timeScale = 0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        startingPos = GameObject.FindGameObjectWithTag("start").transform.position;
        startingRotate = GameObject.FindGameObjectWithTag("start").transform.rotation;
        if(stageNum >= 1)
        {
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            startingPos = GameObject.FindGameObjectWithTag("start").transform.position;
            startingRotate = GameObject.FindGameObjectWithTag("start").transform.rotation;

    }

    void StartGame()
    {
        Time.timeScale = 1f;
        

        startingPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
        Instantiate(player, startingPos, startingRotate);
      
    }

    public static void  EndGame()
    {
        Time.timeScale = 0f;
        stageNum = SceneManager.GetActiveScene().buildIndex;
        stageNum++;


        SceneManager.LoadScene(stageNum, LoadSceneMode.Single);
       
    }

    


}