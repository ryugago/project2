using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class train_drop : MonoBehaviour
{

    public TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    public TMP_Text quest;
    public TMP_Text quest2;

    bool isButtonClicked = false;

    private void Start()
    {
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
        while (timer < autoProceedDelay )
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
        quest.text = " ";
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("으윽!!"));
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat("갑자기 머리가..."));
        autoProceedDelay = 7f;
        yield return StartCoroutine(NormalChat("허억.."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat("허억.."));
        autoProceedDelay = 5f;
        quest2.text = "진통제를 찾아보자";
        yield return StartCoroutine(NormalChat("내 진통제가... 어딘가 있을텐데"));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = " ";

    }
    
}


//
//            if (isButtonClicked)
//            {
//                ChatText.text = narration;
//                a = narration.Length; // 버튼 눌리면 그냥 다 출력하게 함
//                isButtonClicked = false;
//            }