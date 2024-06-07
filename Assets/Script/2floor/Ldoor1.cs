using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ldoor1 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ
    
    private bool trigger1;
    private bool trigger2 = true;


    //public TMPro.TMP_Text quest;
    public GameObject door;
    public Animator dooropen;


    bool Ldoor = true;

    public AudioClip unlocksound;
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        door.SetActive(false);
    }

    private void Update()
    {
        /*foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
        if (trigger1&&trigger2)
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

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (door.activeSelf && Ldoor)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = door.GetComponent<Collider>().bounds.center;

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

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        door.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("���� ���� �����־�"));
        yield return StartCoroutine(NormalChat("���� �Ź���"));
        Ldoor = false;
        //quest.text = "����Ʈ ������ �̵��Ͻʽÿ�";
        GameManager.canPlayerMove2 = true;
        yield return new WaitForSeconds(1f);
        SoundManager.instance.SFXPlay("UNLocksound", unlocksound);
        dooropen.SetBool("open", true);
        //quest.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //messagepanel.num = 5;
            trigger1 = true;
        }
    }
}
