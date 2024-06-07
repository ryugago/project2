using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttriggerchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public TMPro.TMP_Text TextChater; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Ttrigger2_2 chat;
    //public Animator armpivot;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
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
        TextChater.text = "[��]";
        yield return StartCoroutine(NormalChat("�̺�!! ���⼭ ���� �� �ߴٴ� �ž�??"));
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("�ſ�� ������ �˾�.."));
        Iphone.sprite = callimg;
        chat.enabled = true;
        yield return StartCoroutine(NormalChat("�ʵ� ���غ�!! ��..��.."));
        TextChater.text = " ";
        yield return StartCoroutine(NormalChat("��.. ���..."));
        //autoProceedDelay = 0.1f;
        //yield return StartCoroutine(NormalChat(" "));
    }
}
