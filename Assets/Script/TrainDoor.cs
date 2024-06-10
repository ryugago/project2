using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainDoor : MonoBehaviour
{
    public Animator Train_door1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //public Animator Train_door2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public GameObject Trigger;
    public TMP_Text interaction_txt;
    public PlayerController Play;

    public nolever chat1;
    //public brokenlever chat2;

    [Header("������!!")]
    //public TMP_Text keydowntxt;
    public TMP_Text quest;

    [Header("EŰ �̹���")]
    public GameObject Eimg;

    public bool trap = false;
    private bool Isplayer = false;
    private bool Isplayer2 = true;
    int keydown = 0;

    bool check = true;

    public bool trigger_open = false;

    public AudioClip clip;


    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";
    
    bool isButtonClicked = false;
    
    private void Update()
    {

        if (Isplayer&& Isplayer2)
        {
            Eimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Play.hasKeys[1])
                {
                    Isplayer2 = false;
                    StartCoroutine(TextPractice());
                    quest.text = " �� �����̰��� ������ ����";
                    trigger_open = true;
                    Invoke("ClearInteractionText", 2f);
                    /*keydowntxt.gameObject.SetActive(true);
                    keydown++;
                    if (keydown >= 10)
                    {
                        Train_door1.SetBool("open", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                        keydowntxt.gameObject.SetActive(false);
                    }
                    //Train_door2.SetBool("open", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����*/
                }
                else
                {
                    if (check)
                    {
                        chat1.enabled = true;
                        check = false;
                    }
                    SoundManager.instance.BgSoundPlay(clip);
                    //interaction_txt.text = "������ �����ִ�";
                    trap = true;
                }
            }
        }
        else if (Isplayer && !Isplayer2)
        {
            Eimg.SetActive(false);

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
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���� ������ ���峵�� ���� ������ �������!!"));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = " ";
    }


    void ClearInteractionText()
    {
        interaction_txt.text = "";
        quest.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            //interaction_txt.text = "���� EŰ";
            Isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //interaction_txt.text = " ";
        Eimg.SetActive(false);
        Isplayer = false;
    }

}
