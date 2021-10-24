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

        if (isDead==true)
        {
            GameOverCanvas.SetActive(true);
        }
        else
        {
            GameOverCanvas.SetActive(false);
        }


    }
    public void checkHP()
    {
        if (Player.HP <= 0)
        {
            isDead = true;
            Time.timeScale = 0f;
        }
        else
        {
            isDead = false;
            Time.timeScale = 1f;
        }
    }
    public void clickStart()
    {
        warning.SetActive(true);
    }
    public void clickYes()
    {
        Time.timeScale = 1f;
        float backVol = PlayerPrefs.GetFloat("backVol", 1f);
        float effectVol = PlayerPrefs.GetFloat("effectVol", 1f);
        Player.HP = 10;
        Player.isInvenStart = true;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("backVol", backVol);
        PlayerPrefs.SetFloat("effectVol", effectVol);
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

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
        isDead = false;
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        int sn = PlayerPrefs.GetInt("StageNum");
        int hp = PlayerPrefs.GetInt("HP");
        Player.HP = hp;
        SceneManager.LoadScene(sn, LoadSceneMode.Single);
    }

    public void clickRetry()
    {
        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("HP"))
        {
            isDead = false;
            Player.HP = 10;
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
