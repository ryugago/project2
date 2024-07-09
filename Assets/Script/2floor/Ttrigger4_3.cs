using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_3 : MonoBehaviour
{

    private bool trigger1;
    private bool trigger2 = true;
    private bool trigger3 = false;

    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public TMPro.TMP_Text TextChater; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Sprite callimg2;
    public Sprite callimg3;
    public TMPro.TMP_Text Tinteraction;
    //public GameObject takecall;

    public Animator armpivot;
    public Animator doorclose;
    [Header("��ƼŬ")]
    public ParticleSystem fog;
    public bool fogLook;

    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ

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
            doorclose.SetTrigger("close");
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
        if (trigger3)
        {
            armpivot.enabled = false;
            Tinteraction.text = "��ȭ�� �Խ��ϴ�.";
            //takecall.SetActive(true);
            Iphone.sprite = callimg;
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger3 = false;
                Tinteraction.text = " ";
                //takecall.SetActive(false);
                Iphone.sprite = callimg2;
                StartCoroutine(TextPractice2());
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

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (fog.gameObject.activeSelf && fogLook)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = fog.gameObject.GetComponent<Collider>().bounds.center;

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
        autoProceedDelay = 3f;
        fogLook = true;
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("���ڱ� �Ȱ���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���� �Ⱥ���..."));
        //middlshelter.SetActive(false);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        trigger3 = true;
    }

    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("�׳��� �Ȱ��� ���� �� ���̿���..."));
        autoProceedDelay = 3f;
        TextChater.text = "[��]";
        yield return StartCoroutine(NormalChat("�׳�? �׳��̶�� ���� ���̾�?"));
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("�ʰ� �����̿� ��ȭ�� �ϴ� ��"));
        autoProceedDelay = 3f;
        TextChater.text = "[��]";
        yield return StartCoroutine(NormalChat("�����̶��... ���� ���� ���� �ִٰ�.."));
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("�ʰ� �� �߾˲� �ƴϾ�..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("������ �𸣰ھ�? �ްԽ��� �ѹ� ã�ƺ�..."));

        TextChater.text = " ";
        Iphone.sprite = callimg3;

        fogLook = false;
        armpivot.enabled = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.canPlayerMove2 = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
