using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questsc : MonoBehaviour
{
    public TMPro.TMP_Text quest; // 캔버스에 있는 Text 컴포넌트
    public TMPro.TMP_Text quest2; // 캔버스에 있는 Text 컴포넌트
    public float delay = 2f; // 변경될 때까지의 지연 시간(초)
    //private string previousQuestText;

    void Start()
    {
        quest.text = " ";
        quest2.text = " ";
        // 2초 후에 텍스트 변경 함수 호출
    }
    void Update()
    {
        // 예시: questText의 텍스트가 변경되는 경우를 가정
        if (quest.text != " " && quest.text!= "노트가 업데이트가 됐습니다.")
        {
            StartCoroutine(UpdateQuestTextAfterDelay(quest.text));
            // 변경된 텍스트를 questText2로 업데이트
        }/*
        else if(quest2.text=="휴대폰 둘러보기")
        {
            StartCoroutine(nomessage());
        }*/
    }

    IEnumerator nomessage()
    {
        // delay 초 동안 대기
        yield return new WaitForSeconds(1);

        // delay 이후에 텍스트 변경
        quest2.text = " ";
    }

    IEnumerator UpdateQuestTextAfterDelay(string newText)
    {
        // delay 초 동안 대기
        yield return new WaitForSeconds(delay);

        // delay 이후에 텍스트 변경
        quest2.text = newText;
    }
    
}
