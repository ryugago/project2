using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainoutchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public TMPro.TMP_Text quest; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

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
        yield return StartCoroutine(NormalChat("��... �״� ��Ƴ���..."));
        yield return StartCoroutine(NormalChat("���ڱ� ��� ����..."));
        yield return StartCoroutine(NormalChat("���� �̻��Ѱɱ�..."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.onlycamera = true;
        quest.text = "��Ʈ�� ������Ʈ�� �ƽ��ϴ�";
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
