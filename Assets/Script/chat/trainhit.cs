using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.Blurry;

public class trainhit : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

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

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(delayTime); // delayTime만큼 대기
            if (isButtonClicked)
            {
                ChatText.text = narration;
                a = narration.Length; // 버튼 눌리면 그냥 다 출력하게 함
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
        yield return StartCoroutine(NormalChat("으..윽..잠시만..."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("으..윽..뭐..뭐야!!"));
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
