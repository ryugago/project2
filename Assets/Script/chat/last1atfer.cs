using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last1atfer : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public TMPro.TMP_Text TextChater; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    public SpriteRenderer Iphone;
    public Sprite callimg;


    public Animator armpivot;

    [Header("메모장")]
    public TMPro.TMP_Text noteupdate;
    private bool nextMessage;

    private PlayerController Playercon;

    public AudioClip clip;
    public AudioClip bgmclip;

    void Start()
    {
        Playercon = FindObjectOfType<PlayerController>();
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        /*foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
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
        yield return StartCoroutine(NormalChat("당신 누구야?? 뭐하는 사람이야!!"));
        yield return StartCoroutine(NormalChat("왜 자꾸 장난질이야!!"));
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("장난이라… 당신은 이게 장난으로 보여요? 곧 알게 될꺼에요..."));
        Iphone.sprite = callimg;
        SoundManager.instance.SFXPlay("PhoneCallCut", clip, false);
        TextChater.text = "[나]";
        yield return StartCoroutine(NormalChat("뭐?? 뭐야..."));

        TextChater.text = " ";
        autoProceedDelay = 0.1f;
        armpivot.enabled = true;
        nextMessage = true;
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
        SoundManager.instance.BgSoundPlay(bgmclip);
        noteupdate.text = "노트가 업데이트가 됐습니다";
        Playercon.notenum = 2;
        //notepage.notenum = 2;
        yield return new WaitForSeconds(3f);
        noteupdate.text = " ";

    }
        
}
