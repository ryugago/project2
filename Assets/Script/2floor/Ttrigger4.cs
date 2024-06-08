using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4 : MonoBehaviour
{
    public Ttrigger3_6 trigger;
    public Animator sit;

    public Animator Tdoor;

    private bool trigger1;
    private bool trigger2=true;

    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    [Header("주사")]
    public Animator jusa;
    public GameObject keyjusa;
    private bool Tjusa = false;

    [Header("블러")]
    public NovaSamples.Effects.BlurEffect blurEffect;
    public Material mental_breakdown;

    public Material hand5ma;
    public Renderer hand5ren;


    public TMPro.TMP_Text intertxt;
    private GameObject maincamer;
    private GameObject Player;
    
    public Quaternion Protation;
    public Vector3 Pposition;

    private PlayerController Playercon;

    public TMPro.TMP_Text quest1;

    // Start is called before the first frame update
    void Start()
    {
        Playercon = FindAnyObjectByType<PlayerController>();
        maincamer = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            StartCoroutine(PRcamerplay());
            Tdoor.SetBool("openOut", false);
            sit.SetBool("Tout", true);
            StartCoroutine(TextPractice());
            trigger2 = false;
        }
        if (Tjusa)
        {
            keyjusa.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
            {
                Tjusa = false;
                keyjusa.SetActive(false);
                jusa.enabled = true;
                jusa.SetBool("jusa", true);
                StartCoroutine(TriggerEffectsAfterDelay());
            }
        }
    }

    IEnumerator PRcamerplay()
    {
        maincamer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        PlayerController playerController = Player.GetComponent<PlayerController>();
        playerController.SetCameraRotationX(0f);
        //Player.transform.rotation = Protation;
        //Player.transform.position = Pposition;
        yield return null;
    }

    IEnumerator TriggerEffectsAfterDelay()
    {
        yield return new WaitForSeconds(1.4f); // 1.4초 대기

        // 1.4초 후에 실행될 작업들
        blurEffect.BlurRadius = 0;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
        hand5ren.material = hand5ma;
        yield return new WaitForSeconds(1f); // 1초 대기
                                             //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        yield return new WaitForSeconds(3.2f); // 1초 대기
        sit.SetBool("Tout", false);
        yield return new WaitForSeconds(1f); // 1초 대기
        StartCoroutine(TextPractice2());

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

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("허억... 허억.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("진정제를 투여 해야해..."));
        //
        autoProceedDelay = 0f;
        Tjusa = true;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("그 전화는 대체 뭐야..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("날 왜 죽이려고 하는거지..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("화장실에서 도대체 무슨일이 있었던거야..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("출구쪽은 다 막혀있었어.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("그러면 중앙 셔터 쪽으로 가볼까..."));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.canPlayerMove2 = true;
        Playercon.notenum = 3;
        //notepage.notenum = 3;
        intertxt.text = "메모장이 업데이트가 되었습니다";
        yield return new WaitForSeconds(2f);
        intertxt.text = " ";
        quest1.text = "중앙으로 이동하기";
        yield return new WaitForSeconds(3f);
        quest1.text =  "";

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (trigger.trigger)
            {
                trigger1 = true;
            }
        }
    }
}
