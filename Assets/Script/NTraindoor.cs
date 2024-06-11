using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NTraindoor : MonoBehaviour
{
    public TMPro.TMP_Text quest;

    bool trigger1=true;
    bool trigger2;


    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

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
        yield return StartCoroutine(NormalChat("����ö ���� ����..."));
        autoProceedDelay = 0f;
        //quest.text = "������ ������ ������";
        yield return StartCoroutine(NormalChat(" "));
        //yield return new WaitForSeconds(1f);
        //quest.text = " ";

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&trigger1)
        {
            trigger1 = false;
            trigger2 = true;
        }
    }
}
