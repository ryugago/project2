using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chatcaptinkey : MonoBehaviour
{
    public TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    public TMP_Text quest;

    public bool door=false;

    bool isButtonClicked = false;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
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
        door = true;
        GameManager.onlycamera = false;
        yield return StartCoroutine(NormalChat("기장실 문이 잠겨있어.. 열쇠가 어디있지.."));
        quest.text = "열쇠를 찾아보자.";
        GameManager.onlycamera = true;
        yield return StartCoroutine(NormalChat(" "));
        yield return new WaitForSeconds(5f);
        quest.text = " ";

    }
}
