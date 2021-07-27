using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickStart()
    {
        SceneManager.LoadScene("Top");
    }

    public void clickExit()
    {
        Application.Quit();
    }

    public void clickSetting()
    {
        Debug.Log("구현예정");
    }
  

}
