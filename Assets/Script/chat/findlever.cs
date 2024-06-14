using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findlever : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    public TMPro.TMP_Text quest;


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
        autoProceedDelay = 2f;
        SoundManager.instance.BgSoundPlay(clip);
        yield return StartCoroutine(NormalChat("아니!! 갑자기 어디서 불이!!"));
        yield return StartCoroutine(NormalChat("불이 이쪽으로 오고 있어!!"));
        yield return StartCoroutine(NormalChat("빨리 여기서 나가야해!!"));
        //quest.text = "지하철을 탈출 하십시오.";
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        //quest.text = " ";
    }
}
