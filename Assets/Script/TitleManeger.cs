using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManeger : MonoBehaviour
{
    public GameObject setWindow;
    public GameObject warning;
    public GameObject GameOverCanvas;
    public static bool isSetWin = false;
    public static bool isDead = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isSetWin)
        {
            setWindow.SetActive(true);
        }
        else
        {
           
        }
        checkHP();
        if (isDead)
        {
            GameOverCanvas.SetActive(true);
        }


    }
    public void checkHP()
    {
        if (Player.HP <= 0)
        {
            isDead = true;
            Time.timeScale = 0f;
        }
    }
    public void clickStart()
    {
        warning.SetActive(true);
    }
    public void clickYes()
    {
        PlayerPrefs.DeleteAll();
        LoadSceneController.LoadScene(1);
    }
    public void clickNo()
    {
        warning.SetActive(false);
    }


    public void clickExit()
    {
        Application.Quit();
    }
    public void clickMain()
    {
        LoadSceneController.LoadScene(0);
        isDead = false;
    }


    public void clickSetting()
    {
        isSetWin = true;
    }
    
    public void endSetting()
    {
        isSetWin = false;
        setWindow.SetActive(false);
    }
 
    public void clickContinue()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        else
        {
            int sn = PlayerPrefs.GetInt("StageNum");
            SceneManager.LoadScene(sn, LoadSceneMode.Single);
        }
    }

    public void clickRetry()
    {
        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("HP"))
        {
            isDead = false;
            Player.HP = 6;
            LoadSceneController.LoadScene(1);
        }
        else
        {
            int hp = PlayerPrefs.GetInt("HP");
            Player.HP = hp;
            isDead = false;
            int sn = PlayerPrefs.GetInt("StageNum");
            SceneManager.LoadScene(sn, LoadSceneMode.Single);
        }
       
    }
    
}
