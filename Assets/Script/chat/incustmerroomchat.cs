using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incustmerroomchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public bool cutomertrigger = false;
    public TMPro.TMP_Text quest;

    public Animator Ccamera;


    public GameObject Player;
    public PlayerController Maincamera;
    public GameObject PMaincamera;

    public Quaternion Protation;
    public Vector3 Pposition;

    void Start()
    {
        StartCoroutine(TextPractice());
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
    }

    IEnumerator MovePlayerToPosition(Transform playerTransform, Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = playerTransform.position;
        Quaternion startRotation = playerTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //playerTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            playerTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //playerTransform.position = targetPosition;
        playerTransform.rotation = targetRotation;
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
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 3f;
        //Ccamera.SetTrigger("close");

        Maincamera.SetCameraRotationX(0);
        PMaincamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(MovePlayerToPosition(Player.transform, Pposition, Protation, 2f));

        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("��..��..����..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�̷�..���� ���ٴ�.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("��� ���� �� ��Ұ���..."));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        quest.text = "���� ����� ã�ƺ���.";
        cutomertrigger = true;
        GameManager.canPlayerMove2 = true;
        yield return new WaitForSeconds(3f);
        quest.text = " ";

    }
}
