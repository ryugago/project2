using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneAnimation : MonoBehaviour
{
    public Animator phoneani;
    public PlayerController player;
    public TextMeshProUGUI messagetext;
    public GameObject receive;
    [Header("메시지")]
    //public GameObject messagemenu;
    //public GameObject Message;

    [Header("키맵핑")]
    public GameObject tab;
    public GameObject Fflash;


    private bool phoneis = true;

    private bool trigger1;
    private bool trigger2 = true;

    [Header("휴대폰 조작")]
    //public TMP_Text opertxt;
    public GameObject baseui;

    [Header("문자 받기")]
    //public chat_pick_phone chat;
    [Header("퀘스트")]
    public TMP_Text quest;
    public TMP_Text quest2;
    public PauseMenu menu;

    public TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키
    public string writerText = "";

    bool isButtonClicked = false;


    [Header("플레이어 움직임")]
    public bool playerct = false;
    // public TMP_Text quest2;

    public bool message = false;
    private bool isMessage = true;

    public GameObject cellphone;

    public AudioClip[] clip;

    void Start()
    {
        //GameManager.canPlayerMove2 = false;
        //StartCoroutine(TextPractice());
    }

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




    // Update is called once per frame
    void Update()
    {
        if (player.hasPhone)
        {
            Destroy(cellphone);
            if(trigger1 && trigger2)
            {

                trigger2 = false;
                quest.text = " ";
                quest2.text = " ";
                GameManager.canPlayerMove2 = false;
                StartCoroutine(TextPractice());
                phoneani.SetBool("arm", true); // 애니메이션의 bool 변수를 false로 설정하여 정지
                phoneis = false;
            }
        }
        
        if (isMessage && message)
        {
            messagetext.text = "메시지가 왔습니다.";
            receive.SetActive(true);
            //messagepanel.num = 1;
            if (Input.GetKeyDown(KeyCode.T))
            {
                quest.text = " ";
                messagetext.text = " ";
                receive.SetActive(false);
                phoneani.SetBool("arm", false);
                isMessage = false;

                GameManager.canPlayerMove2 = true;
                GameManager.isPause = true;
                menu.CallMenu();
                menu.ClickMessage();
                //baseui.SetActive(false);
                //messagemenu.SetActive(true);
                //Message.SetActive(true);
                //quest.text = "휴대폰 둘러보기";
                //GameManager.message1 = true;
                StartCoroutine(DisplayMessageForTime(5.0f));
                StartCoroutine(messageempty());
            }
        }
    }

    IEnumerator messageempty()
    {
        yield return new WaitForSeconds(3f);
        quest2.text = " ";
    }
    
    IEnumerator DisplayMessageForTime(float duration)
    {
        //GameManager.canPlayerMove2 = false;
        //tab.SetActive(true);
        Fflash.SetActive(true);
        yield return new WaitForSeconds(duration);
        //tab.SetActive(false);
        Fflash.SetActive(false);
    }

    IEnumerator TextPractice()
    {
        //quest2.text = " ";
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("누구지.. 모르는 번호야..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("여보세요? 누구세요?"));
        autoProceedDelay = 2f;
        SoundManager.instance.SFXPlay("PhoneCall", clip[0], false);
        yield return StartCoroutine(NormalChat("저기요!!"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("제말 안들리세요?"));
        autoProceedDelay = 6f;
        yield return StartCoroutine(NormalChat("뭐야.. 아무말도 없잖아.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("아까 그 모르는 번호야..."));
        message = true;
        playerct = false;
        ChatText.text = " ";
        //yield return StartCoroutine(NormalChat(" "));
        /*yield return StartCoroutine(NormalChat("지하철 안이라... 여기서 어떻게 나가지?"));
        yield return StartCoroutine(NormalChat(" "));*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }
}
