using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_5 : MonoBehaviour
{

    public Animator dooropen;
    public GameObject open_img;

    public GameObject mannequin;

    private bool trigger1;
    private bool trigger2=true;

    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ
    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            open_img.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                dooropen.SetTrigger("open");
                StartCoroutine(TextPractice());
                trigger2 = false;
            }
        }
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (mannequin.activeSelf)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = mannequin.GetComponent<Collider>().bounds.center;

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
        mannequin.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���ƾ�..��¦�̾�..."));
        GameManager.canPlayerMove2 = true;
        mannequin.SetActive(false);
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
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
