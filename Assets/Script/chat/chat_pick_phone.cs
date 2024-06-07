using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chat_pick_phone : MonoBehaviour
{
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

    void Start()
    {
        GameManager.canPlayerMove2 = false;
        StartCoroutine(TextPractice());
    }

    void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        if (playerct)
        {
            GameManager.canPlayerMove2 = true;
            playerct = false;
        }
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
        //quest2.text = " ";
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("누구지.. 모르는 번호야..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("여보세요? 누구세요?"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("저기요!!"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("제말 안들리세요?"));
        autoProceedDelay = 6f;
        yield return StartCoroutine(NormalChat("뭐야.. 아무말도 없잖아.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("아까 그 모르는 번호야..."));
        message = true;
        ChatText.text = " ";
        //yield return StartCoroutine(NormalChat(" "));
        /*yield return StartCoroutine(NormalChat("지하철 안이라... 여기서 어떻게 나가지?"));
        yield return StartCoroutine(NormalChat(" "));*/
    }
}
