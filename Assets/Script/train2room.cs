using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class train2room : MonoBehaviour
{
    public TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    public PlayerController player;
    [Header("����Ʈ")]
    public TMP_Text quest;
    [Header("��ȣ�ۿ�")]
    public TMP_Text inter;
    public GameObject inter_img;

    public chatcaptinkey chptinkey;

    bool isButtonClicked = false;

    bool check = false;

    GameObject nearObject;

    public GameObject traindoorimg;

    void Start()
    {
    }

    private void Update()
    {
        if ( nearObject!=null && nearObject.tag== "ViewPoint" && Input.GetKeyDown(KeyCode.E)&& check)
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();

            if (boxCollider != null)
            {
                boxCollider.enabled = false; // BoxCollider ��Ȱ��ȭ
                inter_img.SetActive(false);
                check = false;
            }
            StartCoroutine(TextPractice());
        }
        /*else if (nearObject == null)
        {
            inter_img.SetActive(false);
        }*/
        if (chptinkey.door)
        {
            Destroy(gameObject);
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
        yield return StartCoroutine(NormalChat("�̹��� ��� ���� ����?"));
        //traindoorimg.SetActive(true);
        quest.text = "�� ���¹� ã��";
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        yield return new WaitForSeconds(5f);
        //traindoorimg.SetActive(false);
        quest.text = " ";
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.hasPhone)
        {
            if (other.tag == "ViewPoint")
            {
                check = true;
                nearObject = other.gameObject;
                inter_img.SetActive(true);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (player.hasPhone)
        {
            if (other.tag == "ViewPoint")
            {
                nearObject = null;
                inter_img.SetActive(false);
            }
        }
    }
}
