using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NTraindoor : MonoBehaviour
{
    public TMPro.TMP_Text quest;

    bool trigger1=true;
    bool trigger2;


    public TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    //public TMP_Text quest;
    //public TMP_Text quest2;

    bool isButtonClicked = false;

    // Update is called once per frame
    void Update()
    {
        if (trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }

    /*IEnumerator TextPractice()
    {
        yield return new WaitForSeconds(1f);
        quest.text = " ";

    }*/

    IEnumerator TextPractice()
    {
        quest.text = " ";
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("지하철 끝에 빛이..."));
        autoProceedDelay = 0f;
        //quest.text = "빛나는 곳으로 가보자";
        yield return StartCoroutine(NormalChat(" "));
        //yield return new WaitForSeconds(1f);
        //quest.text = " ";

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&trigger1)
        {
            trigger1 = false;
            trigger2 = true;
        }
    }
}
