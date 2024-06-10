using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_1 : MonoBehaviour
{
    
    public GameObject middlshelter;
    public GameObject deleobj;

    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ

    public bool trigger = false;

    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;
    private bool trigger1 = false;
    private bool trigger2 = true;
    private bool trigger3 = false;

    private bool inpute = true;
    private bool text = true;

    public GameObject resist_img;

    public float total = 0f;
    public float Keydown = 0.1f;

    public GameObject open;

    private PlayerController playercon;
    
    public MeshRenderer doorbutton;
    public Material shader;
    
    public Collider doorbuttoncollider;

    public AudioClip clip;

    private bool shaterup = true;
    // Start is called before the first frame update
    void Start()
    {

        playercon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger1&& trigger2)
        {
            if (text)
            {
                text = false;
                StartCoroutine(anidoor());
                StartCoroutine(TextPractice());
            }
            if (inpute)
            {
                open.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inpute = false;
                    open.SetActive(false);
                    Destroy(deleobj);
                    StartCoroutine(TextPractice2());
                    SoundManager.instance.SFXPlay("shutterunlock", clip);
                }
            }
            if (trigger3)
            {
                if (total <= 0.99f)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        total += Keydown;
                    }
                }
                if (total > 1f)
                {
                    trigger2 = false;
                    resist_img.SetActive(false);
                    StartCoroutine(TextPractice3());
                }
            }
        }
    }


    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (middlshelter.activeSelf)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = middlshelter.GetComponent<Collider>().bounds.center;

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

    IEnumerator anidoor()
    {
        yield return new WaitForSeconds(1f);
        doorbuttoncollider.enabled = true;
        Material[] currentMaterials = doorbutton.materials;
        int currentLength = currentMaterials.Length;

        // ���ο� �迭�� �����ϰ�, ���̸� 2�� ����ϴ�.
        Material[] newMaterials = new Material[currentLength + 1];

        // ���� �迭�� ��Ҹ� �� �迭�� �����մϴ�.
        for (int i = 0; i < currentLength; i++)
        {
            newMaterials[i] = currentMaterials[i];
        }

        // ���ο� ���׸����� �迭�� ������ ��ҷ� �߰��մϴ�.
        newMaterials[currentLength] = shader;

        // ������Ʈ�� ���׸��� �迭�� ���� ������ �迭�� ��ü�մϴ�.
        doorbutton.materials = newMaterials;
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
        autoProceedDelay = 3f;
        middlshelter.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("�ڹ��谡 �־�.. �Ʊ� ���踦 ���°ɱ�..?"));
        //middlshelter.SetActive(false);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���� �ɾƼ� ������ϳ�..?"));
        trigger3 = true;
        resist_img.SetActive(true);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice3()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("��ó�� ��ư�� �ֳ�..."));
        autoProceedDelay = 0f;
        middlshelter.SetActive(false);
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playercon.hasKeys[2])
            {
                trigger1 = true;
            }
        }
    }
}
