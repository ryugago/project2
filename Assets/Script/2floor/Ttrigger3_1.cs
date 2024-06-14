using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_1 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

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
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("여기를 나가야해.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("왜이리 미끄러워..."));
        autoProceedDelay = 4.5f;
        SoundManager.instance.SFXPlay("sadare", clip);
        yield return StartCoroutine(NormalChat("여기로 나갈수 있겠지.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("제발!!! 으아아아!!!"));
        autoProceedDelay = 3.2f;
        TdropPlayer.SetBool("trigger", true);
        yield return StartCoroutine(NormalChat("으아악!!"));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" ")); chat.enabled = true;
    }
}
