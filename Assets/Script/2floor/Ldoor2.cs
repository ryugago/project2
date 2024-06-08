using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.DoubleVision;

public class Ldoor2 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2=true;
    public TMPro.TMP_Text quest2;
    public Ldoor2_1 chat;
    private bool Tjusa;
    [Header("ȭ���")]
    public NovaSamples.Effects.BlurEffect blurEffect;
    public Material mental_breakdown;

    [Header("�ڵ�")]
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
        /*foreach (var element in skipButton) // ��ư �˻�
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
        yield return new WaitForSeconds(1.0f); // 1.4�� ���
        Ptrip.SetBool("trap2f", false);
        yield return new WaitForSeconds(0.4f); // 1.4�� ���

        foreach(GameObject TV in CCTV)
        {
            TV.SetActive(false);
        }
        blurEffect.BlurRadius = 0;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        settings.strength.z = 0f;
        // 1.4�� �Ŀ� ����� �۾���
        yield return new WaitForSeconds(1f); // 1�� ���

        hand4ren.material = hand4ma;                                     //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        yield return new WaitForSeconds(3.2f); // 1�� ���
        yield return new WaitForSeconds(1f); // 1�� ���
        chat.enabled = true;
        nextplayermove = true;
    }

    // Start is called before the first frame update
    IEnumerator NormalChat(string narration)
    {
        int a = 0;
        writerText = "";

        //�ؽ�Ʈ Ÿ���� ȿ��
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(delayTime); // delayTime��ŭ ���
            if (isButtonClicked)
            {
                ChatText.text = narration;
                a = narration.Length; // ��ư ������ �׳� �� ����ϰ� ��
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
        yield return StartCoroutine(NormalChat("�̻���... �ʹ� �̻���..."));
        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        settings.strength.z = 1f;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0.1f);
        yield return StartCoroutine(NormalChat("�������� ������ ���� ������� �־�.."));

        yield return StartCoroutine(NormalChat("�������� ���� ���� �ؾ���..."));
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
