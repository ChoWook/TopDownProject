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

    Player MyPlayer;
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
        FadeEffect.isActive=true;
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            startingPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
            Instantiate(player, startingPos, startingRotate);
            isClear = false;

        }
        else
        {
            if (isClear)
            {
                startingPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
                MyPlayer = Instantiate(player, startingPos, startingRotate).GetComponent<Player>();
                Invoke("FullHealth", 0.1f);
                isClear = false;
            }
            else
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                loadPos = new Vector3(x, y, 0);
                MyPlayer = Instantiate(player, loadPos, startingRotate).GetComponent<Player>();
                MyPlayer.equipment.Load();
                MyPlayer.inventory.Load();

            }

        }
    }

    public static void  EndGame()
    {
        //Time.timeScale = 0f;
        //stageNum = SceneManager.GetActiveScene().buildIndex;
        stageNum++;
        LoadSceneController.LoadScene(stageNum);
        isClear = true;
       
       
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        int sn = PlayerPrefs.GetInt("StageNum");
        int hp = PlayerPrefs.GetInt("HP");
        Player.HP = hp;
        SceneManager.LoadScene(sn, LoadSceneMode.Single);
      
    }
    
    public void FullHealth()
    {
        Debug.Log("GetHealth : " + MyPlayer.getHealth());
        Player.HP = MyPlayer.getHealth();
    }
 }

