using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCnt : MonoBehaviour
{
    public GameObject player;

    Vector3 startingPos;
    Vector3 loadPos;
    Quaternion startingRotate;

    static int stageNum = 0;
    static bool isClear = false;

    private void Awake()
    {
      Time.timeScale = 0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        stageNum = SceneManager.GetActiveScene().buildIndex;
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

    }

    void StartGame()
    {
        Time.timeScale = 1f;
      
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            startingPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
            Instantiate(player, startingPos, startingRotate);
            isClear = false;
        }
        else
        {
            if (isClear == true)
            {
                startingPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
                Instantiate(player, startingPos, startingRotate);
                isClear = false;
            }
            else
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                loadPos = new Vector3(x, y, 0);
                Instantiate(player, loadPos, startingRotate);
            }

        }
    }

    public static void  EndGame()
    {
        Time.timeScale = 0f;
        //stageNum = SceneManager.GetActiveScene().buildIndex;
        stageNum++;


        SceneManager.LoadScene(stageNum, LoadSceneMode.Single);
        isClear = true;
       
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        int sn = PlayerPrefs.GetInt("StageNum");

        SceneManager.LoadScene(sn, LoadSceneMode.Single);

    }



}
