using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//if (DoubleVision.IsInRenderFeatures() == false)
    

public class PlayerStart : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;
    // Start is called before the first frame update
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    [Header("스타트 애니메이션")]
    public Animator start_camera;
    public bool playerct = false;

    [Header("이동키 알려주는 화면")]
    public GameObject image;
    public TMPro.TMP_Text targettxt;

    public PlayerController player;

    private bool enabl = true;

    void Start()
    {
    }

    void Update()
    {
        if (!enabl)
            return;

        if (trigger1 && trigger2)
        {
            trigger2 = false;
            targettxt.text = " ";
            start_camera.SetTrigger("start");
            GameManager.canPlayerMove2 = false;
            StartCoroutine(TextPractice());
        }
        if (playerct)
        {
            GameManager.canPlayerMove2 = true;
            playerct = false;
            image.SetActive(true);

            StartCoroutine(startarget(2f));
            // 4초 후에 image 비활성화
            StartCoroutine(DisableImage(5f));
        }
    }
    IEnumerator startarget(float delay)
    {
        yield return new WaitForSeconds(delay);

        targettxt.text = "휴대폰 찾아보자";
        yield return new WaitForSeconds(2f);

        targettxt.text = " ";
    }

    IEnumerator DisableImage(float delay)
    {
        yield return new WaitForSeconds(delay);

        // image 비활성화
        image.SetActive(false);
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

    IEnumerator TextPractice()
    {
        autoProceedDelay = 7f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 5f;
        //chatBack.enabled = true;
        yield return StartCoroutine(NormalChat("여기가.."));
        autoProceedDelay = 2.5f;
        yield return StartCoroutine(NormalChat("여기가 어디지?"));
        autoProceedDelay = 2.5f;
        yield return StartCoroutine(NormalChat("깜깜해서 아무것도 안보여..."));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("뭐야!!"));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("뭐야!!"));
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat("여긴 어디야!!"));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("잠깐만"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("휴대폰.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("어..내 휴대폰!!"));
        playerct = true;
        //chatBack.enabled = false;
        yield return StartCoroutine(NormalChat(" "));
        enabl = false;
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
            trigger1 = false;
    }
}
