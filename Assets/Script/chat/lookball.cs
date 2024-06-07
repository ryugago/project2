using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookball : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 5f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;
    
    public GameObject ball;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    private bool trigger1;
    private bool trigger2=true;

    bool Tlookball = true;

    private Transform player;
    public Vector3 Pposition;
    public Quaternion Protation;

    public last1 chat;
    public AudioClip clip;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        ball.SetActive(false);
    }
    private void Update()
    {/*
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/

        //Iphone.sprite = callimg;
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }

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

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (ball.activeSelf && Tlookball)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = ball.GetComponent<Collider>().bounds.center;

                // 카메라를 오브젝트의 중심을 바라보게 회전
                Vector3 directionToLook = objectCenter - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else
            {
                break; // 코루틴 종료
            }
        }
    }


    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        player.position = Pposition;
        player.rotation = Protation;
        ball.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 1.5f;
        SoundManager.instance.SFXPlay("ball", clip);
        yield return StartCoroutine(NormalChat("저기에 공이.."));
        autoProceedDelay = 2f;
        yield return StartCoroutine(NormalChat("이쪽으로 오고 있잖아"));
        Tlookball = false;
        GameManager.canPlayerMove2 = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        chat.enabled = true;
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
