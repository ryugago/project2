using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatroller : MonoBehaviour
{

    public Material hit_effect;

    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public AudioClip clip;

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
        autoProceedDelay = 2;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3;
        yield return StartCoroutine(NormalChat("����?? �����Ҹ���?"));
        autoProceedDelay = 6;
        hit_effect.SetFloat("_Fullscreenintensity", 0.2f);
        SoundManager.instance.SFXPlay("stairs", clip);
        yield return StartCoroutine(NormalChat("��..��.. ���ƾ�!!"));
        autoProceedDelay = 9; //20
        yield return StartCoroutine(NormalChat("��.. ����.. ���Ķ�.."));
        autoProceedDelay = 3; //23
        yield return StartCoroutine(NormalChat("�Ʊ� �� �Ҹ��� ������.."));
        autoProceedDelay = 3; //26
        yield return StartCoroutine(NormalChat("��..���� �����ǰ�?"));
        autoProceedDelay = 3; //29
        yield return StartCoroutine(NormalChat("���� ���� ���ߵǴ°ɱ�..."));
        autoProceedDelay = 3;//32
        yield return StartCoroutine(NormalChat("�ϴ� ��������� ������..."));
        autoProceedDelay = 3;//35
        yield return StartCoroutine(NormalChat(" "));
    }
}
