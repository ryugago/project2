using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2 = true;
    private bool trigger3;
    private bool trigger4 = true;

    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ

    private bool Wall;
    public GameObject WallLook;
    public bool Ntrigger = false;


    [Header("�޴��� �޽���")]
    public TMPro.TMP_Text messagetext;
    public GameObject T_img;
    public GameObject messagepenul;
    public GameObject Message;

    public PauseMenu menu;

    private PlayerController Playercon;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Playercon = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }

        if(trigger3&& trigger4)
        {
            messagetext.text = "�޽����� �Խ��ϴ�.";
            T_img.SetActive(true);
            Playercon.Messagenum = 10;
            //messagepanel.num = 10;
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger4 = false;
                GameManager.canPlayerMove2 = true;
                GameManager.isPause = true;
                //posterlight.gameObject.SetActive(false);
                messagetext.text = " ";
                T_img.SetActive(false);
                //���⿡ �޴��� �޴������°� ���� ȭ�鳪����
                //messagepenul.SetActive(true);
                //Message.SetActive(true);
                Ntrigger = true;
                menu.CallMenu();
                menu.ClickMessage();
            }

        }
    }

    public void messageafter()
    {
        messagepenul.SetActive(false);
        Message.SetActive(false);
        GameManager.canPlayerMove2 = true;
        GameManager.isPause = false;
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
        Wall = true;
        GameManager.canPlayerMove2 = false;
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("����� ���� �־�.."));
        yield return StartCoroutine(NormalChat("�ٸ� ����� ã�ƺ���..."));
        Wall = false;
        autoProceedDelay = 0.1f;
        trigger3 = true;
        yield return StartCoroutine(NormalChat(" "));
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (WallLook.activeSelf && Wall)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = WallLook.GetComponent<Collider>().bounds.center;

                // ī�޶� ������Ʈ�� �߽��� �ٶ󺸰� ȸ��
                Vector3 directionToLook = objectCenter - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else
            {
                break; // �ڷ�ƾ ����
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Wall = true;
            trigger1 = true;
        }
    }

}
