using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chat_pick_phone : MonoBehaviour
{
    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű
    public string writerText = "";

    bool isButtonClicked = false;


    [Header("�÷��̾� ������")]
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
        foreach (var element in skipButton) // ��ư �˻�
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
        yield return StartCoroutine(NormalChat("������.. �𸣴� ��ȣ��..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("��������? ��������?"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("�����!!"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("���� �ȵ鸮����?"));
        autoProceedDelay = 6f;
        yield return StartCoroutine(NormalChat("����.. �ƹ����� ���ݾ�.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("�Ʊ� �� �𸣴� ��ȣ��..."));
        message = true;
        ChatText.text = " ";
        //yield return StartCoroutine(NormalChat(" "));
        /*yield return StartCoroutine(NormalChat("����ö ���̶�... ���⼭ ��� ������?"));
        yield return StartCoroutine(NormalChat(" "));*/
    }
}
