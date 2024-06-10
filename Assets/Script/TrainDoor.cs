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


    public TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";
    
    bool isButtonClicked = false;
    
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
                    StartCoroutine(TextPractice());
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
                    //interaction_txt.text = "레버가 빠져있다";
                    trap = true;
                }
            }
        }
        else if (Isplayer && !Isplayer2)
        {
            Eimg.SetActive(false);

        }
        
    }

    // Start is called before the first frame update
    IEnumerator NormalChat(string narration)
    {
        int a = 0;
        writerText = "";

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(delayTime); // delayTime만큼 대기
            if (isButtonClicked)
            {
                ChatText.text = narration;
                a = narration.Length; // 버튼 눌리면 그냥 다 출력하게 함
                isButtonClicked = false;
            }
        }
        //yield return new WaitForSeconds(autoProceedDelay);


        float timer = 0f;
        while (timer < autoProceedDelay)
        {

            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("젠장 레버가 고장났어 문을 강제로 열어야해!!"));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = " ";
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
