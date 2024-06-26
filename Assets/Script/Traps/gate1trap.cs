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

    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

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
                inter.text = "����ֽ��ϴ�";
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
    /*
    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���⵵ �����־�.. �ϴ� ����������.."));
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
