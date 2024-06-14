using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookShadow : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";
    [Header("ī�޶� ������")]
    public GameObject shadow; // Ȱ��ȭ�� ������Ʈ�� ���� ����
    private Transform cameraTransform; // ī�޶��� Transform ������Ʈ
    bool isButtonClicked = false;

    public TMPro.TMP_Text quest;

    private bool trigger1;
    private bool trigger2 = true;

    public Light shadowlight;

    public AudioClip sfxclip;
    public AudioClip bgm;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        shadowlight.gameObject.SetActive(false);
        shadow.SetActive(false);
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
        if (trigger1&&trigger2)
        {
            trigger2 = false;
            shadowlight.gameObject.SetActive(true);
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
            if (shadow.activeSelf)
            {
                // ī�޶� ������Ʈ�� �ٶ󺸰� ȸ��
                Vector3 directionToLook = shadow.transform.position - cameraTransform.position;
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

    IEnumerator removeobj()
    {
        yield return new WaitForSeconds(5f); // 1�� ���
        shadow.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        //GameManager.canPlayerMove2 = true;
        yield return null;
    }

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        shadow.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        StartCoroutine(removeobj());
        SoundManager.instance.SFXPlay("shadow1", sfxclip);
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�ű� ���� �־��?"));
        shadowlight.gameObject.SetActive(false);
        autoProceedDelay = 3f;
        yield return new WaitForSeconds(1f);
        shadowlight.gameObject.SetActive(true);
        yield return StartCoroutine(NormalChat("�׸��ڰ� �������"));
        autoProceedDelay = 0f;
        shadowlight.gameObject.SetActive(false);
        SoundManager.instance.BgSoundPlay(bgm);
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            trigger1 = true;
    }
}
