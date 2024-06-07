using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger5_1 : MonoBehaviour
{
    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public TMPro.TMP_Text quest; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;
    // Start is called before the first frame update
    private bool trigger1;
    private bool trigger2=true;
    private bool trigger3;
    // Update is called once per frame


    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ
    public GameObject look;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        look.SetActive(false);
    }

    void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }
    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (look.activeSelf)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = look.GetComponent<Collider>().bounds.center;

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
        autoProceedDelay = 1f;
        look.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�ްԽ�... ���� ���� �ͺ����� �־���?"));
        StartCoroutine(questchange());
        GameManager.canPlayerMove2 = true;
        look.SetActive(false);
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
    }

    IEnumerator questchange()
    {
        quest.text = "����� ������ ���� ã��";
        yield return new WaitForSeconds(1f);
        quest.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
