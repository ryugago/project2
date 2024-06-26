using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerdoor : MonoBehaviour
{
    public Animator open;

    private bool dooropen;


    // Update is called once per frame
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    bool istrigger = false;


    public AudioClip unlocksound;
    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ
    public GameObject custmerdoor; // Ȱ��ȭ�� ������Ʈ�� ���� ����

    private void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        if (dooropen)
        {
            SoundManager.instance.SFXPlay("UNLocksound", unlocksound);
            open.SetBool("open", true);
            StartCoroutine(TextPractice());
            dooropen = false;
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
            if (custmerdoor.activeSelf)
            {
                // ī�޶� ������Ʈ�� �ٶ󺸰� ȸ��
                Vector3 directionToLook = custmerdoor.transform.position - cameraTransform.position;
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
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���Ⱑ �� ���� �ΰ�.. "));
        custmerdoor.SetActive(false);
        GameManager.canPlayerMove2 = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dooropen = true;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }

    }
}
