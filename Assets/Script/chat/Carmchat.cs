using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmchat : MonoBehaviour
{
    public TrainDoor3open1 trigger;


    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2 = true;

    void Start()
    {
    }

    private void Update()
    {/*
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/

        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }

    // Start is called before the first frame update
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
        yield return StartCoroutine(NormalChat("��.. ���� ���� �̻��ѵ�..."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trigger.Carm)
        {
            if (other.tag == "Player")
            {
                trigger1 = true;
            }
        }
    }
}
