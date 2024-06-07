using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatControl : MonoBehaviour
{

    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    [Header("��ŸƮ �ִϸ��̼�")]
    public Animator start_camera;
    public bool playerct = false;

    [Header("�̵�Ű �˷��ִ� ȭ��")]
    public GameObject image;
    public TMP_Text targettxt;

    void Start()
    {
        targettxt.text = " ";
        start_camera.SetTrigger("start");
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
            image.SetActive(true);

            StartCoroutine(startarget(2f));
            // 4�� �Ŀ� image ��Ȱ��ȭ
            StartCoroutine(DisableImage(5f));
        }
    }
    IEnumerator startarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        targettxt.text = "�޴��� ã�ƺ���";
        yield return new WaitForSeconds(2f);

        targettxt.text = " ";
    }

    IEnumerator DisableImage(float delay)
    {
        yield return new WaitForSeconds(delay);

        // image ��Ȱ��ȭ
        image.SetActive(false);
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
        autoProceedDelay = 7f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 5f;
        //chatBack.enabled = true;
        yield return StartCoroutine(NormalChat("���Ⱑ.."));
        autoProceedDelay = 2.5f;
        yield return StartCoroutine(NormalChat("���Ⱑ �����?"));
        autoProceedDelay = 2.5f;
        yield return StartCoroutine(NormalChat("�����ؼ� �ƹ��͵� �Ⱥ���..."));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("����!!"));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("����!!"));
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat("���� ����!!"));
        autoProceedDelay = 1.5f;
        yield return StartCoroutine(NormalChat("���"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("�޴���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("��..�� �޴���!!"));

        //chatBack.enabled = false;
        yield return StartCoroutine(NormalChat(" "));
    }
}


//
//            if (isButtonClicked)
//            {
//                ChatText.text = narration;
//                a = narration.Length; // ��ư ������ �׳� �� ����ϰ� ��
//                isButtonClicked = false;
//            }