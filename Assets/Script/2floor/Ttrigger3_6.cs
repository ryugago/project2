using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_6 : MonoBehaviour
{
    public GameObject lookopen;

    private PlayerController playercon;
    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ

    public Animator dooropen;

    public bool trigger = false;

    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;
    private bool trigger1=true;

    public AudioClip opentoilet;
    //private bool trigger2=true;
    // Start is called before the first frame update
    void Start()
    {
        lookopen.SetActive(false);
        playercon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playercon.hasKeys[2]&& trigger1)
        {
            trigger1 = false;
            dooropen.SetBool("openOut", true);
            SoundManager.instance.SFXPlay("opentoilet", opentoilet);
            StartCoroutine(TextPractice());
        }
    }


    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (lookopen.activeSelf)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = lookopen.GetComponent<Collider>().bounds.center;

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
        GameManager.canPlayerMove2 = false;
        lookopen.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat(" "));
        lookopen.SetActive(false);
        trigger = true;
        GameManager.canPlayerMove2 = true;
    }

}
