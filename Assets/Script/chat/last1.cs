using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last1 : MonoBehaviour
{
    //public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    //public float delayTime;
    //public float autoProceedDelay = 5f;

    //public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    //public string writerText = "";

    //bool isButtonClicked = false;
    public last1atfer Achat;
    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Sprite callimg2;

    public TMPro.TMP_Text Tinteraction;
    public GameObject takecall;

    public Animator armpivot;

    private bool chat_a;


    private bool trigger1;
    private bool trigger2=true;


    public AudioClip clip;

    private void Update()
    {/*
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
        if (chat_a)
        {
            GameManager.canPlayerMove2 = false;
            armpivot.enabled = false;
            Tinteraction.text = "��ȭ�� �Խ��ϴ�.";
            SoundManager.instance.SFXPlay("PhoneCall", clip, true);
            takecall.SetActive(true);
            Iphone.sprite = callimg;
            if (Input.GetKeyDown(KeyCode.T))
            {
                SoundManager.instance.SFXStop("PhoneCall");
                takecall.SetActive(false);
                Tinteraction.text = " ";
                Iphone.sprite = callimg2;
                Achat.enabled = true;
                chat_a = false;
            }
        }
        if (trigger1 && trigger2)
        {
            chat_a = true;
            trigger2 = false;
        }
        //Iphone.sprite = callimg;
    }

    // Start is called before the first frame update
    /*IEnumerator NormalChat(string narration)
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
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
