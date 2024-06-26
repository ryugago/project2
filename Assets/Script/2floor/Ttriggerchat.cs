using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttriggerchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public TMPro.TMP_Text TextChater; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Ttrigger2_2 chat;
    //public Animator armpivot;
    public AudioClip clip;
    public AudioClip clip2;
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
        TextChater.text = "[나]";
        yield return StartCoroutine(NormalChat("이봐!! 여기서 내가 뭘 했다는 거야??"));
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("거울로 가보면 알아.."));
        Iphone.sprite = callimg;
        chat.enabled = true;
        SoundManager.instance.SFXStop("Walk");
        SoundManager.instance.SFXPlay("callcut", clip2);
        yield return StartCoroutine(NormalChat("너도 당해봐!! 뚝..뚝.."));
        TextChater.text = " ";
        SoundManager.instance.SFXStop("callcut");
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("뭐.. 우웁..."));
        SoundManager.instance.SFXPlay("waterdrop", clip);
        //autoProceedDelay = 0.1f;
        //yield return StartCoroutine(NormalChat(" "));
    }
}
