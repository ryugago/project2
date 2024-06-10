using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneAnimation : MonoBehaviour
{
    public Animator phoneani;
    public PlayerController player;
    public TextMeshProUGUI messagetext;
    public GameObject receive;
    [Header("�޽���")]
    //public GameObject messagemenu;
    //public GameObject Message;

    [Header("Ű����")]
    public GameObject tab;
    public GameObject Fflash;


    private bool phoneis = true;

    private bool trigger1;
    private bool trigger2 = true;

    [Header("�޴��� ����")]
    //public TMP_Text opertxt;
    public GameObject baseui;

    [Header("���� �ޱ�")]
    //public chat_pick_phone chat;
    [Header("����Ʈ")]
    public TMP_Text quest;
    public TMP_Text quest2;
    public PauseMenu menu;

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
    private bool isMessage = true;

    public GameObject cellphone;

    public AudioClip[] clip;

    void Start()
    {
        //GameManager.canPlayerMove2 = false;
        //StartCoroutine(TextPractice());
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




    // Update is called once per frame
    void Update()
    {
        if (player.hasPhone)
        {
            Destroy(cellphone);
            if(trigger1 && trigger2)
            {

                trigger2 = false;
                quest.text = " ";
                quest2.text = " ";
                GameManager.canPlayerMove2 = false;
                StartCoroutine(TextPractice());
                phoneani.SetBool("arm", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                phoneis = false;
            }
        }
        
        if (isMessage && message)
        {
            messagetext.text = "�޽����� �Խ��ϴ�.";
            receive.SetActive(true);
            //messagepanel.num = 1;
            if (Input.GetKeyDown(KeyCode.T))
            {
                quest.text = " ";
                messagetext.text = " ";
                receive.SetActive(false);
                phoneani.SetBool("arm", false);
                isMessage = false;

                GameManager.canPlayerMove2 = true;
                GameManager.isPause = true;
                menu.CallMenu();
                menu.ClickMessage();
                //baseui.SetActive(false);
                //messagemenu.SetActive(true);
                //Message.SetActive(true);
                //quest.text = "�޴��� �ѷ�����";
                //GameManager.message1 = true;
                StartCoroutine(DisplayMessageForTime(5.0f));
                StartCoroutine(messageempty());
            }
        }
    }

    IEnumerator messageempty()
    {
        yield return new WaitForSeconds(3f);
        quest2.text = " ";
    }
    
    IEnumerator DisplayMessageForTime(float duration)
    {
        //GameManager.canPlayerMove2 = false;
        //tab.SetActive(true);
        Fflash.SetActive(true);
        yield return new WaitForSeconds(duration);
        //tab.SetActive(false);
        Fflash.SetActive(false);
    }

    IEnumerator TextPractice()
    {
        //quest2.text = " ";
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("������.. �𸣴� ��ȣ��..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("��������? ��������?"));
        autoProceedDelay = 2f;
        SoundManager.instance.SFXPlay("PhoneCall", clip[0], false);
        yield return StartCoroutine(NormalChat("�����!!"));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("���� �ȵ鸮����?"));
        autoProceedDelay = 6f;
        yield return StartCoroutine(NormalChat("����.. �ƹ����� ���ݾ�.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("�Ʊ� �� �𸣴� ��ȣ��..."));
        message = true;
        playerct = false;
        ChatText.text = " ";
        //yield return StartCoroutine(NormalChat(" "));
        /*yield return StartCoroutine(NormalChat("����ö ���̶�... ���⼭ ��� ������?"));
        yield return StartCoroutine(NormalChat(" "));*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }
}
