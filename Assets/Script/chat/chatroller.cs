using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatroller : MonoBehaviour
{

    public Material hit_effect;

    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    public AudioClip clip;

    void Start()
    {
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
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
        SoundManager.instance.SFXStop("Walk");
        autoProceedDelay = 2;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3;
        yield return StartCoroutine(NormalChat("뭐야?? 무슨소리야?"));
        autoProceedDelay = 6;
        hit_effect.SetFloat("_Fullscreenintensity", 0.2f);
        SoundManager.instance.SFXPlay("stairs", clip);
        yield return StartCoroutine(NormalChat("어..어.. 으아악!!"));
        autoProceedDelay = 9; //20
        yield return StartCoroutine(NormalChat("으.. 으윽.. 아파라.."));
        autoProceedDelay = 3; //23
        yield return StartCoroutine(NormalChat("아까 그 소리는 뭐였지.."));
        autoProceedDelay = 3; //26
        yield return StartCoroutine(NormalChat("기..길이 막힌건가?"));
        autoProceedDelay = 3; //29
        yield return StartCoroutine(NormalChat("나는 어디로 가야되는걸까..."));
        autoProceedDelay = 3;//32
        yield return StartCoroutine(NormalChat("일단 기둥쪽으로 가볼까..."));
        autoProceedDelay = 3;//35
        yield return StartCoroutine(NormalChat(" "));
    }
}
