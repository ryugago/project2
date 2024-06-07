using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger2_4 : MonoBehaviour
{

    private bool trigger1;
    private bool trigger2 = true;

    public GameObject open_img;
    public TMPro.TMP_Text interac;
    public TMPro.TMP_Text quest;


    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public bool nextTrigger = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            open_img.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                trigger2 = false;
                interac.text = "������ �ʽ��ϴ�";
                StartCoroutine(TextPractice());
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
        GameManager.onlycamera = false;
        yield return StartCoroutine(NormalChat("���� ���̷�!!"));
        yield return StartCoroutine(NormalChat("���� ������ �ʾ�"));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = "���� â�������� ������";
        GameManager.onlycamera = true;
        yield return new WaitForSeconds(1f);
        nextTrigger = true;
        quest.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            open_img.SetActive(false);
            trigger1 = false;
        }
    }
}
