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
    public GameObject remindImage;
    public GameObject reminder2;
    public GameObject thankyou;
    Dictionary<int, string[]> talkData;
    public bool isActivate;
    public bool isEnd=false;



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
       

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Action();
        }

        if (isEnd)
        {
            thankyou.SetActive(true);
            Time.timeScale = 0f;
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
        talkData.Add(6, new string[] {"윽.. 드디어 탈출했다...",".....?!","기억이..!","","탑에서 심상치않은 기운이 느껴진다.","본래의 모습으로 돌아가려는 듯하다" });
        talkData.Add(8, new string[] { "저게.. 뭐야??"});
        talkData.Add(10, new string[] {"탑에 어떻게 이런 공간이..?","바닥에 닿으면 위험할 것 같다." });
        talkData.Add(12, new string[] { "덤벼라!" });
        talkData.Add(14, new string[] { "뿜어내는 기운이 심상치 않다.","저 녀석만 잡으면 이제 보석이..!" });
        talkData.Add(15, new string[] { "드디어 보석이..!","..?","갑자기 보석이 밝게 빛난다.","","", "...", "......?", "난 누구지..?" ,""});
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

        if(id==6 && tIndex == 3)
        {
            remindImage.SetActive(true);
        }
        else
        {
            remindImage.SetActive(false);
        }
        if(id==15 && tIndex == 3)
        {
            reminder2.SetActive(true);

        }
        else if(id==15 && tIndex >= 8)
        {
            isEnd = true;
        }
        else
        {
            reminder2.SetActive(false);
           
        }



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
