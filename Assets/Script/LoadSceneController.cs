using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadSceneController : MonoBehaviour
{
    static int nextSceneNumber;
    [SerializeField]
    Image progressbar;


    public static void LoadScene(int sceneNumber)
    {
        nextSceneNumber = sceneNumber;
        switch (sceneNumber)
        {
            case 8:
                SceneManager.LoadScene("Boss4Loading");
                break;
            case 10:
                SceneManager.LoadScene("Boss3Loading");
                break;
            case 12:
                SceneManager.LoadScene("Boss2Loading");
                break;
            case 14:
                SceneManager.LoadScene("Boss1Loading");
                break;
            default:
                SceneManager.LoadScene("LoadingScene");
                break;
        }

       
    }

 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneNumber,LoadSceneMode.Single);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;


            if(op.progress < 0.9f)
            {
                progressbar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.deltaTime;
                progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, 1f, timer);
                if(progressbar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }



}
