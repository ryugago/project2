using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_4 : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;
    private bool trigger3;
    
    public ParticleSystem fog;
    public ParticleSystem[] fogall;

    public bool fogLook;
    public bool manelook;
    public GameObject manequin;
    // Start is called before the first frame update
    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ

    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
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
            if (fog.gameObject.activeSelf && fogLook && manelook)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = fog.gameObject.GetComponent<Collider>().bounds.center;

                // ī�޶� ������Ʈ�� �߽��� �ٶ󺸰� ȸ��
                Vector3 directionToLook = objectCenter - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else if (fog.gameObject.activeSelf && !fogLook&& manelook)
            {
                // ������Ʈ�� �߽��� ��� ���� �ݶ��̴��� �߽��� ����մϴ�.
                Vector3 objectCenter = manequin.GetComponent<Collider>().bounds.center;

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
        foreach (ParticleSystem fogs in fogall)
        {
            var main = fogs.main;
            main.startLifetime = 2f;
            main.loop = false;
        }
        fogLook = true;
        StartCoroutine(FollowObjectRotation());
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat(" "));
        manequin.SetActive(true);
        fogLook = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("��¦�̾�.. �̰� �������̾�.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("����!! ���İ� ���峪��!!"));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("???: Ǫ���� ����� �̷��ſ� �̱⳪�ϰ� ���� ������"));
        manelook = false;
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

}
