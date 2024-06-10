using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NovaSamples.Effects;

public class Ttrigger2_3 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    public Ttrigger2_2 playcon;

    public TMPro.TMP_Text quest;
    private GameObject Player;
    public GameObject Toliet;
    //public OnlyPlayer1 Oplayer;
    //public onlycamer1 Ocamera;
    public Quaternion Protation;
    public Vector3 Pposition;

    //public BlurEffect blurEffect;

    public Material hit_effect;

    public GameObject TOUT;
    public bool sadare=false;

    public AudioClip clip;

    void Start()
    {
        Player = playcon.Player;
        StartCoroutine(TextPractice());
    }

    

    private void Update()
    {
        /*foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
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
        autoProceedDelay = 2f;
        SoundManager.instance.SFXStop("underwater");
        SoundManager.instance.SFXPlay("waterup", clip);
        yield return StartCoroutine(NormalChat("Ǫ��.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("���.. ���.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("���ڱ� ���� �ڴ��.."));
        yield return StartCoroutine(NormalChat("���� �� �߾�..."));
        yield return StartCoroutine(NormalChat("���� ���⼭ ��������.."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        //Oplayer.enabled = true;
        //Ocamera.enabled = true;
        Toliet.SetActive(false);
        Player.SetActive(true);
        StartCoroutine(PRcamerplay());
        StartCoroutine(ApplyHitEffect());
        GameManager.canPlayerMove2 = true;
        quest.text = "ȭ��ǿ��� �����ÿ�";
        sadare = true;
        //TOUT.SetActive(true);
        yield return new WaitForSeconds(1f);
        quest.text = " ";
    }
    IEnumerator PRcamerplay()
    {
        GameObject maincamer = GameObject.FindGameObjectWithTag("MainCamera");
        maincamer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        PlayerController playerController = Player.GetComponent<PlayerController>();
        playerController.SetCameraRotationX(0f);
        Player.transform.rotation = Protation;
        Player.transform.position = Pposition;
        yield return null;
    }

    IEnumerator ApplyHitEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3)
        {
            float t = elapsedTime / 3;
            hit_effect.SetFloat("_Fullscreenintensity", Mathf.Lerp(0f, 0.3f, t)); // ������ �κ�
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the effect ends at exactly 0.3
        hit_effect.SetFloat("_Fullscreenintensity", 0.3f); // ������ �κ�
    }

}
