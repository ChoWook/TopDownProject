using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{

    public Image img;
    public static bool isActive=false;
    public bool isPlaying;
    float fadeCount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (isActive)
        {
            FadeInEffect();
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

 

    IEnumerator FadeIn()
    {
       if(fadeCount == 0f)
        {
            yield break;
        }
        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.001f);
            img.color = new Color(0, 0, 0, fadeCount);
        }
    }


    public void FadeInEffect()
    {
      
        StartCoroutine("FadeIn");
    }
    
}
