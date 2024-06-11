using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 2f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;


    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Sprite callimg2;
    public GameObject takecall;
    public Animator armpivot;
    public TMPro.TMP_Text Tinteraction;
    private bool chat_a;

    public Ttriggerchat chat_a2;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        if (chat_a)
        {
            GameManager.canPlayerMove2 = false;
            armpivot.enabled = false;
            Tinteraction.text = "��ȭ�� �Խ��ϴ�.";
            takecall.SetActive(true);
            Iphone.sprite = callimg;
            if (Input.GetKeyDown(KeyCode.T))
            {
                takecall.SetActive(false);
                Tinteraction.text = " ";
                Iphone.sprite = callimg2;
                chat_a2.enabled = true;
                chat_a = false;
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
        yield return StartCoroutine(NormalChat("������ ȭ����̾�..."));
        yield return StartCoroutine(NormalChat("���⼭ �������� �־��� �ž�..."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        chat_a = true;
    }
}
