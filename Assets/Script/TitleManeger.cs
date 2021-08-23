using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManeger : MonoBehaviour
{
    public GameObject setWindow;
    public static bool isSetWin = false;
    



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
     
    }

    public void clickStart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Top");
    }

    public void clickExit()
    {
        Application.Quit();
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

}
