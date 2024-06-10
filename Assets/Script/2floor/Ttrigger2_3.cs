using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NovaSamples.Effects;

public class Ttrigger2_3 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

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
        /*foreach (var element in skipButton) // 버튼 검사
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
        autoProceedDelay = 2f;
        SoundManager.instance.SFXStop("underwater");
        SoundManager.instance.SFXPlay("waterup", clip);
        yield return StartCoroutine(NormalChat("푸하.."));
        autoProceedDelay = 5f;
        yield return StartCoroutine(NormalChat("허억.. 허억.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("갑자기 몸이 멋대로.."));
        yield return StartCoroutine(NormalChat("죽을 뻔 했어..."));
        yield return StartCoroutine(NormalChat("당장 여기서 나가야해.."));
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
        //Oplayer.enabled = true;
        //Ocamera.enabled = true;
        Toliet.SetActive(false);
        Player.SetActive(true);
        StartCoroutine(PRcamerplay());
        StartCoroutine(ApplyHitEffect());
        GameManager.canPlayerMove2 = true;
        quest.text = "화장실에서 나가시오";
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
            hit_effect.SetFloat("_Fullscreenintensity", Mathf.Lerp(0f, 0.3f, t)); // 수정된 부분
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the effect ends at exactly 0.3
        hit_effect.SetFloat("_Fullscreenintensity", 0.3f); // 수정된 부분
    }

}
