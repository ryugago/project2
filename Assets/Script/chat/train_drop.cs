using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class train_drop : MonoBehaviour
{

    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

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
        
        
        foreach (var element in skipButton) // ��ư �˻�
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

        //�ؽ�Ʈ Ÿ���� ȿ��
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(delayTime); // delayTime��ŭ ���
            if (isButtonClicked)
            {
                ChatText.text = narration;
                a = narration.Length; // ��ư ������ �׳� �� ����ϰ� ��
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
        yield return StartCoroutine(NormalChat("����!!"));
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat("���ڱ� �Ӹ���..."));
        autoProceedDelay = 7f;
        yield return StartCoroutine(NormalChat("���.."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat("���.."));
        autoProceedDelay = 5f;
        quest2.text = "�������� ã�ƺ���";
        yield return StartCoroutine(NormalChat("�� ��������... ��� �����ٵ�"));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = " ";

    }
    
}


//
//            if (isButtonClicked)
//            {
//                ChatText.text = narration;
//                a = narration.Length; // ��ư ������ �׳� �� ����ϰ� ��
//                isButtonClicked = false;
//            }