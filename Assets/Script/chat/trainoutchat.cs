using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainoutchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public TMPro.TMP_Text quest; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    private PlayerController Playercon;


    //public GameObject Trigger2;
    public GameObject Blinklight;
    public GameObject remove_fire;

    private bool trigger = false;
    private bool trigger1 = true;

    public bool enble = true;

    public AudioClip clip;
    void Start()
    {
        Blinklight.SetActive(false);
        Playercon = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!enble)
            return;

        if (trigger1 && trigger)
        {
            trigger1 = false;
            SoundManager.instance.BgSoundPlay(clip);
            StartCoroutine(TextPractice());
            GameManager.onlycamera = false;
            Destroy(remove_fire);
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
        yield return StartCoroutine(NormalChat("휴... 죽다 살아났네..."));
        yield return StartCoroutine(NormalChat("갑자기 어디서 불이..."));
        yield return StartCoroutine(NormalChat("내가 이상한걸까..."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.onlycamera = true;
        quest.text = "노트가 업데이트가 됐습니다";
        Playercon.notenum = 1;
        //notepage.notenum = 1;
        yield return new WaitForSeconds(2f);
        quest.text = " ";
        enble = false;
        DataManager.instance.DataSave();
        //Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
        }
    }


}
