using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.DoubleVision;

public class Ldoor2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2=true;
    public TMPro.TMP_Text quest2;
    public Ldoor2_1 chat;
    private bool Tjusa;
    [Header("화면블러")]
    public NovaSamples.Effects.BlurEffect blurEffect;
    public Material mental_breakdown;

    [Header("핸드")]
    public GameObject jusaimg;
    public Animator jusa;
    public Material hand4ma;
    public Renderer hand4ren;
    public Animator Ptrip;

    public onlycamer Ocamer;
    public OnlyPlayer Oplayer;

    bool jusa_img = true;

    private bool nextplayermove;

    private GameObject player;

    public Animator gate_close;
    public AudioClip gate_close_sound;

    public GameObject[] CCTV;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject TV in CCTV)
        {
            TV.SetActive(false);
        }
    }
    private void Update()
    {
        AnimatorStateInfo stateInfo = Ptrip.GetCurrentAnimatorStateInfo(0); // Assuming OCamera_stop is in layer 0
        bool isOCameraStopPlaying = stateInfo.IsName("OCamera_stop");
        bool playermove = stateInfo.IsName("camera_idle");

        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        /*foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
        if (trigger1 && trigger2)
        {
            foreach (GameObject TV in CCTV)
            {
                TV.SetActive(true);
            }
            trigger2 = false;
            Ptrip.SetBool("trap2f", true);
            StartCoroutine(TextPractice());
        }
        if (Tjusa&& isOCameraStopPlaying)
        {
            if (jusa_img) { jusa_img = false; jusaimg.SetActive(true); }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                Tjusa = false;
                jusaimg.SetActive(false);
                jusa.SetBool("jusa", true);
                StartCoroutine(TriggerEffectsAfterDelay());
            }
        }
        if(nextplayermove&& playermove)
        {
            nextplayermove = false;
            Vector3 newPosition = player.transform.position;
            newPosition.x += 1.5f;
            player.transform.position = newPosition;
        }
    }

    IEnumerator TriggerEffectsAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // 1.4초 대기
        Ptrip.SetBool("trap2f", false);
        yield return new WaitForSeconds(0.4f); // 1.4초 대기

        foreach(GameObject TV in CCTV)
        {
            TV.SetActive(false);
        }
        blurEffect.BlurRadius = 0;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        settings.strength.z = 0f;
        // 1.4초 후에 실행될 작업들
        yield return new WaitForSeconds(1f); // 1초 대기

        hand4ren.material = hand4ma;                                     //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        yield return new WaitForSeconds(3.2f); // 1초 대기
        yield return new WaitForSeconds(1f); // 1초 대기
        chat.enabled = true;
        nextplayermove = true;
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
        gate_close.SetBool("open", false);
        SoundManager.instance.SFXPlay("gateclosesound", gate_close_sound);
        quest2.text = " ";
        GameManager.canPlayerMove2 = false;
        jusa.enabled = true;
        Ocamer.enabled = true;
        Oplayer.enabled = true;
        jusa.enabled = true;
        yield return StartCoroutine(NormalChat("이상해... 너무 이상해..."));
        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        settings.strength.z = 1f;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0.1f);
        yield return StartCoroutine(NormalChat("빨간색의 점들이 나를 노려보고 있어.."));

        yield return StartCoroutine(NormalChat("진정제를 빨리 투여 해야해..."));
        Tjusa = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

}
