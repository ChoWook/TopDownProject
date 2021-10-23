using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TalkManager : MonoBehaviour
{
    public static int sceneNumber;
    public int tIndex;
    public Text talkTxt;
    public GameObject txtPanel;
    Dictionary<int, string[]> talkData;
    public bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();

        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        Action();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (Input.GetButtonDown("Fire1")){
            Action();
        }
    }
    public void Action()
    {
        Talk(sceneNumber);
        txtPanel.SetActive(isActivate);
    }


    public void GenerateData()
    {
        talkData.Add(1, new string[] { "윽..","기억이..", "여긴.. 뭐지..?" ,"일단 빠져나가자"});
        talkData.Add(7, new string[] { "저게.. 뭐야??"});
        talkData.Add(9, new string[] {"탑에 어떻게 이런 공간이..?","바닥에 닿으면 위험할 것 같다." });
        talkData.Add(11, new string[] { "덤벼라!" });
        talkData.Add(13, new string[] { "뿜어내는 기운이 심상치 않다.","저 녀석만 잡으면 이제 보석이..!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }

        return talkData[id][talkIndex];
    }

    public void Talk(int id)
    {
        int temp = id;

        string talkDt = GetTalk(id,tIndex);

        if(talkDt == null)
        {
            isActivate = false;
            if (temp != id)
            {
                tIndex = 0;
            }
            return;
        }
        talkTxt.text = talkDt;
        isActivate = true;
        tIndex++;

    }

    
    
}
