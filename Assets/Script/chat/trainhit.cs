using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.Blurry;

public class trainhit : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public AudioClip clip;

    private bool trigger1;
    private bool trigger2 = true;

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            SoundManager.instance.SFXPlay("tinnitus", clip);
            Blurry.AddRenderFeature();
            Blurry.Settings setting = Blurry.GetSettings();
            StartCoroutine(IncreaseBlurryStrength(0.12f, 0.5f));
            StartCoroutine(TextPractice());
        }
    }

    private IEnumerator IncreaseBlurryStrength(float targetStrength, float duration)
    {
        Blurry.Settings setting = Blurry.GetSettings();
        float initialStrength = setting.strength;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            setting.strength = Mathf.Lerp(initialStrength, targetStrength, elapsedTime / duration);
            //Blurry.SetSettings(setting);
            yield return null;
        }

        setting.strength = targetStrength;
        //Blurry.SetSettings(setting);
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
        autoProceedDelay = 2.5f;
        yield return StartCoroutine(NormalChat("��..��..��ø�..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("��..��..��..����!!"));
        GameManager.canPlayerMove2 = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }
}
