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
        SceneManager.LoadScene("LoadingScene");
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
                timer += Time.unscaledDeltaTime;
                progressbar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressbar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }



}
