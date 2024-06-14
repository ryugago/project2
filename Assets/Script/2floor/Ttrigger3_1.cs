using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_1 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public Ttrigger3_2 chat;

    public Animator TdropPlayer;

    public AudioClip clip;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = TdropPlayer.GetCurrentAnimatorStateInfo(0); // Assuming OCamera_stop is in layer 0
        bool isOCameraStopPlaying = stateInfo.IsName("OCamera_stop");
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
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

    IEnumerator TextPractice()
    {
        SoundManager.instance.SFXStop("Walk");
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("���⸦ ��������.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���̸� �̲�����..."));
        autoProceedDelay = 4.5f;
        SoundManager.instance.SFXPlay("sadare", clip);
        yield return StartCoroutine(NormalChat("����� ������ �ְ���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("����!!! ���ƾƾ�!!!"));
        autoProceedDelay = 3.2f;
        TdropPlayer.SetBool("trigger", true);
        yield return StartCoroutine(NormalChat("���ƾ�!!"));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" ")); chat.enabled = true;
    }
}
