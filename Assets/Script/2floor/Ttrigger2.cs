using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 2f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;


    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Sprite callimg2;
    public GameObject takecall;
    public Animator armpivot;
    public TMPro.TMP_Text Tinteraction;
    private bool chat_a;

    public Ttriggerchat chat_a2;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        if (chat_a)
        {
            GameManager.canPlayerMove2 = false;
            armpivot.enabled = false;
            Tinteraction.text = "전화가 왔습니다.";
            takecall.SetActive(true);
            Iphone.sprite = callimg;
            if (Input.GetKeyDown(KeyCode.T))
            {
                takecall.SetActive(false);
                Tinteraction.text = " ";
                Iphone.sprite = callimg2;
                chat_a2.enabled = true;
                chat_a = false;
            }
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
        yield return StartCoroutine(NormalChat("오래된 화장실이야..."));
        yield return StartCoroutine(NormalChat("여기서 무슨일이 있었던 거야..."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        chat_a = true;
    }
}
