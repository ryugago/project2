using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate1trap : MonoBehaviour
{
    public GameObject openimage;

    public TMPro.TMP_Text inter;

    public bool istrigger = false;
    public bool istrigger2 = true;
    public bool trap = false;

    public GameObject pillar;

    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    public bool cutomertrigger = false;

    public AudioClip locksound;
    //public TMPro.TMP_Text quest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (istrigger&& istrigger2)
        {
            openimage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                istrigger2 = false;
                SoundManager.instance.SFXPlay("Locksound", locksound);
                inter.text = "잠겨있습니다";
                pillar.SetActive(false);
                trap = true;
                cutomertrigger = true;
                //StartCoroutine(TextPractice());
            }
        }
    }

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
    /*
    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("여기도 막혀있어.. 일단 내려가보자.."));
        cutomertrigger = true;
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            istrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            istrigger = false;
            openimage.SetActive(false);
            inter.text = " ";
        }
    }
}
