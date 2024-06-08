using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public Animator TdropPlayer;
    public Ttrigger3 POn;
    public GameObject Player;
    public GameObject Pani;
    public TMPro.TMP_Text quest;
    private bool scriptActive = true;

    public Ttrigger3_3 trigger;

    void Start()
    {
        Pani = POn.aniPlayer;
        Player = POn.Player;
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        if (!scriptActive) // ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ��ٸ� �Ʒ� �ڵ带 �������� ����
            return;
        AnimatorStateInfo currentState = TdropPlayer.GetCurrentAnimatorStateInfo(0);
        bool isTrapAnimationPlaying = currentState.IsName("awkeplayer2");
        if (isTrapAnimationPlaying)
        {
            Pani.SetActive(false);
            Player.SetActive(true);
            trigger.enabled = true;
            scriptActive = false;
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
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���..."));
        autoProceedDelay = 3f;
        TdropPlayer.SetBool("trigger", false);
        yield return StartCoroutine(NormalChat("��.. ����"));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�ٸ� ������� ã�ƾ���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�̴�� ���� �� ����.."));
        autoProceedDelay = 0f;
        //quest.text = "ȭ����� �ѷ�����";
        yield return StartCoroutine(NormalChat(" "));
        yield return new WaitForSeconds(1f);
        //quest.text = " ";
    }
}
