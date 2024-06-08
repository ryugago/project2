using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

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
        if (!scriptActive) // 스크립트가 비활성화되었다면 아래 코드를 실행하지 않음
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
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("허억..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("허억..."));
        autoProceedDelay = 3f;
        TdropPlayer.SetBool("trigger", false);
        yield return StartCoroutine(NormalChat("하.. 젠장"));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("다른 방법으로 찾아야해.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("이대로 죽을 수 없어.."));
        autoProceedDelay = 0f;
        //quest.text = "화장실을 둘러보자";
        yield return StartCoroutine(NormalChat(" "));
        yield return new WaitForSeconds(1f);
        //quest.text = " ";
    }
}
