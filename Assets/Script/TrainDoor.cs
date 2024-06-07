using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainDoor : MonoBehaviour
{
    public Animator Train_door1; // 애니메이터 컴포넌트를 가리키는 변수
    //public Animator Train_door2; // 애니메이터 컴포넌트를 가리키는 변수
    public GameObject Trigger;
    public TMP_Text interaction_txt;
    public PlayerController Play;

    public nolever chat1;
    //public brokenlever chat2;

    [Header("눌러라!!")]
    //public TMP_Text keydowntxt;
    public TMP_Text quest;

    [Header("E키 이미지")]
    public GameObject Eimg;

    public bool trap = false;
    private bool Isplayer = false;
    private bool Isplayer2 = true;
    int keydown = 0;

    bool check = true;

    public bool trigger_open = false;

    public AudioClip clip;

    private void Update()
    {

        if (Isplayer&& Isplayer2)
        {
            Eimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Play.hasKeys[1])
                {
                    Isplayer2 = false;
                    interaction_txt.text = "레버가 고장났습니다.";
                    quest.text = " 문 가까이가서 강제로 열자";
                    trigger_open = true;
                    Invoke("ClearInteractionText", 2f);
                    /*keydowntxt.gameObject.SetActive(true);
                    keydown++;
                    if (keydown >= 10)
                    {
                        Train_door1.SetBool("open", true); // 애니메이션의 bool 변수를 false로 설정하여 정지
                        keydowntxt.gameObject.SetActive(false);
                    }
                    //Train_door2.SetBool("open", true); // 애니메이션의 bool 변수를 false로 설정하여 정지*/
                }
                else
                {
                    if (check)
                    {
                        chat1.enabled = true;
                        check = false;
                    }
                    SoundManager.instance.BgSoundPlay(clip);
                    interaction_txt.text = "레버가 빠져있다";
                    trap = true;
                }
            }
        }
        else if (Isplayer && !Isplayer2)
        {
            Eimg.SetActive(false);

        }
        
    }
    void ClearInteractionText()
    {
        interaction_txt.text = "";
        quest.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에 들어올 때
        {
            //interaction_txt.text = "열기 E키";
            Isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //interaction_txt.text = " ";
        Eimg.SetActive(false);
        Isplayer = false;
    }

}
