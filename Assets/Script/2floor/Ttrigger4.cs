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

    [Header("ä��")]
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    [Header("�ֻ�")]
    public Animator jusa;
    public GameObject keyjusa;
    private bool Tjusa = false;

    [Header("��")]
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
        yield return new WaitForSeconds(1.4f); // 1.4�� ���

        // 1.4�� �Ŀ� ����� �۾���
        blurEffect.BlurRadius = 0;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
        hand5ren.material = hand5ma;
        yield return new WaitForSeconds(1f); // 1�� ���
                                             //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        yield return new WaitForSeconds(3.2f); // 1�� ���
        sit.SetBool("Tout", false);
        yield return new WaitForSeconds(1f); // 1�� ���
        StartCoroutine(TextPractice2());

    }

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
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���... ���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�������� ���� �ؾ���..."));
        //
        autoProceedDelay = 0f;
        Tjusa = true;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�� ��ȭ�� ��ü ����..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�� �� ���̷��� �ϴ°���..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("ȭ��ǿ��� ����ü �������� �־����ž�..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�ⱸ���� �� �����־���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("�׷��� �߾� ���� ������ ������..."));
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.canPlayerMove2 = true;
        Playercon.notenum = 3;
        //notepage.notenum = 3;
        intertxt.text = "�޸����� ������Ʈ�� �Ǿ����ϴ�";
        yield return new WaitForSeconds(2f);
        intertxt.text = " ";
        quest1.text = "�߾����� �̵��ϱ�";
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
